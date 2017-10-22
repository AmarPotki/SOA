using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Appointment;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class AppointmentStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ICryptographer _cryptographer;
        public AppointmentStateHandler(DateTime dateTime, AddAppointmentLogDto addAppointmentLogDto)
        {
            _cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>(); ;
            ExpireDate = dateTime;
            HistoryCustomerDelinquentId = addAppointmentLogDto.CustomerDelinquentId;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
            _hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt( addAppointmentLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addAppointmentLogDto, personnelCode)).Wait(); 
        }
        private async Task InitializeAsync(AddAppointmentLogDto addAppointmentLogDto, string personnelCode)
        {
            var callLog = new AppointmentLog
            {
                Author = personnelCode,
                Result = addAppointmentLogDto.Result,
                Created = DateTime.Now,
                AgentFullName = addAppointmentLogDto.AgentFullName,
                DateAction = addAppointmentLogDto.DateAction,
            };
            callLog.SetCustomerDelinquentId(addAppointmentLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(callLog);
            var appointmentState = Mapper.Map<AppointmentStateHandler, AppointmentState>(this);
            await _stateRepository.SaveAsync(appointmentState);
            Id = appointmentState.Id;
        }
        public Task Initialization { get; private set; }

        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var notification = _notificationFactory.Create("گذشت ده روز از ملاقات حضوری", "ده روز از ملاقات حضوری گذشت", customerDelinquent, NotificationType.Appointment);
            await _notificationRepository.SaveAsync(notification);
        }
    }
}