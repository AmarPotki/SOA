using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RequestClearingStateHandler : DelinquentState{
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly IStateRepository _stateRepository;
        public RequestClearingStateHandler(AddClearingLogDto addClearingLogDto){
            HistoryCustomerDelinquentId = addClearingLogDto.CustomerDelinquentId;            
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();           
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();            
            Task.Run(() => InitializeAsync(addClearingLogDto)).Wait();
        }
        private async Task InitializeAsync(AddClearingLogDto addClearingLogDto){            
            var requestClearingState =
                Mapper.Map<RequestClearingStateHandler, RequestClearingState>(this);
            await _stateRepository.SaveAsync(requestClearingState);
            Id = requestClearingState.Id;
        }
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new NotImplementedException();
        }
    }
}