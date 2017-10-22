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
    public class ClearingStateHandler : DelinquentState{
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly IStateRepository _stateRepository;
        public ClearingStateHandler(AddClearingLogDto addClearingLogDto ){
            HistoryCustomerDelinquentId = addClearingLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>(); 
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addClearingLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addClearingLogDto, personnelCode)).Wait(); 
        }
        public ClearingStateHandler(RequestClearingLog requestClearingLogDto, string respondUserName){
            HistoryCustomerDelinquentId = requestClearingLogDto.CustomerDelinquentId;
            ExpireDate = null;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            var personnelCode = hrFacade.GetPersonnelCode(respondUserName);
            Task.Run(() => InitializeAsync(requestClearingLogDto,personnelCode)).Wait();
        }
        private async Task InitializeAsync(RequestClearingLog requestClearingLogDto, string personnelCode)
        {
            var clearingLog = Mapper.Map<RequestClearingLog, ClearingLog>(requestClearingLogDto);
            clearingLog.Created = DateTime.Now;
            clearingLog.ApproverPersonnelCode = personnelCode;
            await _logBaseRepository.SaveAsync(clearingLog);
            var clearingState = Mapper.Map<ClearingStateHandler, ClearingState>(this);
            await _stateRepository.SaveAsync(clearingState);
            Id = clearingState.Id;
        }
        private async Task InitializeAsync(AddClearingLogDto addNoticeLogDto, string personnelCode)
        { 
            var clearingLog = Mapper.Map<AddClearingLogDto, ClearingLog>(addNoticeLogDto);
            clearingLog.Author = personnelCode;
            clearingLog.Created = DateTime.Now;
            clearingLog.SetCustomerDelinquentId(addNoticeLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(clearingLog);
            var clearingState = Mapper.Map<ClearingStateHandler, ClearingState>(this);
            await _stateRepository.SaveAsync(clearingState);
            Id = clearingState.Id;
        }
        public Task Initialization { get; private set; }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
          throw new Exception("اینجا چرا امدی ؟؟؟");
        }
    }
}