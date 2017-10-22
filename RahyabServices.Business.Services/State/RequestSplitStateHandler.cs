using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RequestSplitStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        public RequestSplitStateHandler(AddSplitLogDto addSplitLogDto){
            HistoryCustomerDelinquentId = addSplitLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
          
            Task.Run(() => InitializeAsync(addSplitLogDto)).Wait();
        }
        public RequestSplitStateHandler(){
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
        }
        private async Task InitializeAsync(AddSplitLogDto addSplitLogDto)
        {
         
            var requestSplitState = Mapper.Map<RequestSplitStateHandler, RequestSplitState>(this);
            await _stateRepository.SaveAsync(requestSplitState);
            Id = requestSplitState.Id;
        }
        public override  Task Handler(CustomerDelinquent customerDelinquent){

           throw new Exception();
        }
      
    }
}