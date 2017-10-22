using System;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class ImpunityService : IImpunityService{
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly ICryptographer _cryptographer;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IHrFacade _hrFacade;
        private readonly IImpunityLogListRepository _impunityLogListRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ILogPrivilegeService _logPrivilegeService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        public ImpunityService(ICustomerDelinquentRepository customerDelinquentRepository,
            INotificationFactory notificationFactory, INotificationRepository notificationRepository,
            ILogBaseRepository logBaseRepository, IHrFacade hrFacade,
            ICryptographer cryptographer, IBranchPrivilegesService branchPrivilegesService,
            ILogPrivilegeService logPrivilegeService, IImpunityLogListRepository impunityLogListRepository){
            _customerDelinquentRepository = customerDelinquentRepository;
            _notificationFactory = notificationFactory;
            _notificationRepository = notificationRepository;
            _logBaseRepository = logBaseRepository;
            _hrFacade = hrFacade;
            _cryptographer = cryptographer;
            _branchPrivilegesService = branchPrivilegesService;
            _logPrivilegeService = logPrivilegeService;
            _impunityLogListRepository = impunityLogListRepository;
        }
        public async Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addImpunityForCrimesLogDto.AuthorUserName));
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(
                    addImpunityForCrimesLogDto.CustomerDelinquentId);
            if (await _branchPrivilegesService.IsValidPrivilege(branchCode, addImpunityForCrimesLogDto)){
                var state = new ImpunityForCrimesStateHandler(addImpunityForCrimesLogDto);
                customerDelinquent.SetState(state.Id);
            }
            else{
                // save sharepoint item
                var requestSplitStateState = new RequestImpunityForCrimesStateHandler(addImpunityForCrimesLogDto);
                customerDelinquent.SetState(requestSplitStateState.Id);
                var splitLogList = Mapper.Map<AddImpunityForCrimesLogDto, ImpunityLogList>(addImpunityForCrimesLogDto);
                splitLogList.RequestStateHandlerId = requestSplitStateState.Id;
                var id = _impunityLogListRepository.Save(splitLogList);
                //save Log
                var personnelCode =
                    _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addImpunityForCrimesLogDto.AuthorUserName));
                var requestSplitStateLog =
                    Mapper.Map<AddImpunityForCrimesLogDto, RequestImpunityForCrimesLog>(addImpunityForCrimesLogDto);
                requestSplitStateLog.Author = personnelCode;
                requestSplitStateLog.Created = DateTime.Now;
                requestSplitStateLog.AllowEdit = true;
                requestSplitStateLog.SharpointItemId = id;
                requestSplitStateLog.SetCustomerDelinquentId(addImpunityForCrimesLogDto.CustomerDelinquentId);
                await _logBaseRepository.SaveAsync(requestSplitStateLog);
            }
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<ImpunityForCrimesLogDto> GetImpunityAsync(GetImpunityLogDto ImpunityLogDto){
            var impunityForCrimesLog = await _logBaseRepository.OneAsync(ImpunityLogDto.RequestId);
            var log = (ImpunityForCrimesLog) impunityForCrimesLog;
            return new ImpunityForCrimesLogDto {AuthorUserName = log.Author, InterestRate = log.InterestRate,ImpunityAmount = log.ImpunityAmount};
        }
        public async Task<RequestImpunityForCrimesLogDto> GetRequestImpunityAsync(
            GetRequestImpunityLogDto ImpunityLogDto){
            var impunityForCrimesLog = await _logBaseRepository.OneAsync(ImpunityLogDto.RequestId);
            var log = (RequestImpunityForCrimesLog) impunityForCrimesLog;
            return new RequestImpunityForCrimesLogDto {AuthorUserName = log.Author, InterestRate = log.InterestRate,ImpunityAmount = log.ImpunityAmount};
        }
        public async Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editImpunityForCrimesLogDto){
            var impunityLog = Mapper.Map<EditImpunityForCrimesLogDto, ImpunityForCrimesLog>(editImpunityForCrimesLogDto);
            var personalCode =
                _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(editImpunityForCrimesLogDto.AuthorUserName));
            impunityLog.Created = DateTime.Now;
            impunityLog.Author = personalCode;
            impunityLog.CustomerDelinquentId = editImpunityForCrimesLogDto.CustomerDelinquentId;
            await _logBaseRepository.SaveAsync(impunityLog);
        }
        public async Task EditRequestImpunityForCrimesLogAsync(
            EditRequestImpunityForCrimesLogDto editImpunityForCrimesLogDto){
            var sharePointListItemId = 0;
            // update Database
            var Log = await _logBaseRepository.OneAsync(editImpunityForCrimesLogDto.Id);
            var requestLog = (RequestImpunityForCrimesLog) Log;
            requestLog.InterestRate = editImpunityForCrimesLogDto.InterestRate;
            requestLog.ImpunityAmount = editImpunityForCrimesLogDto.ImpunityAmount;
            sharePointListItemId = requestLog.SharpointItemId;
            await _logBaseRepository.SaveAsync(requestLog);

            // update sharepoint item 
            _impunityLogListRepository.Update(editImpunityForCrimesLogDto, sharePointListItemId);
        }
        public async Task RespondRequestImpunityForCrimesAsync(
            RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto){
            var customerDelinquent =
                await _customerDelinquentRepository.OneAsync(respondRequestImpunityForCrimesDto.CustomerDelinquentId);
            var requestSplitLog = await _logBaseRepository.GetRequestImpunityForCrimesLog(customerDelinquent.Id);
            requestSplitLog.Description = respondRequestImpunityForCrimesDto.Description;
            if (respondRequestImpunityForCrimesDto.Approve){
                requestSplitLog.IsApprove = true;
                var stateHandler = new ImpunityForCrimesStateHandler(requestSplitLog,
                    respondRequestImpunityForCrimesDto.RespondUserName);
                customerDelinquent.SetState(stateHandler.Id);
                var notification = _notificationFactory.Create("قبول درخواست",
                    "با درخواست بخشودگی جرائم مشتری موافقت شد", customerDelinquent,
                    NotificationType.ApproveRequestImpunityForCrimes);
                await _notificationRepository.SaveAsync(notification);
            }
            else{
                requestSplitLog.IsApprove = false;
                var notification = _notificationFactory.Create("رد درخواست",
                    "با درخواست بخشودگی جرائم مشتری مخالفت شد ، اقدام قانونی را شروع کنید", customerDelinquent,
                    NotificationType.RejectRequestImpunityForCrimes);
                await _notificationRepository.SaveAsync(notification);
            }
            await _logBaseRepository.SaveAsync(requestSplitLog);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<bool> CheckPrivilegeAddImpunityLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addImpunityForCrimesLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, addImpunityForCrimesLogDto);
        }
        public async Task<bool> CheckPrivilegeEditImpunityLogAsync(
            EditImpunityForCrimesLogDto editImpunityForCrimesLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(editImpunityForCrimesLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, editImpunityForCrimesLogDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(
            GetRequestImpunityLogDto getRequestImpunityLogDto){
            return await _logPrivilegeService.IsValidPrivilege(getRequestImpunityLogDto);
        }
        public async Task DisableImpunityEditingDto(DisableImpunityEditingDto disableImpunityEditingDto){
            var requestLog =
                await _logBaseRepository.GetRequestImpunityForCrimesLog(disableImpunityEditingDto.CustomerDelinquentId);
            requestLog.AllowEdit = false;
            await _logBaseRepository.SaveAsync(requestLog);
        }
    }
}