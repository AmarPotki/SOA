using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RequestImpunityForCrimesStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;

        public RequestImpunityForCrimesStateHandler(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto){
            HistoryCustomerDelinquentId = addImpunityForCrimesLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addImpunityForCrimesLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addImpunityForCrimesLogDto, personnelCode)).Wait();
        }
        private async Task InitializeAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto, string personnelCode)
        {
            
            var requestgivingAChanceState = Mapper.Map<RequestImpunityForCrimesStateHandler, RequestImpunityForCrimesState>(this);
            await _stateRepository.SaveAsync(requestgivingAChanceState);
            Id = requestgivingAChanceState.Id;
        }
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new NotImplementedException();
        }
    }
}