using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
using FluentValidation;
using RahyabServices.Business.Contracts;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Delinquent.BranchClaim;
using RahyabServices.Business.Dtos.Delinquent.Contracts;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Dtos.Delinquent.Customer;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Appointment;
using RahyabServices.Business.Dtos.Delinquent.Log.Call;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Notice;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice;
using RahyabServices.Business.Dtos.Delinquent.Notification;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
namespace RahyabServices.Business.Contracts.Implementations
{
    public class DelinquentServiceRestContract : ContractBase, IDelinquentServiceRestContract
    {
        private readonly IContractService _contractService;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly ILogService _logService;
        private readonly INotificationService _notificationService;
        private readonly IRPTFTDelinquentService _rptftDelinquentService;
        private readonly IClearingService _clearingService;
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly ISplitService _splitService;
        private readonly IGivingAChanceService _givingAChanceService;
        private readonly IImpunityService _impunityService;
        private readonly IBranchClaimService _branchClaimService;
        private readonly IBranchService _branchService;
        private readonly IRenewalService _renewalService;
        private readonly IDelinquentService _delinquentService;
        public DelinquentServiceRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, IContractService contractService, ICustomerAccountService customerAccountService,
            ILogService logService, IRPTFTDelinquentService rptftDelinquentService,
            INotificationService notificationService, IClearingService clearingService, ISharepointAuthorizationService sharepointAuthorizationService, IBranchPrivilegesService branchPrivilegesService, ISplitService splitService, IGivingAChanceService givingAChanceService, IImpunityService impunityService, IBranchClaimService branchClaimService, IBranchService branchService, IRenewalService renewalService, IDelinquentService delinquentService) :
            base(validatorFactory, cryptographer, logger, sharepointAuthorizationService)
        {
            _contractService = contractService;
            _customerAccountService = customerAccountService;
            _logService = logService;
            _rptftDelinquentService = rptftDelinquentService;
            _notificationService = notificationService;
            _clearingService = clearingService;
            _branchPrivilegesService = branchPrivilegesService;
            _splitService = splitService;
            _givingAChanceService = givingAChanceService;
            _impunityService = impunityService;
            _branchClaimService = branchClaimService;
            _branchService = branchService;
            _renewalService = renewalService;
            _delinquentService = delinquentService;
        }

        #region RequestBranch

        #region Split

        public async Task SetAllowEditToFalseForClearingLogAsync(DisableClearingEditingDto disableClearingEditingDto){
            await
         ValidateThenExecuteFaultHandledOperation<DisableClearingEditingDto>(
             async () => await _clearingService.DisableClearingEditingDto(disableClearingEditingDto),
             disableClearingEditingDto);
        }
        public async Task<SplitLogDto> GetSplitLogAsync(string requestId, string userName)
        {
            var splitLogDto = new GetSplitLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<SplitLogDto, GetSplitLogDto>(
                             async () => await _splitService.GetSplitLogAsync(splitLogDto), splitLogDto);
        }

        public async Task<RequestSplitLogDto> GetRequestSplitLogAsync(string requestId, string userName)
        {
            var splitLogDto = new GetRequestSplitLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<RequestSplitLogDto, GetRequestSplitLogDto>(
                             async () => await _splitService.GetRequestSplitLogAsync(splitLogDto), splitLogDto);
        }

