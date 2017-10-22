using System;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class SplitService : ISplitService{
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly ICryptographer _cryptographer;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ILogPrivilegeService _logPrivilegeService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly ISplitLogListRepository _splitLogListRepository;
        private readonly IStateRepository _stateRepository;
        public SplitService(IHrFacade hrFacade, ICryptographer cryptographer,
            ICustomerDelinquentRepository customerDelinquentRepository, IBranchPrivilegesService branchPrivilegesService,
            ILogBaseRepository logBaseRepository, INotificationRepository notificationRepository,
            INotificationFactory notificationFactory, ISplitLogListRepository splitLogListRepository,
            IStateRepository stateRepository, ILogPrivilegeService logPrivilegeService){
            _hrFacade = hrFacade;
            _cryptographer = cryptographer;
            _customerDelinquentRepository = customerDelinquentRepository;
            _branchPrivilegesService = branchPrivilegesService;
            _logBaseRepository = logBaseRepository;
            _notificationRepository = notificationRepository;
            _notificationFactory = notificationFactory;
            _splitLogListRepository = splitLogListRepository;
            _stateRepository = stateRepository;
            _logPrivilegeService = logPrivilegeService;
        }
        public async Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addSplitLogDto.AuthorUserName));
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(addSplitLogDto.CustomerDelinquentId);
            if (await _branchPrivilegesService.IsValidPrivilege(branchCode, addSplitLogDto)){
                var splitStateState = new SplitStateHandler(addSplitLogDto);
                customerDelinquent.SetState(splitStateState.Id);
            }
            else{
                var requestSplitStateState = new RequestSplitStateHandler(addSplitLogDto);
                customerDelinquent.SetState(requestSplitStateState.Id);
                var splitLogList = Mapper.Map<AddSplitLogDto, SplitLogList>(addSplitLogDto);
                splitLogList.RequestStateHandlerId = requestSplitStateState.Id;
                var id = _splitLogListRepository.Save(splitLogList);
                //save Log
                var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addSplitLogDto.AuthorUserName));
                var requestSplitStateLog = Mapper.Map<AddSplitLogDto, RequestSplitLog>(addSplitLogDto);
                requestSplitStateLog.Author = personnelCode;
                requestSplitStateLog.Created = DateTime.Now;
                requestSplitStateLog.AllowEdit = true;
                requestSplitStateLog.SharpointItemId = id;
                requestSplitStateLog.SetCustomerDelinquentId(addSplitLogDto.CustomerDelinquentId);
                requestSplitStateLog.StartDate = addSplitLogDto.LegislationDate.AddMonths(addSplitLogDto.Count);
                await _logBaseRepository.SaveAsync(requestSplitStateLog);
            }
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addSplitLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, addSplitLogDto);
        }
        public async Task<bool> CheckPrivilegeEditSplitLogAsync(EditSplitLogDto editSplitLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(editSplitLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, editSplitLogDto);
        }
        public async Task RespondRequestSplitAsync(RespondRequestSplitDto respondRequestClearingDto){
            var customerDelinquent =
                await _customerDelinquentRepository.OneAsync(respondRequestClearingDto.CustomerDelinquentId);
            var requestSplitLog = await _logBaseRepository.GetRequestSplitLog(customerDelinquent.Id);
            requestSplitLog.Description = respondRequestClearingDto.Description;
            if (respondRequestClearingDto.Approve){
                requestSplitLog.IsApprove = true;
                var splitStateHandler = new SplitStateHandler(requestSplitLog, respondRequestClearingDto.RespondUserName);
                customerDelinquent.SetState(splitStateHandler.Id);
                var notification = _notificationFactory.Create("قبول درخواست", "با درخواست تقسیط مشتری موافقت شد",
                    customerDelinquent, NotificationType.ApproveRequestSplit);
                await _notificationRepository.SaveAsync(notification);
            }
            else{
                requestSplitLog.IsApprove = false;
                var notification = _notificationFactory.Create("رد درخواست",
                    "با درخواست تقسیط مشتری مخالفت شد ، اقدام قانونی را شروع کنید", customerDelinquent,
                    NotificationType.RejectRequestSplit);
                await _notificationRepository.SaveAsync(notification);
            }
            await _logBaseRepository.SaveAsync(requestSplitLog);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<SplitLogDto> GetSplitLogAsync(GetSplitLogDto getSplitLogDto){
            var splitLog = await _logBaseRepository.OneAsync(getSplitLogDto.RequestId);
            var log = (SplitLog) splitLog;
            return new SplitLogDto
            {
                InterestRate = log.InterestRate,
                LegislationDate = log.LegislationDate,
                Count = log.Count,
                StartDate = log.StartDate,
                ApplyLegislatinAfterDueDate = log.ApplyLegislatinAfterDueDate,
                BreakTime = log.BreakTime,
                DepositAmount = log.DepositAmount,
                RefundsType = log.RefundsType,
                AuthorUserName = log.Author               
            };
        }
        public async Task<RequestSplitLogDto> GetRequestSplitLogAsync(GetRequestSplitLogDto getSplitLogDto){
            var splitLog = await _logBaseRepository.OneAsync(getSplitLogDto.RequestId);
            var log = (RequestSplitLog) splitLog;
            return new RequestSplitLogDto
            {
                InterestRate = log.InterestRate,
                LegislationDate = log.LegislationDate,
                Count = log.Count,
                ExpireDate = log.StartDate,
                AuthorUserName = log.Author
            };
        }
        public async Task EditSplitAsync(EditSplitLogDto editSplitLogDto){
            var splitLog = Mapper.Map<EditSplitLogDto, SplitLog>(editSplitLogDto);
            var personalCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(editSplitLogDto.AuthorUserName));
            splitLog.Created = DateTime.Now;
            splitLog.Author = personalCode;
            splitLog.CustomerDelinquentId = editSplitLogDto.CustomerDelinquentId;
            await _logBaseRepository.SaveAsync(splitLog);
        }
        public async Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto){
            var sharePointListItemId = 0;
            // update Database
            var Log = await _logBaseRepository.OneAsync(editRequestSplitLogDto.Id);
            var splitLog = (RequestSplitLog) Log;
            splitLog.LegislationDate = editRequestSplitLogDto.LegislationDate;
            splitLog.StartDate = editRequestSplitLogDto.StartDate;
            splitLog.InterestRate = editRequestSplitLogDto.InterestRate;
            splitLog.Count = editRequestSplitLogDto.Count;
            sharePointListItemId = splitLog.SharpointItemId;
            await _logBaseRepository.SaveAsync(splitLog);

            // update sharepoint item 
            _splitLogListRepository.Update(editRequestSplitLogDto, sharePointListItemId);
        }
        public async Task DisableSplitEditingDto(DisableSplitEditingDto disableSplitEditingDto){
            var requestLog = await _logBaseRepository.GetRequestSplitLog(disableSplitEditingDto.CustomerDelinquentId);
            requestLog.AllowEdit = false;
            await _logBaseRepository.SaveAsync(requestLog);
        }
        public async Task CancelRequestSplit(CancelRequestSplitDto cancelRequestSplitDto){
            await _logBaseRepository.RemoveAsync(cancelRequestSplitDto.RequestLogId);
            var customerDelinquent =
                await
                    _customerDelinquentRepository.GetCustomerDelinquentWithState(
                        cancelRequestSplitDto.CustomerDelinquentId);
            var stateId = customerDelinquent.CurrentState.Id;
            customerDelinquent.SetState(customerDelinquent.CurrentState.HistoryCustomerDelinquentId);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
            await _stateRepository.RemoveAsync(stateId);
        }
        public async Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto){
            return await _logPrivilegeService.IsValidPrivilege(getRequestSplitLogDto);
        }
    }
}