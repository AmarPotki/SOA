using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class RenewalStateHandler : DelinquentState
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        public RenewalStateHandler(AddRenewalLogDto addRenewalLogDto){
            HistoryCustomerDelinquentId = addRenewalLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addRenewalLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addRenewalLogDto, personnelCode)).Wait();
        }
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new System.NotImplementedException();
        }
        private async Task InitializeAsync(AddRenewalLogDto addRenewalLogDto, string personnelCode){
            var renewalLog = Mapper.Map<AddRenewalLogDto, RenewalLog>(addRenewalLogDto);

            renewalLog.Author = personnelCode;
            renewalLog.Created = DateTime.Now;
            renewalLog.SetCustomerDelinquentId(addRenewalLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(renewalLog);
            var renewalState = Mapper.Map<RenewalStateHandler, RenewalState>(this);
            await _stateRepository.SaveAsync(renewalState);
            Id = renewalState.Id;
        }
    }
}