        public async Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddSplitLogDto>(
                    async () => await _splitService.AddSplitLogAsync(addSplitLogDto), addSplitLogDto);
        }
        public async Task EditSplitLogAsync(EditSplitLogDto editSplitLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditSplitLogDto>(
                    async () => await _splitService.EditSplitAsync(editSplitLogDto), editSplitLogDto);
        }

        public async Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestSplitLogDto>(
                    async () => await _splitService.EditRequestSplitLogAsync(editRequestSplitLogDto), editRequestSplitLogDto);
        }

        public async Task RespondRequestSplitAsyncTask(RespondRequestSplitDto respondRequestSplitDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<RespondRequestSplitDto>(
                    async () => await _splitService.RespondRequestSplitAsync(respondRequestSplitDto),
                    respondRequestSplitDto);
        }
        public async Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool,AddSplitLogDto>(
                    async () => await _splitService.CheckPrivilegeAddSplitLogAsync(addSplitLogDto), addSplitLogDto);
        }
        public async Task SetAllowEditToFalseForSplitLogAsync(DisableSplitEditingDto disableSplitEditingDto){
            await
          ValidateThenExecuteFaultHandledOperation<DisableSplitEditingDto>(
              async () => await _splitService.DisableSplitEditingDto(disableSplitEditingDto),
              disableSplitEditingDto);
        }
        public async Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(string userName, string fromPersianDate, string toPersianDate){
            var getAllBranchActivityDtq = new GetAllBranchActivityDtq
            {
                UserName = userName,
                FromPersianDate = fromPersianDate,
                ToPersianDate = toPersianDate
            };
            return await
              ValidateThenExecuteFaultHandledOperation<IEnumerable<AllBranchActivityDto>, GetAllBranchActivityDtq>(
                  async () => await _contractService.GetAllBranchActivityAsync(getAllBranchActivityDtq), getAllBranchActivityDtq);
        }
        public async Task<string> GetLastAbisUpdateDateAsync(string userName)
        {
            var getLastUpdateDateDto = new GetLastUpdateDateDto{UserName = userName};
            return await
     ValidateThenExecuteFaultHandledOperation<string, GetLastUpdateDateDto>(
         async () => await _contractService.GetLastAbisUpdateDateAsync(getLastUpdateDateDto),
         getLastUpdateDateDto);
        }
        public async Task<string> GetLastBankIranUpdateDateAsync(string userName)
        {
            var getLastUpdateDateDto = new GetLastUpdateDateDto { UserName = userName };
            return await
     ValidateThenExecuteFaultHandledOperation<string, GetLastUpdateDateDto>(
         async () => await _contractService.GetLastBankIranUpdateDateAsync(getLastUpdateDateDto),
         getLastUpdateDateDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto)
        {
            return await
                ValidateThenExecuteFaultHandledOperation<bool, GetRequestSplitLogDto>(
                    async () => await _splitService.CheckPrivilegeEditRequestSplitLogAsync(getRequestSplitLogDto), getRequestSplitLogDto);

        }

        #endregion
 
        #region AddWrittenNotice
  public async Task AddWrittenNoticeAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddWrittenNoticeLogDto>(
                    async () => await _logService.AddWrittenNoticeLogAsync(addWrittenNoticeLogDto), addWrittenNoticeLogDto);
        }
  public async Task EditWrittenNoticeAsync(EditWrittenNoticeLogDto dto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<EditWrittenNoticeLogDto>(
              async () => await _logService.EditWrittenNoticeLogAsync(dto), dto);
  }
  public async Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(string requestId, string userName)
  {
      var log = new GetWrittenNoticeLogDto
      {
          UserName = userName,
          RequestId = int.Parse(requestId)
      };
      return await
                   ValidateThenExecuteFaultHandledOperation<WrittenNoticeLogDto, GetWrittenNoticeLogDto>(
                       async () => await _logService.GetWrittenNoticeLogAsync(log), log);
  }
        #endregion

        #region Clearing
  public async Task<ClearingLogDto> GetClearingLogAsync(string clearingRequestId, string userName)
  {
      GetClearingLogDto clearingLogDto = new GetClearingLogDto {
          UserName = userName,
          ClearingRequestId = int.Parse(clearingRequestId)
      };
      return await
                   ValidateThenExecuteFaultHandledOperation<ClearingLogDto, GetClearingLogDto>(
                       async () => await _clearingService.GetClearingAsync(clearingLogDto), clearingLogDto);
  }

  public async Task<RequestClearingLogDto> GetRequestClearingLogAsync(string requestId, string userName)
  {
      GetRequestClearingLogDto requestClearingLogDto = new GetRequestClearingLogDto
      {
          UserName = userName,
          RequestId = int.Parse(requestId)
      };
      return await
                   ValidateThenExecuteFaultHandledOperation<RequestClearingLogDto, GetRequestClearingLogDto>(
                       async () => await _clearingService.GetRequestClearingAsync(requestClearingLogDto), requestClearingLogDto);
  }

  public async Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<AddClearingLogDto>(
              async () => await _clearingService.AddClearingLogAsync(addClearingLogDto), addClearingLogDto);
  }

  public async Task EditClearingLogAsync(EditClearingLogDto editClearingLogDto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<EditClearingLogDto>(
              async () => await _clearingService.EditClearingAsync(editClearingLogDto), editClearingLogDto);
  }

  public async Task EditRequestClearingLogAsync(EditRequestClearingLogDto editClearingLogDto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<EditRequestClearingLogDto>(
              async () => await _clearingService.EditRequestClearingAsync(editClearingLogDto), editClearingLogDto);
  }
        public async Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(string userName){
            var yesterdayLogDto = new GetYesterdayLogsDto{UserName = userName};
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<YesterdayLogDto>, GetYesterdayLogsDto>(
                   async () => await _logService.GetYesterDayLogsAsync(yesterdayLogDto), yesterdayLogDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(string userName, string persianDate)
        {
            var getCustomer = new GetCustomerDelinquentHistoryDto { UserName = userName,PersianDate = persianDate};
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetCustomerDelinquentHistoryDto>(
                   async () => await _contractService.GetCustomerDelinquentHistoryAsync(getCustomer), getCustomer);
        }
     
        public async Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto)
  {
      await
   ValidateThenExecuteFaultHandledOperation<RespondRequestClearingDto>(
       async () => await _clearingService.RespondRequestClearingAsync(respondRequestClearingDto),
       respondRequestClearingDto);
  }
  public async Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto)
  {
      return await
               ValidateThenExecuteFaultHandledOperation<bool, AddClearingLogDto>(
                   async () => await _clearingService.CheckPrivilegeAddClearingLogAsync(addClearingLogDto), addClearingLogDto);
  }

  public async Task<bool> CheckPrivilegeEditRequestClearingLogAsync(GetRequestClearingLogDto getRequestClearingLogDto)
  {
      return await
          ValidateThenExecuteFaultHandledOperation<bool, GetRequestClearingLogDto>(
              async () => await _clearingService.CheckPrivilegeEditRequestClearingLogAsync(getRequestClearingLogDto), getRequestClearingLogDto);

  }

  #endregion

  #region Renewal
  public async Task<RenewalLogDto> GetRenewalLogAsync(string renewalRequestId, string userName)
  {
      var clearingLogDto = new GetRenewalLogDto
      {
          UserName = userName,
          RequestId = int.Parse(renewalRequestId)
      };
      return await
                   ValidateThenExecuteFaultHandledOperation<RenewalLogDto, GetRenewalLogDto>(
                       async () => await _renewalService.GetRenewalLogAsync(clearingLogDto), clearingLogDto);
  }


  public async Task AddRenewalLogAsync(AddRenewalLogDto dto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<AddRenewalLogDto>(
              async () => await _renewalService.AddRenewalLogAsync(dto), dto);
  }

  public async Task EditRenewalLogAsync(EditRenewalLogDto dto)
  {
      await
          ValidateThenExecuteFaultHandledOperation<EditRenewalLogDto>(
              async () => await _renewalService.EditRenewalAsync(dto), dto);
  }
  public async Task<bool> CheckPrivilegeAddRenewalLogAsync(AddRenewalLogDto dto)
  {
      return await
               ValidateThenExecuteFaultHandledOperation<bool, AddRenewalLogDto>(
                   async () => await _renewalService.CheckPrivilegeAddRenewalLogAsync(dto), dto);
  }

  public async Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto)
  {
      return await
          ValidateThenExecuteFaultHandledOperation<bool, EditRenewalLogDto>(
              async () => await _renewalService.CheckPrivilegeEditRenewalLogAsync(dto), dto);

  }

   
  #endregion

  #region Impunity
  public async Task SetAllowEditToFalseForImpunityLogAsync(DisableImpunityEditingDto disableImpunityEditingDto)
          {
              await
            ValidateThenExecuteFaultHandledOperation<DisableImpunityEditingDto>(
                async () => await _impunityService.DisableImpunityEditingDto(disableImpunityEditingDto),
                disableImpunityEditingDto);
          }
        #endregion

