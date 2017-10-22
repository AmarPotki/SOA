using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State{
    public class GivingAChanceStateHandler : DelinquentState{
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly IStateRepository _stateRepository;
        public GivingAChanceStateHandler(AddGivingAChanceLogDto addGivingAChanceLogDto){
            HistoryCustomerDelinquentId = addGivingAChanceLogDto.CustomerDelinquentId;
            ExpireDate = addGivingAChanceLogDto.LegislationDate.Date.AddMonths(addGivingAChanceLogDto.Count).AddDays(-10);
            var cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode =
                hrFacade.GetPersonnelCode(cryptographer.Decrypt(addGivingAChanceLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addGivingAChanceLogDto, personnelCode)).Wait();
        }
        public GivingAChanceStateHandler(int id){
            _notificationRepository = AutofacHostFactory.Container.Resolve<INotificationRepository>();
            _notificationFactory = AutofacHostFactory.Container.Resolve<INotificationFactory>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            Id = id;
        }
        public GivingAChanceStateHandler(RequestGivingAChanceLog requestGivingAChanceLog, string respondUserName){
            HistoryCustomerDelinquentId = requestGivingAChanceLog.CustomerDelinquentId;
            ExpireDate = requestGivingAChanceLog.LegislationDate.Date.AddMonths(requestGivingAChanceLog.Count).AddDays(-10);         
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            var personnelCode = hrFacade.GetPersonnelCode(respondUserName);
            Task.Run(() => InitializeAsync(requestGivingAChanceLog, personnelCode)).Wait();
        }
        private async Task InitializeAsync(RequestGivingAChanceLog requestGivingAChanceLog, string personnelCode){
            var givingAChanceLog =
                Mapper.Map<RequestGivingAChanceLog, GivingAChanceLog>(requestGivingAChanceLog);
            givingAChanceLog.Created = DateTime.Now;
            givingAChanceLog.ApproverPersonnelCode = personnelCode;
            givingAChanceLog.ExpireDate = requestGivingAChanceLog.LegislationDate.AddMonths(requestGivingAChanceLog.Count);
            await _logBaseRepository.SaveAsync(givingAChanceLog);
            var givingAChanceState = Mapper.Map<GivingAChanceStateHandler, GivingAChanceState>(this);
            await _stateRepository.SaveAsync(givingAChanceState);
            Id = givingAChanceState.Id;
        }
        private async Task InitializeAsync(AddGivingAChanceLogDto addGivingAChanceLogDto, string personnelCode){
            var givingAChanceLog =
                Mapper.Map<AddGivingAChanceLogDto, GivingAChanceLog>(addGivingAChanceLogDto);
            givingAChanceLog.Author = personnelCode;
            givingAChanceLog.SetCustomerDelinquentId(addGivingAChanceLogDto.CustomerDelinquentId);
            givingAChanceLog.Created = DateTime.Now;
            givingAChanceLog.ExpireDate = addGivingAChanceLogDto.LegislationDate.AddMonths(addGivingAChanceLogDto.Count);
            await _logBaseRepository.SaveAsync(givingAChanceLog);
            var givingAChanceState = Mapper.Map<GivingAChanceStateHandler, GivingAChanceState>(this);
            await _stateRepository.SaveAsync(givingAChanceState);
            Id = givingAChanceState.Id;
        }
        public override async Task Handler(CustomerDelinquent customerDelinquent){
            Notification notification;
            if (!await _notificationRepository.IsExistTendDaysLeftType(customerDelinquent.Id)){
                notification = _notificationFactory.Create("چک کردن عملکرد مشتری",
                    "ده روز به پایان مهلت تعیین شده ی مشتری مانده است، عملکرد مشتری چک شود ", customerDelinquent,
                    NotificationType.TenDaysLeft);
                var state = await _stateRepository.OneAsync(Id);
                state.ExpireDate = DateTime.Now.Date.AddDays(10);
                await _stateRepository.SaveAsync(state);
            }
            else{
                notification = _notificationFactory.Create("شروع اقدام قانونی",
                    "  مهلت تعیین شده برای مشتری به پایان رسیده است، اقدام قانونی شروع شود ", customerDelinquent,
                    NotificationType.StartLegalAction);
            }
            await _notificationRepository.SaveAsync(notification);
        }
    }
}