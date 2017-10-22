using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class ThirdAnnounceStateHandler : DelinquentState{
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly IStateRepository _stateRepository;
        public ThirdAnnounceStateHandler(CustomerDelinquent customerDelinquent, DateTime dateTime){
            HistoryCustomerDelinquentId = customerDelinquent.Id;
            ExpireDate = dateTime;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
            Task.Run(() => InitializeAsync(customerDelinquent)).Wait();
        }
        public ThirdAnnounceStateHandler(){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
        }
        public Task Initialization { get; private set; }
        private async Task InitializeAsync(CustomerDelinquent customerDelinquent){
            var smsService = AutofacHostFactory.Container.Resolve<ISmsService>();
            //sms felan ersal nemishavad
            await smsService.SendSms(customerDelinquent, TemplateType.Week);
            var thirdAnnounceState = Mapper.Map<ThirdAnnounceStateHandler, ThirdAnnounceState>(this);
            await _stateRepository.SaveAsync(thirdAnnounceState);
            Id = thirdAnnounceState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var notification = _notificationFactory.Create("ارسال اخطاریه اول برای تسهیلات به شماره ", "اخطار اول به ",
                customerDelinquent, NotificationType.Call);
            await _notificationRepository.SaveAsync(notification);
        }
    }
}