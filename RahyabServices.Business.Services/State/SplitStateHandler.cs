using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State
{
    public class SplitStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;
        public SplitStateHandler(AddSplitLogDto addSplitLogDto){
            HistoryCustomerDelinquentId = addSplitLogDto.CustomerDelinquentId;
            ExpireDate = addSplitLogDto.StartDate.Date.AddMonths(2);
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>(); 
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addSplitLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addSplitLogDto, personnelCode)).Wait();
        }
        public SplitStateHandler(){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
        }
        public SplitStateHandler(RequestSplitLog requestSplitLog, string respondUserName){
            HistoryCustomerDelinquentId = requestSplitLog.CustomerDelinquentId;
            ExpireDate = requestSplitLog.LegislationDate.Date.AddMonths(2);
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            var personnelCode = hrFacade.GetPersonnelCode(respondUserName);
            Task.Run(() => InitializeAsync(requestSplitLog, personnelCode)).Wait();
        }
        private async Task InitializeAsync(RequestSplitLog requestSplitLog, string personnelCode){
            var splitLog = Mapper.Map<RequestSplitLog, SplitLog>(requestSplitLog);
            splitLog.Created = DateTime.Now;
            splitLog.ApproverPersonnelCode = personnelCode;
            await _logBaseRepository.SaveAsync(splitLog);
            var spliteState = Mapper.Map<SplitStateHandler, SplitState>(this);
            await _stateRepository.SaveAsync(spliteState);
            Id = spliteState.Id;
        }
        private async Task InitializeAsync(AddSplitLogDto addSplitLogDto, string personnelCode)
        {
            var splitStateLog = Mapper.Map<AddSplitLogDto, SplitLog>(addSplitLogDto);
            splitStateLog.Author = personnelCode;
            splitStateLog.Created = DateTime.Now;
            splitStateLog.StartDate =addSplitLogDto.LegislationDate.AddMonths(addSplitLogDto.Count);
            splitStateLog.SetCustomerDelinquentId(addSplitLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(splitStateLog);
            var spliteState = Mapper.Map<SplitStateHandler, SplitState>(this);
            await _stateRepository.SaveAsync(spliteState);
            Id = spliteState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var notification = _notificationFactory.Create("چک کردن عملکرد مشتری بعد از تقسیط", "دو ماه از زمان تقسیط مشتری گذشته است، عملکرد مشتری چک شود ", customerDelinquent, NotificationType.CheckProcess);
            await _notificationRepository.SaveAsync(notification); 
        }
    }
}