using System;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class ClearingService:IClearingService{
        private readonly ICryptographer _cryptographer;
        private readonly IHrFacade _hrFacade;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly IClearingLogListRepository _clearingLogListRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly ILogPrivilegeService _logPrivilegeService;
        public ClearingService(ICryptographer cryptographer, IHrFacade hrFacade, ICustomerDelinquentRepository customerDelinquentRepository, IBranchPrivilegesService branchPrivilegesService, IClearingLogListRepository clearingLogListRepository, ILogBaseRepository logBaseRepository, INotificationRepository notificationRepository, INotificationFactory notificationFactory, ILogPrivilegeService logPrivilegeService){
            _cryptographer = cryptographer;
            _hrFacade = hrFacade;
            _customerDelinquentRepository = customerDelinquentRepository;
            _branchPrivilegesService = branchPrivilegesService;
            _clearingLogListRepository = clearingLogListRepository;
            _logBaseRepository = logBaseRepository;
            _notificationRepository = notificationRepository;
            _notificationFactory = notificationFactory;
            _logPrivilegeService = logPrivilegeService;
        }
        public async Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto)
        {
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addClearingLogDto.AuthorUserName));
            var customerDelinquent = await
          _customerDelinquentRepository.GetCustomerDelinquentWithState(addClearingLogDto.CustomerDelinquentId);
            if (await _branchPrivilegesService.IsValidPrivilege(branchCode, addClearingLogDto))
            {
                var state = new ClearingStateHandler(addClearingLogDto);
                customerDelinquent.SetState(state.Id);
            }
            else
            {
                // save sharepoint item
                var requestSplitStateState = new RequestClearingStateHandler(addClearingLogDto);  
                customerDelinquent.SetState(requestSplitStateState.Id);
                var splitLogList = Mapper.Map<AddClearingLogDto, ClearingLogList>(addClearingLogDto);
                splitLogList.RequestStateHandlerId = requestSplitStateState.Id;
                var id = _clearingLogListRepository.Save(splitLogList);
                //save Log
                var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addClearingLogDto.AuthorUserName));
                var requestSplitStateLog = Mapper.Map<AddClearingLogDto, RequestClearingLog>(addClearingLogDto);
                requestSplitStateLog.Author = personnelCode;
                requestSplitStateLog.Created = DateTime.Now;
                requestSplitStateLog.AllowEdit = true;
                requestSplitStateLog.SharpointItemId = id;
                requestSplitStateLog.SetCustomerDelinquentId(addClearingLogDto.CustomerDelinquentId);
                await _logBaseRepository.SaveAsync(requestSplitStateLog);
            }
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<ClearingLogDto> GetClearingAsync(GetClearingLogDto clearingLogDto) {
            var clearingLog = await _logBaseRepository.OneAsync(clearingLogDto.ClearingRequestId);
            var log = (ClearingLog) clearingLog;
            return new ClearingLogDto {AuthorUserName = log.Author,LegislationDate = log.LegislationDate};
        }

        public async Task<RequestClearingLogDto> GetRequestClearingAsync(GetRequestClearingLogDto getRequestClearingLogDto) {
            var clearingLog = await _logBaseRepository.OneAsync(getRequestClearingLogDto.RequestId);
            var log = (RequestClearingLog)clearingLog;
            return new RequestClearingLogDto { TheAmountOfAssessment = log.TheAmountOfAssessment, LegislationDate = log.LegislationDate, InterestRate = log.InterestRate };        
        }

        public async Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto)
        {
            var customerDelinquent =
                   await _customerDelinquentRepository.OneAsync(respondRequestClearingDto.CustomerDelinquentId);
            var requestClearingLog = await _logBaseRepository.GetRequestClearingLog(customerDelinquent.Id);
            requestClearingLog.Description = respondRequestClearingDto.Description;
            if (respondRequestClearingDto.Approve)
            {
                requestClearingLog.IsApprove = true;
                var splitStateHandler = new ClearingStateHandler(requestClearingLog, respondRequestClearingDto.RespondUserName);
                customerDelinquent.SetState(splitStateHandler.Id);
                var notification = _notificationFactory.Create("قبول درخواست", "با درخواست تهاتر مشتری موافقت شد", customerDelinquent, NotificationType.ApproveRequestClearing);
                await _notificationRepository.SaveAsync(notification);
            }
            else
            {
                requestClearingLog.IsApprove = false;
                var notification = _notificationFactory.Create("رد درخواست", "با درخواست تهاتر مشتری مخالفت شد ، اقدام قانونی را شروع کنید", customerDelinquent, NotificationType.RejectRequestClearing);
                await _notificationRepository.SaveAsync(notification);
            }
            await _logBaseRepository.SaveAsync(requestClearingLog);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }

        public async Task EditClearingAsync(EditClearingLogDto editClearingLogDto){
            var clearingLog =Mapper.Map<EditClearingLogDto, ClearingLog>(editClearingLogDto);
            var personalCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(editClearingLogDto.AuthorUserName));
            clearingLog.Created = DateTime.Now;
            clearingLog.Author = personalCode;
            clearingLog.CustomerDelinquentId = editClearingLogDto.CustomerDelinquentId;
            await _logBaseRepository.SaveAsync(clearingLog);
        }
        
        public async Task EditRequestClearingAsync(EditRequestClearingLogDto editRequestClearingLogDto)
        {
            var sharePointListItemId = 0;
            // update Database

            var Log = await _logBaseRepository.OneAsync(editRequestClearingLogDto.Id);
            var requestLog = (RequestClearingLog)Log;
            requestLog.LegislationDate = editRequestClearingLogDto.LegislationDate;
            sharePointListItemId = requestLog.SharpointItemId;
            await _logBaseRepository.SaveAsync(requestLog);

            // update sharepoint item 

            _clearingLogListRepository.Update(editRequestClearingLogDto, sharePointListItemId);
        }
        public Task RemoveRequestClearingAsync(RemoveClearingLogDto removeClearingLogDto){
            throw new NotImplementedException();
        }
        public async Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addClearingLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, addClearingLogDto);
        }
        public async Task<bool> CheckPrivilegeEditClearingLogAsync(EditClearingLogDto editClearingLogDto)
        {
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(editClearingLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, editClearingLogDto);
        }

        public async Task<bool> CheckPrivilegeEditRequestClearingLogAsync(GetRequestClearingLogDto getRequestClearingLogDto)
        {

            return await _logPrivilegeService.IsValidPrivilege(getRequestClearingLogDto);
        }

        public async Task DisableClearingEditingDto(DisableClearingEditingDto disableClearingEditingDto)
        {
            var requestLog = await _logBaseRepository.GetRequestClearingLog(disableClearingEditingDto.CustomerDelinquentId);
            requestLog.AllowEdit = false;
            await _logBaseRepository.SaveAsync(requestLog);
        }

    }
}