#region GivingAChance
          public async Task SetAllowEditToFalseForGivingAChanceLogAsync(DisableGivingAChanceEditingDto disableGivingAChanceEditingDto)
          {
              await
            ValidateThenExecuteFaultHandledOperation<DisableGivingAChanceEditingDto>(
                async () => await _givingAChanceService.DisableGivingAChanceEditingDto(disableGivingAChanceEditingDto),
                disableGivingAChanceEditingDto);
          }
          public async Task<BranchClaimDto> GetBranchClaim(string userName, string branchId, string dateOnly)
          {
              var branchClaimDto = new GetBranchClaimDto{UserName = userName,BranchId =int.Parse(branchId),DateOnly =Convert.ToDateTime(dateOnly)};
            return await
                                  ValidateThenExecuteFaultHandledOperation<BranchClaimDto, GetBranchClaimDto>(
                                      async () => await _branchClaimService.GetBranchClaim(branchClaimDto),
                                      branchClaimDto);
        }
        public async Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(string userName, string branchId){
            var branchClaimsDto = new GetLastSevenDaysBranchClaimsDto { UserName = userName, BranchId =int.Parse( branchId) };
            return await
                                  ValidateThenExecuteFaultHandledOperation<IEnumerable<BranchClaimDto>, GetLastSevenDaysBranchClaimsDto>(
                                      async () => await _branchClaimService.GetLastSevenDaysBranchClaims(branchClaimsDto),
                                      branchClaimsDto);
        }

        #endregion
  public async Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(GetRequestImpunityLogDto getRequestImpunityLogDto)
  {
      return await
          ValidateThenExecuteFaultHandledOperation<bool, GetRequestImpunityLogDto>(
              async () => await _impunityService.CheckPrivilegeEditRequestImpunityLogAsync(getRequestImpunityLogDto), getRequestImpunityLogDto);

  }

  public async Task<bool> CheckPrivilegeEditRequestGivingAChanceLogAsync(GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto)
  {
      return await
          ValidateThenExecuteFaultHandledOperation<bool, GetRequestGivingAChanceLogDto>(
              async () => await _givingAChanceService.CheckPrivilegeEditRequestGivingAchanceLogAsync(getRequestGivingAChanceLogDto), getRequestGivingAChanceLogDto);

  }
        public async Task<BranchDto> GetBranchInformationAsync(string userName){
            var getBranchDto = new GetBranchDto{UserName = userName};
            return await
              ValidateThenExecuteFaultHandledOperation<BranchDto, GetBranchDto>(
                  async () => await _branchService.GetBranchInformationAsync(getBranchDto),
                  getBranchDto);
        }
        public async Task<IEnumerable<BranchDto>> GetAllBranch(string userName)
        {
            var getAllBranch = new GetAllBranchDto { UserName = userName };
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <IEnumerable<BranchDto>, GetAllBranchDto>(
                            async () => await _branchService.GetAllBranchesAsync(getAllBranch), getAllBranch
                            );
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(string userName)
        {
            var getContractsByUserNameDto = new GetContractsByUserNameDto { UserName = userName };
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <IEnumerable<CustomerDelinquentDto>, GetContractsByUserNameDto>(
                            async () => await _contractService.GetBranchContractsAsync(getContractsByUserNameDto),
                            getContractsByUserNameDto);
        }

        public async Task<CountAndSumDto> GetCountContractsAsync(string userName)
        {
            var getContractsCountDto = new GetContractsCountDto { UserName = userName };
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <CountAndSumDto, GetContractsCountDto>(
                            async () => await _contractService.GetBranchContractsCountAsync(getContractsCountDto),
                            getContractsCountDto);
        }
        public async Task<CustomerInformationDto> GetCustomerInformationAsync(string customerNumber, string userName)
        {
            var customerInformationDto = new GetCustomerInformationDto { CustomerNumber = customerNumber, UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CustomerInformationDto, GetCustomerInformationDto>(
                    async () => await _customerAccountService.GetCustomerInformationAsync(customerInformationDto),
                    customerInformationDto);
        }

        public async Task<IEnumerable<LogDto>> GetCustomerLogsAsync(string customerDelinquentId, string userName)
        {
            var customerLogsDto = new GetCustomerLogsDto { CustomerDelinquentId = int.Parse(customerDelinquentId), UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<LogDto>, GetCustomerLogsDto>(
                    async () => await _logService.GetLogsAsync(customerLogsDto), customerLogsDto);
        }
        public async Task<CallLogDto> GetCallLogAsync(string requestId, string userName)
        {
            var callLogDto = new GetCallLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<CallLogDto, GetCallLogDto>(
                             async () => await _logService.GetCallLogDto(callLogDto), callLogDto);
        }
        public async Task AddCallLogAsync(AddCallLogDto addCallLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddCallLogDto>(
                    async () => await _logService.AddCallLogAsync(addCallLogDto), addCallLogDto);
        }
        public async Task EditCallLogAsync(EditCallLogDto editCallLogDto)
        {
            await
               ValidateThenExecuteFaultHandledOperation<EditCallLogDto>(
                   async () => await _logService.EditCallLogAsync(editCallLogDto),
                   editCallLogDto);
        }
        public async Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddAppointmentLogDto>(
                    async () => await _logService.AddAppointmentLogAsync(addAppointmentLogDto), addAppointmentLogDto);
        }

        public async Task EditAppointmentLogAsync(EditAppointmentLogDto dto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditAppointmentLogDto>(
                    async () => await _logService.EditAppointmentLogAsync(dto), dto);
        }
        public async Task<AppointmentLogDto> GetAppointmentLogAsync(string requestId, string userName)
        {
            var log = new GetAppointmentLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<AppointmentLogDto, GetAppointmentLogDto>(
                             async () => await _logService.GetAppointmentLogAsync(log), log);
        }

        public async Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddNoticeLogDto>(
                    async () => await _logService.AddNoticeLogAsync(addNoticeLogDto), addNoticeLogDto);
        }
        public async Task EditNoticeLogAsync(EditNoticeLogDto dto){
            await ValidateThenExecuteFaultHandledOperation<EditNoticeLogDto>(async ()=>await _logService.EditNoticeLogAsync(dto),dto);
        }

        public async Task<NoticeLogDto> GetNoticeLogAsync(string requestId, string userName){
            var notice = new GetNoticeLogDto {UserName = userName, RequestId =int.Parse(requestId)};
            return
                await
                    ValidateThenExecuteFaultHandledOperation<NoticeLogDto, GetNoticeLogDto>(
                        async () => await _logService.GetNoticeLogAsync(notice), notice);
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountAsync(string userName)
        {
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetCurrentContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(string userName){
            var getCurrentContractsDto = new GetCurrentContractsDto { UserName = userName };
            return await
        ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetCurrentContractsDto>(
            async () => await _contractService.GetCurrentContractsAsync(getCurrentContractsDto),
            getCurrentContractsDto);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountAsync(string userName)
        {
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetDueDateContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetOneMonthToDueDateContractsCountAsync(string userName)
        {
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetOneMonthToDueDateCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetBaddebtContractsCountAsync(string userName){
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetBadDebtContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountAsync(string userName){
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetPostponedContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }

        public async Task<CountAndSumDto> GetAllDebtsCountAsync(string userName)
        {
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetAllDebtsCountAsync(contractsCountDto),
                    contractsCountDto);
        }

         public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(string userName){
            var getOneMonthToDueDateContractsDto = new GetOneMonthToDueDateContractsDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetOneMonthToDueDateContractsDto>(
                    async () => await _contractService.GetOneMonthToDueDateContractsAsync(getOneMonthToDueDateContractsDto),
                    getOneMonthToDueDateContractsDto);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountAsync(string userName)
        {
            var contractsCountDto = new GetContractsCountDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetExpireContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }

        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(string userName)
        {
            var getDueDateContractsDto = new GetDueDateContractsDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetDueDateContractsDto>(
                    async () => await _contractService.GetDueDateContractsAsync(getDueDateContractsDto),
                    getDueDateContractsDto);
        }

        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(string userName)
        {
            var getExpireContracts = new GetExpireContractsDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetExpireContractsDto>(
                    async () => await _contractService.GetExpireContractsAsync(getExpireContracts),
                    getExpireContracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(string userName){
            var getBadDebt = new GetBadDebtDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetBadDebtDto>(
                    async () => await _contractService.GetBadDebtContractsAsync(getBadDebt),
                    getBadDebt);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(string userName){
            var getPostponde = new GetPostponedDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetPostponedDto>(
                    async () => await _contractService.GetPostponedContractsAsync(getPostponde),
                    getPostponde);
        }

        public async Task<bool> CheckPrivilegeAddGivingAChanceLogAsync(AddGivingAChanceLogDto givingAChanceLogDto)
        {
            return await
                     ValidateThenExecuteFaultHandledOperation<bool, AddGivingAChanceLogDto>(
                         async () => await _givingAChanceService.CheckPrivilegeAddGivingAChanceLogAsync(givingAChanceLogDto), givingAChanceLogDto);
        }

        public async Task<GivingAChanceLogDto> GetGivingAChanceLogAsync(string requestId, string userName)
        {
            var givingAChanceLogDto = new GetGivingAChanceLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<GivingAChanceLogDto, GetGivingAChanceLogDto>(
                             async () => await _givingAChanceService.GetGivingAChanceAsync(givingAChanceLogDto), givingAChanceLogDto);
        }

        public async Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceLogAsync(string requestId, string userName)
        {
            GetRequestGivingAChanceLogDto givingAChanceLogDto = new GetRequestGivingAChanceLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<RequestGivingAChanceLogDto, GetRequestGivingAChanceLogDto>(
                             async () => await _givingAChanceService.GetRequestGivingAChanceAsync(givingAChanceLogDto), givingAChanceLogDto);
        }

        public async Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddGivingAChanceLogDto>(
                    async () => await _givingAChanceService.AddGivingAChanceLogAsync(addGivingAChanceLogDto),
                    addGivingAChanceLogDto);
        }

        public async Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditGivingAChanceLogDto>(
                    async () => await _givingAChanceService.EditGivingAChanceLogAsync(editGivingAChanceLogDto),
                    editGivingAChanceLogDto);
        }
        public async Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editRequestGivingAChanceLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestGivingAChanceLogDto>(
                    async () => await _givingAChanceService.EditRequestGivingAChanceLogAsync(editRequestGivingAChanceLogDto),
                    editRequestGivingAChanceLogDto);
        }

        public async Task<bool> CheckPrivilegeAddImpunityLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto)
        {
            return await
                     ValidateThenExecuteFaultHandledOperation<bool, AddImpunityForCrimesLogDto>(
                         async () => await _impunityService.CheckPrivilegeAddImpunityLogAsync(addImpunityForCrimesLogDto), addImpunityForCrimesLogDto);
        }
        public async Task<ImpunityForCrimesLogDto> GetImpunityLogAsync(string requestId, string userName)
        {
            GetImpunityLogDto impunityLogDto = new GetImpunityLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<ImpunityForCrimesLogDto, GetImpunityLogDto>(
                             async () => await _impunityService.GetImpunityAsync(impunityLogDto), impunityLogDto);
        }

        public async Task<RequestImpunityForCrimesLogDto> GetRequestImpunityLogAsync(string requestId, string userName)
        {
            GetRequestImpunityLogDto impunityLogDto = new GetRequestImpunityLogDto
            {
                UserName = userName,
                RequestId = int.Parse(requestId)
            };
            return await
                         ValidateThenExecuteFaultHandledOperation<RequestImpunityForCrimesLogDto, GetRequestImpunityLogDto>(
                             async () => await _impunityService.GetRequestImpunityAsync(impunityLogDto), impunityLogDto);
        }

        public async Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<AddImpunityForCrimesLogDto>(
                    async () => await _impunityService.AddImpunityForCrimesLogAsync(addImpunityForCrimesLogDto),
                    addImpunityForCrimesLogDto);
        }

        public async Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editImpunityForCrimesLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditImpunityForCrimesLogDto>(
                    async () => await _impunityService.EditImpunityForCrimesLogAsync(editImpunityForCrimesLogDto),
                    editImpunityForCrimesLogDto);
        }

        public async Task EditRequestImpunityForCrimesLogAsync(EditRequestImpunityForCrimesLogDto editImpunityForCrimesLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestImpunityForCrimesLogDto>(
                    async () => await _impunityService.EditRequestImpunityForCrimesLogAsync(editImpunityForCrimesLogDto),
                    editImpunityForCrimesLogDto);
        }

        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(string customerDelinquentId, string userName)
        {
            var getGuarantorsDto = new GetGuarantorsDto { CustomerDelinquentId = int.Parse(customerDelinquentId), UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<GuarantorsDto>, GetGuarantorsDto>(
                    async () => await _rptftDelinquentService.GetGuarantorsAsync(getGuarantorsDto),
                    getGuarantorsDto);
        }
        public async Task<IEnumerable<BondDto>> GetBondsAsync(string customerDelinquentId, string userName)
        {
            var getBondsDto = new GetBondsDto { CustomerDelinquentId = int.Parse(customerDelinquentId), UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<BondDto>, GetBondsDto>(
                    async () => await _rptftDelinquentService.GetBondsAsync(getBondsDto),
                    getBondsDto);
        }
        public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(string userName)
        {
            var getNotifications = new GetNotificationsDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<NotificationDto>, GetNotificationsDto>(
                    async () => await _notificationService.GetNotifications(getNotifications),
                    getNotifications);
        }
        public async Task UpdateNotificationToSeenAsync(UpdateNotificationToSeenDto updateNotificationToSeenDto)
        {
            await
           ValidateThenExecuteFaultHandledOperation<UpdateNotificationToSeenDto>(
               async () => await _notificationService.UpdateToSeen(updateNotificationToSeenDto),
               updateNotificationToSeenDto);
        }
        public async Task RemoveNotificationAsync(RemoveNotificationDto removeNotification)
        {
            await
           ValidateThenExecuteFaultHandledOperation<RemoveNotificationDto>(
               async () => await _notificationService.RemoveNotification(removeNotification),
               removeNotification);
        }
     
        public async Task RespondRequestGivingAChanceAsync(RespondRequestGivingAChanceDto respondRequestGivingAChanceDto){
            await
          ValidateThenExecuteFaultHandledOperation<RespondRequestGivingAChanceDto>(
              async () => await _givingAChanceService.RespondGivingAChanceAsync(respondRequestGivingAChanceDto),
              respondRequestGivingAChanceDto);
        }
        public async Task RespondRequestImpunityForCrimesAsync(RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto){
            await
                 ValidateThenExecuteFaultHandledOperation<RespondRequestImpunityForCrimesDto>(
                     async () => await _impunityService.RespondRequestImpunityForCrimesAsync(respondRequestImpunityForCrimesDto),
                     respondRequestImpunityForCrimesDto);
        }
       
        #endregion

      
        #region ManagerRequest
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(string userName, string persianDate, string branchCode)
        {
            var getCustomer = new GetCustomerDelinquentHistoryByBranchCodeDto {BranchCode = branchCode,PersianDate = persianDate,UserName = userName};
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetCustomerDelinquentHistoryByBranchCodeDto>(
                    async () => await _contractService.GetCustomerDelinquentHistoryByBranchCodeAsync(getCustomer),
                    getCustomer);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(string userName, string persianDate)
        {
            var getAllCustomerDelinquentsHistoryDto = new GetAllCustomerDelinquentsHistoryDto{UserName = userName,PersianDate = persianDate};
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetAllCustomerDelinquentsHistoryDto>(
                    async () => await _contractService.GetAllCustomerDelinquentsHistoryAsync(getAllCustomerDelinquentsHistoryDto),
                    getAllCustomerDelinquentsHistoryDto);
        }
        public async Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquent(string userName, string branchCode, string persianFromDate, string persianToDate){
            var branchDelinquentDtq = new GetBranchDelinquentDtq {PersianToDate = persianToDate,BranchCode = branchCode,PersianFromDate = persianFromDate,UserName = userName};
            return await
            ValidateThenExecuteFaultHandledOperation<IEnumerable<BranchDelinquentDto>, GetBranchDelinquentDtq>(
                async () => await _delinquentService.GetBranchDelinquent(branchDelinquentDtq),
                branchDelinquentDtq);
        }
        public async Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquent(string userName, string persianFromDate, string persianToDate){
            var branchesDelinquentDtq= new GetBranchesDelinquentDtq {PersianFromDate = persianFromDate,PersianToDate = persianToDate,UserName = userName};
            return await
                       ValidateThenExecuteFaultHandledOperation<IEnumerable<BranchesDelinquentDto>, GetBranchesDelinquentDtq>(
                           async () => await _delinquentService.GetBranchesDelinquent(branchesDelinquentDtq),
                           branchesDelinquentDtq);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getContractsByBranchCodeDto = new GetContractsByBranchCodeDto{BranchCode = branchCode,UserName = userName};
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetContractsByBranchCodeDto>(
                    async () => await _contractService.GetBranchContractsByBranchCodeAsync(getContractsByBranchCodeDto),
                    getContractsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetCountContractsByBranchCodeAsync(
           string userName, string branchCode)
        {
            var contractsCountDto = new GetContractsCountByBranchCodeDto{BranchCode = branchCode,UserName = userName};
            return
               await
                   ValidateThenExecuteFaultHandledOperation
                       <CountAndSumDto, GetContractsCountByBranchCodeDto>(
                           async () => await _contractService.GetBranchContractsCountByBranchCodeAsync(contractsCountDto),
                           contractsCountDto);
        }
        public async Task<IEnumerable<LogDto>> GetCustomerLogsByBranchCodeAsync(string userName, string branchCode,string customerDelinquentId)
        {
            var getCustomerLogsByBranchCodeDto = new GetCustomerLogsByBranchCodeDto { BranchCode = branchCode, UserName = userName,CustomerDelinquentId =int.Parse( customerDelinquentId)};
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<LogDto>, GetCustomerLogsByBranchCodeDto>(
                   async () => await _logService.GetLogsByBranchCodeAsync(getCustomerLogsByBranchCodeDto),
                   getCustomerLogsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(
           string userName, string branchCode)
        {
            var contractsCountByBranchCodeDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
              ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                  async () => await _contractService.GetCurrentContractsCountByBranchCodeAsync(contractsCountByBranchCodeDto),
                  contractsCountByBranchCodeDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(
           string userName, string branchCode)
        {
            var getCurrentContractsDto = new GetCurrentContractsByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                 ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetCurrentContractsByBranchCodeDto>(
                     async () => await _contractService.GetCurrentContractsByBranchCodeAsync(getCurrentContractsDto),
                     getCurrentContractsDto);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(
            string userName, string branchCode)
        {
            var contractsCountDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                    ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                        async () => await _contractService.GetDueDateContractsCountByBranchCodeAsync(contractsCountDto),
                        contractsCountDto);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(
             string userName, string branchCode)
        {
            var contractsCountDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                       ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                           async () => await _contractService.GetExpireContractsCountByBranchCodeAsync(contractsCountDto),
                           contractsCountDto);
        }
        public async Task<CountAndSumDto> GetBaddebtContractsCountByBranchCodeAsync(string userName, string branchCode)
        {
            var contractsCountDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                          ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                              async () => await _contractService.GetBadDebtContractsCountByBranchCodeAsync(contractsCountDto),
                              contractsCountDto);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(string userName, string branchCode)
        {
            var contractsCountDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                           ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                               async () => await _contractService.GetPostponedContractsCountByBranchCodeAsync(contractsCountDto),
                               contractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getBadDebtDto = new GetBadDebtByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                         ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetBadDebtByBranchCodeDto>(
                             async () => await _contractService.GetBadDebtContractsByBranchCodeAsync(getBadDebtDto),
                             getBadDebtDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getPostponedDto = new GetPostponedByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                             ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetPostponedByBranchCodeDto>(
                                 async () => await _contractService.GetPostponedContractsByBranchCodeAsync(getPostponedDto),
                                 getPostponedDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getDueDateContractsDto = new GetDueDateContractsByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                                  ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetDueDateContractsByBranchCodeDto>(
                                      async () => await _contractService.GetDueDateContractsByBranchCodeAsync(getDueDateContractsDto),
                                      getDueDateContractsDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getExpireContracts = new GetExpireContractsByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                                      ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetExpireContractsByBranchCodeDto>(
                                          async () => await _contractService.GetExpireContractsByBranchCodeAsync(getExpireContracts),
                                          getExpireContracts);
        }
        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(string userName, string branchCode, string customerDelinquentId)
        {
            var getGuarantorsByBranchCodeDto = new GetGuarantorsByBranchCodeDto { BranchCode = branchCode, UserName = userName ,CustomerDelinquentId =int.Parse(customerDelinquentId) };
            return await
                                         ValidateThenExecuteFaultHandledOperation<IEnumerable<GuarantorsDto>, GetGuarantorsByBranchCodeDto>(
                                             async () => await _rptftDelinquentService.GetGuarantorsByBranchCodeAsync(getGuarantorsByBranchCodeDto),
                                             getGuarantorsByBranchCodeDto);
        }
        public async Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(string userName, string branchCode, string customerDelinquentId)
        {
            var getBondsByBranchCodeDto = new GetBondsByBranchCodeDto { BranchCode = branchCode, UserName = userName, CustomerDelinquentId = int.Parse(customerDelinquentId) };
            return await
                                          ValidateThenExecuteFaultHandledOperation<IEnumerable<BondDto>, GetBondsByBranchCodeDto>(
                                              async () => await _rptftDelinquentService.GetBondsByBranchCodeAsync(getBondsByBranchCodeDto),
                                              getBondsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetOneMonthToDueDateContractsCountByBranchCodeAsync(string userName, string branchCode)
        {
            var getContractsCountDto = new GetContractsCountByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                                    ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                                        async () => await _contractService.GetOneMonthToDueDateCountByBranchCodeAsync(getContractsCountDto),
                                        getContractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(string userName, string branchCode){
            var getOneMonthToDueDateContractsByBranchCodeDto = new GetOneMonthToDueDateContractsByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                                    ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetContractsCountByBranchCodeDto>(
                                        async () => await _contractService.GetOneMonthToDueDateContractsByBranchCodeAsync(getOneMonthToDueDateContractsByBranchCodeDto),
                                        getOneMonthToDueDateContractsByBranchCodeDto);
        }

        public async Task<CountAndSumDto> GetAllDebtsCountByBranchCodeAsync(string userName, string branchCode)
        {
            var getAllDebtsCountDto = new GetAllDebtsCountByBranchCodeDto{ BranchCode = branchCode, UserName = userName };
            return await
                                    ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetAllDebtsCountByBranchCodeDto>(
                                        async () => await _contractService.GetAllDebtsCountByBranchCodeAsync(getAllDebtsCountDto),
                                        getAllDebtsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(string userName, string branchCode)
        {
            var getAllDebtsByBranchCodeDto = new GetAllDebtsContractsByBranchCodeDto { BranchCode = branchCode, UserName = userName };
            return await
                                    ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetAllDebtsContractsByBranchCodeDto>(
                                        async () => await _contractService.GetAllDebtsContractsByBranchCodeAsync(getAllDebtsByBranchCodeDto),
                                        getAllDebtsByBranchCodeDto);
        }

        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(string userName)
        {
            var getAlldebtsContractsDto = new GetAllDebtsContractsDto { UserName = userName };
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetAllDebtsContractsDto>(
                    async () => await _contractService.GetAllDebtsContractsAsync(getAlldebtsContractsDto),
                    getAlldebtsContractsDto);
        }
        #endregion
       

    }
}