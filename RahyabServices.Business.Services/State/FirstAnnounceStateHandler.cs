using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class FirstAnnounceStateHandler : DelinquentState{
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IStateRepository _stateRepository;
        public FirstAnnounceStateHandler(){
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
        }
        public FirstAnnounceStateHandler(CustomerDelinquent customerDelinquent, DateTime expireDate){
            ExpireDate = expireDate;
            HistoryCustomerDelinquentId = customerDelinquent.Id;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
            Task.Run(() => InitializeAsync(customerDelinquent)).Wait();
        }
        public DateTime Created { get; set; }
        public Task Initialization { get; private set; }
        private async Task InitializeAsync(CustomerDelinquent customerDelinquent){
            var smsService = AutofacHostFactory.Container.Resolve<ISmsService>();
            //sms felan ersal nemishavad
            await smsService.SendSms(customerDelinquent, TemplateType.Month);
            var firstAnnounceState = Mapper.Map<FirstAnnounceStateHandler, FirstAnnounceState>(this);
            await _stateRepository.SaveAsync(firstAnnounceState);
            Id = firstAnnounceState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var scondAnnounce = new SecondAnnounceStateHandler(customerDelinquent,
                customerDelinquent.MaturityDate.AddDays(-7));
            customerDelinquent.SetState(scondAnnounce.Id);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
    }
}