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
using RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class FirstWrittenNoticeDtoStateHandler : DelinquentState{
        private readonly ICryptographer _cryptographer;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly IStateRepository _stateRepository;
        public FirstWrittenNoticeDtoStateHandler(DateTime dateTime, AddWrittenNoticeLogDto addWrittenNoticeLogDto){
            HistoryCustomerDelinquentId = addWrittenNoticeLogDto.CustomerDelinquentId;
            _cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            ExpireDate = dateTime;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
            _hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addWrittenNoticeLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addWrittenNoticeLogDto, personnelCode)).Wait();
        }
        public FirstWrittenNoticeDtoStateHandler(){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
        }
        public Task Initialization { get; private set; }
        private async Task InitializeAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto, string personnelCode)
        {
            var callLog = new WrittenNoticeLog
            {
                Author = personnelCode,
                Created = DateTime.Now,
                LetterNumber = addWrittenNoticeLogDto.LetterNumber,
                DocumentUrl = addWrittenNoticeLogDto.DocumentUrl
               
            };
            callLog.SetCustomerDelinquentId(addWrittenNoticeLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(callLog);
            var firstNoticeCallState =
                Mapper.Map<FirstWrittenNoticeDtoStateHandler, FirstWrittenNoticeState>(this);
            await _stateRepository.SaveAsync(firstNoticeCallState);
            Id = firstNoticeCallState.Id;
        }
        public async Task Handler(CustomerDelinquent customerDelinquent, AddWrittenNoticeLogDto addWrittenNoticeLogDto)
        {
            await Handler(customerDelinquent);
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var notification = _notificationFactory.Create("ارسال اخطاریه دوم برای تسهیلات به شماره ", "اخطار دوم به  ",
                customerDelinquent,
                NotificationType.Call);
            await _notificationRepository.SaveAsync(notification);
        }
    }
}