using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RegisterStateHandler : DelinquentState{
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IStateRepository _stateRepository;
        private  ISmsService _smsService;
        public RegisterStateHandler(){
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
        }
        public RegisterStateHandler(DateTime expireDate, IStateRepository stateRepository,
            ICustomerDelinquentRepository customerDelinquentRepository){
            ExpireDate = expireDate;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _customerDelinquentRepository = AutofacHostFactory.Container.Resolve<ICustomerDelinquentRepository>();
               
                Task.Run(()=> InitializeAsync()).Wait();
        }
        public RegisterStateHandler(DateTime expireDate, ICustomerDelinquentRepository customerDelinquentRepository){
            _customerDelinquentRepository = customerDelinquentRepository;
            ExpireDate = expireDate;
         
            Initialization = InitializeAsync();
        }
        public DateTime Created { get; set; }
        public Task Initialization { get; private set; }
        private async Task InitializeAsync(){
            var registerState = Mapper.Map<RegisterStateHandler, RegisterState>(this);
           
            await _stateRepository.SaveAsync(registerState);
            Id = registerState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var currenState = customerDelinquent.CurrentState;
            currenState.HistoryCustomerDelinquentId = customerDelinquent.Id;
           await _stateRepository.SaveAsync(currenState);
            var announceOne = new FirstAnnounceStateHandler(customerDelinquent, customerDelinquent.MaturityDate.AddDays(-15));
            customerDelinquent.SetState(announceOne.Id);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
    }
}