using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RequestGivingAChanceStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        public RequestGivingAChanceStateHandler(AddGivingAChanceLogDto addGivingAChanceLogDto){
            HistoryCustomerDelinquentId = addGivingAChanceLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addGivingAChanceLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addGivingAChanceLogDto, personnelCode)).Wait();
        }
        private async Task InitializeAsync(AddGivingAChanceLogDto addGivingAChanceLogDto, string personnelCode)
        {
            
            var requestgivingAChanceState = Mapper.Map<RequestGivingAChanceStateHandler, RequestGivingAChanceState>(this);
            await _stateRepository.SaveAsync(requestgivingAChanceState);
            Id = requestgivingAChanceState.Id;
        }
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new NotImplementedException();
        }
    }
}