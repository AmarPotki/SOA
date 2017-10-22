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
namespace RahyabServices.Business.Services.State
{
    public class SecondWrittenNoticeStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ICryptographer _cryptographer;
       

        public SecondWrittenNoticeStateHandler(DateTime dateTime, AddWrittenNoticeLogDto addWrittenNoticeLogDto)
        {
            _cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            HistoryCustomerDelinquentId = addWrittenNoticeLogDto.CustomerDelinquentId;
            ExpireDate = dateTime;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
            _hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addWrittenNoticeLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addWrittenNoticeLogDto, personnelCode)).Wait();
        }
        public SecondWrittenNoticeStateHandler(){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
        }
        private async Task InitializeAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto, string personnelCode)
        {
            var writtenNoticeLog = new WrittenNoticeLog
            {
                Author = personnelCode,
                LetterNumber = addWrittenNoticeLogDto.LetterNumber,
                Created = DateTime.Now,
                DocumentUrl = addWrittenNoticeLogDto.DocumentUrl,
            };

            writtenNoticeLog.SetCustomerDelinquentId(addWrittenNoticeLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(writtenNoticeLog);
            var secondNoticeCallState = Mapper.Map<SecondWrittenNoticeStateHandler, SecondWrittenNoticeState>(this);
            await _stateRepository.SaveAsync(secondNoticeCallState);
            Id = secondNoticeCallState.Id;
        }
        public Task Initialization { get; private set; }
        public override async Task Handler(CustomerDelinquent customerDelinquent)
        {
            var notification = _notificationFactory.Create("ارسال اخطاریه سوم برای تسهیلات به شماره ", "اخطار سوم به ", customerDelinquent, NotificationType.Call);
            await _notificationRepository.SaveAsync(notification);

         
        }
    }
}