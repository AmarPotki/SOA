using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State
{
    public class SecondAnnounceStateHandler : DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        public SecondAnnounceStateHandler(CustomerDelinquent customerDelinquent, System.DateTime dateTime){
            HistoryCustomerDelinquentId = customerDelinquent.Id;
            var smsService = AutofacHostFactory.Container.Resolve<ISmsService>();
            //sms felan ersal nemishavad
            smsService.SendSms(customerDelinquent, TemplateType.Week);
            ExpireDate= dateTime;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
            Task.Run(() => InitializeAsync(customerDelinquent)).Wait();
        }
        public SecondAnnounceStateHandler(){
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
        }
        private async Task InitializeAsync(CustomerDelinquent customerDelinquent){
            var smsService = AutofacHostFactory.Container.Resolve<ISmsService>();
            //sms felan ersal nemishavad
            await smsService.SendSms(customerDelinquent, TemplateType.SecondWeek);
            var scondAnnounceState = Mapper.Map<SecondAnnounceStateHandler, SecondAnnounceState>(this);
            await _stateRepository.SaveAsync(scondAnnounceState);
            Id = scondAnnounceState.Id;
        }
        public Task Initialization { get;  set; }
        public override async Task Handler(CustomerDelinquent customerDelinquent)
        {
            var thirdAnnounceState = new ThirdAnnounceStateHandler(customerDelinquent, customerDelinquent.MaturityDate.AddDays(10));
            customerDelinquent.SetState(thirdAnnounceState.Id);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
    }
}