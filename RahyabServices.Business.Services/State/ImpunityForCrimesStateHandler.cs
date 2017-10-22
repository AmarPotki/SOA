using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class ImpunityForCrimesStateHandler:DelinquentState{
        private readonly IStateRepository _stateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;
        public ImpunityForCrimesStateHandler(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto){
            HistoryCustomerDelinquentId = addImpunityForCrimesLogDto.CustomerDelinquentId;
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>(); 
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = hrFacade.GetPersonnelCode(cryptographer.Decrypt(addImpunityForCrimesLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addImpunityForCrimesLogDto, personnelCode)).Wait();
        }
        public ImpunityForCrimesStateHandler(RequestImpunityForCrimesLog requestImpunityForCrimesLogDto, string respondUserName){
            HistoryCustomerDelinquentId = requestImpunityForCrimesLogDto.CustomerDelinquentId;
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var hrfacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            var personnelCode = hrfacade.GetPersonnelCode(respondUserName);
            Task.Run(() => InitializeAsync(requestImpunityForCrimesLogDto,personnelCode)).Wait();
        }
        public ImpunityForCrimesStateHandler(){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
        }
        private async Task InitializeAsync(RequestImpunityForCrimesLog requestImpunityForCrimesLogDto, string personnelCode)
        {
            var impunityForCrimesLog = Mapper.Map<RequestImpunityForCrimesLog, ImpunityForCrimesLog>(requestImpunityForCrimesLogDto);
            impunityForCrimesLog.Created = DateTime.Now;
            impunityForCrimesLog.ApproverPersonnelCode = personnelCode;
            await _logBaseRepository.SaveAsync(impunityForCrimesLog);
            var givingAChanceState = Mapper.Map<ImpunityForCrimesStateHandler, ImpunityForCrimesState>(this);
            await _stateRepository.SaveAsync(givingAChanceState);
            Id = givingAChanceState.Id;
        }
        private async Task InitializeAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto, string personnelCode)
        {
            var impunityForCrimesLog = Mapper.Map<AddImpunityForCrimesLogDto, ImpunityForCrimesLog>(addImpunityForCrimesLogDto);
            impunityForCrimesLog.Author = personnelCode;
            impunityForCrimesLog.SetCustomerDelinquentId(addImpunityForCrimesLogDto.CustomerDelinquentId);
            impunityForCrimesLog.Created = DateTime.Now;
            await _logBaseRepository.SaveAsync(impunityForCrimesLog);
            var givingAChanceState = Mapper.Map<ImpunityForCrimesStateHandler, ImpunityForCrimesState>(this);
            await _stateRepository.SaveAsync(givingAChanceState);
            Id = givingAChanceState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            var notification = _notificationFactory.Create("چک کردن عملکرد بعد از بخشودگی جرائم  ", "دو ماه از زمان بخشودگی جرائم مشتری گذشته است، عملکرد مشتری چک شود ", customerDelinquent, NotificationType.CheckProcess);
            await _notificationRepository.SaveAsync(notification); 
        }
    }
}