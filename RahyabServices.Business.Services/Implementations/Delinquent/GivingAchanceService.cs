using System;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class GivingAchanceService : IGivingAChanceService{
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly ICryptographer _cryptographer;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IGivingAChanceLogListRepository _givingAChanceLogListRepository;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ILogPrivilegeService _logPrivilegeService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        public GivingAchanceService(ILogBaseRepository logBaseRepository, IHrFacade hrFacade,
            ICustomerDelinquentRepository customerDelinquentRepository, INotificationRepository notificationRepository,
            ICryptographer cryptographer, IBranchPrivilegesService branchPrivilegesService, INotificationFactory notificationFactory,
            ILogPrivilegeService logPrivilegeService, IGivingAChanceLogListRepository givingAChanceLogListRepository){
            _logBaseRepository = logBaseRepository;
            _hrFacade = hrFacade;
            _customerDelinquentRepository = customerDelinquentRepository;
            _notificationRepository = notificationRepository;
            _cryptographer = cryptographer;
            _branchPrivilegesService = branchPrivilegesService;
            _notificationFactory = notificationFactory;
            _logPrivilegeService = logPrivilegeService;
            _givingAChanceLogListRepository = givingAChanceLogListRepository;
        }
        public async Task<GivingAChanceLogDto> GetGivingAChanceAsync(GetGivingAChanceLogDto givingAChanceLogDto){
            var givingAChanceLog = await _logBaseRepository.OneAsync(givingAChanceLogDto.RequestId);
            var log = (GivingAChanceLog) givingAChanceLog;
            return new GivingAChanceLogDto
            {
                ExpireDate = log.ExpireDate,
                LegislationDate = log.LegislationDate,
                DepositAmount = log.DepositAmount,
                Count = log.Count,
                AuthorUserName = log.Author
            };
        }
        public async Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceAsync(
            GetRequestGivingAChanceLogDto givingAChanceLogDto){
            var givingAChanceLog = await _logBaseRepository.OneAsync(givingAChanceLogDto.RequestId);
            var log = (RequestGivingAChanceLog) givingAChanceLog;
            return new RequestGivingAChanceLogDto
            {
                ExpireDate = log.ExpireDate,
                LegislationDate = log.LegislationDate,
                Count = log.Count,
                AuthorUserName = log.Author,
                DepositAmount = log.DepositAmount
            };
        }
        public async Task<bool> CheckPrivilegeAddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addGivingAChanceLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, addGivingAChanceLogDto);
        }
        public async Task<bool> CheckPrivilegeEditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(editGivingAChanceLogDto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, editGivingAChanceLogDto);
        }
        public async Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(addGivingAChanceLogDto.AuthorUserName));
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(addGivingAChanceLogDto.CustomerDelinquentId);
            if (await _branchPrivilegesService.IsValidPrivilege(branchCode, addGivingAChanceLogDto)){
                var state = new GivingAChanceStateHandler(addGivingAChanceLogDto);
                customerDelinquent.SetState(state.Id);
            }
            else{
                // save sharepoint item
                var requestSplitStateState = new RequestGivingAChanceStateHandler(addGivingAChanceLogDto);
                customerDelinquent.SetState(requestSplitStateState.Id);
                var splitLogList = Mapper.Map<AddGivingAChanceLogDto, GivingAChanceLogList>(addGivingAChanceLogDto);
                splitLogList.RequestStateHandlerId = requestSplitStateState.Id;
                var id = _givingAChanceLogListRepository.Save(splitLogList);
                //save Log
                var personnelCode =
                    _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addGivingAChanceLogDto.AuthorUserName));
                var requestSplitStateLog =
                    Mapper.Map<AddGivingAChanceLogDto, RequestGivingAChanceLog>(addGivingAChanceLogDto);
                requestSplitStateLog.Author = personnelCode;
                requestSplitStateLog.Created = DateTime.Now;
                requestSplitStateLog.AllowEdit = true;
                requestSplitStateLog.SharpointItemId = id;
                requestSplitStateLog.SetCustomerDelinquentId(addGivingAChanceLogDto.CustomerDelinquentId);
                await _logBaseRepository.SaveAsync(requestSplitStateLog);
            }
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto){
            var givingAChanceLog = Mapper.Map<EditGivingAChanceLogDto, GivingAChanceLog>(editGivingAChanceLogDto);
            var personalCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(editGivingAChanceLogDto.AuthorUserName));
            givingAChanceLog.Created = DateTime.Now;
            givingAChanceLog.Author = personalCode;
            givingAChanceLog.CustomerDelinquentId = editGivingAChanceLogDto.CustomerDelinquentId;
            await _logBaseRepository.SaveAsync(givingAChanceLog);
        }
        public async Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editGivingAChanceLogDto){
            var sharePointListItemId = 0;
            // update Database
            var Log = await _logBaseRepository.OneAsync(editGivingAChanceLogDto.Id);
            var requestLog = (RequestGivingAChanceLog) Log;
            requestLog.LegislationDate = editGivingAChanceLogDto.LegislationDate;
            requestLog.ExpireDate = editGivingAChanceLogDto.ExpireDate;
            requestLog.DepositAmount = editGivingAChanceLogDto.DepositAmount;
            requestLog.Count = editGivingAChanceLogDto.Count;
            sharePointListItemId = requestLog.SharpointItemId;
            await _logBaseRepository.SaveAsync(requestLog);

            // update sharepoint item 
            _givingAChanceLogListRepository.Update(editGivingAChanceLogDto, sharePointListItemId);
        }
        public async Task RespondGivingAChanceAsync(RespondRequestGivingAChanceDto respondRequestGivingAChanceDto){
            var customerDelinquent =
                await _customerDelinquentRepository.OneAsync(respondRequestGivingAChanceDto.CustomerDelinquentId);
            var requestGivingAChanceLog = await _logBaseRepository.GetRequestGivinAChanceLog(customerDelinquent.Id);
            requestGivingAChanceLog.Description = respondRequestGivingAChanceDto.Description;
            if (respondRequestGivingAChanceDto.Approve){
                requestGivingAChanceLog.IsApprove = true;
                var splitStateHandler = new GivingAChanceStateHandler(requestGivingAChanceLog,
                    respondRequestGivingAChanceDto.RespondUserName);
                customerDelinquent.SetState(splitStateHandler.Id);
                var notification = _notificationFactory.Create("قبول درخواست", "با درخواست امهال مشتری موافقت شد",
                    customerDelinquent, NotificationType.ApproveRequestGivingAChance);
                await _notificationRepository.SaveAsync(notification);
            }
            else{
                requestGivingAChanceLog.IsApprove = false;
                var notification = _notificationFactory.Create("رد درخواست",
                    "با درخواست امهال مشتری مخالفت شد ، اقدام قانونی را شروع کنید", customerDelinquent,
                    NotificationType.RejectRequestGivingAChance);
                await _notificationRepository.SaveAsync(notification);
            }
            await _logBaseRepository.SaveAsync(requestGivingAChanceLog);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<bool> CheckPrivilegeEditRequestGivingAchanceLogAsync(
            GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto){
            return await _logPrivilegeService.IsValidPrivilege(getRequestGivingAChanceLogDto);
        }
        public async Task DisableGivingAChanceEditingDto(DisableGivingAChanceEditingDto disableGivingAChanceEditingDto){
            var requestLog =
                await _logBaseRepository.GetRequestGivinAChanceLog(disableGivingAChanceEditingDto.CustomerDelinquentId);
            requestLog.AllowEdit = false;
            await _logBaseRepository.SaveAsync(requestLog);
        }
    }
}