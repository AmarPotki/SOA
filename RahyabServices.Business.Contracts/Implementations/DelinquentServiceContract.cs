using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
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
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
using FluentValidation;
namespace RahyabServices.Business.Contracts.Implementations{
    public class DelinquentServiceContract : ContractBase, IDelinquentServiceContract{
        private readonly IBranchClaimService _branchClaimService;
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly IClearingService _clearingService;
        private readonly IContractService _contractService;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IGivingAChanceService _givingAChanceService;
        private readonly IHrFacade _hrFacade;
        private readonly IImpunityService _impunityService;
        private readonly ILogService _logService;
        private readonly INotificationService _notificationService;
        private readonly IRPTFTDelinquentService _rptftDelinquentService;
        private readonly ISplitService _splitService;
        private readonly IBranchService _branchService;
        private readonly IRenewalService _renewalService;
        private readonly IDelinquentService _delinquentService;
        public DelinquentServiceContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, IContractService contractService, ICustomerAccountService customerAccountService,
            ILogService logService, IRPTFTDelinquentService rptftDelinquentService,
            INotificationService notificationService, IHrFacade hrFacade, IClearingService clearingService,
            ISharepointAuthorizationService sharepointAuthorizationService,
            IBranchPrivilegesService branchPrivilegesService, ISplitService splitService,
            IGivingAChanceService givingAChanceService, IImpunityService impunityService,
            IBranchClaimService branchClaimService, IBranchService branchService, IRenewalService renewalService, IDelinquentService delinquentService) :
                base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _contractService = contractService;
            _customerAccountService = customerAccountService;
            _logService = logService;
            _rptftDelinquentService = rptftDelinquentService;
            _notificationService = notificationService;
            _hrFacade = hrFacade;
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

        #region BranchRequest

        public async Task SetAllowEditToFalseForImpunityLogAsync(DisableImpunityEditingDto disableImpunityEditingDto){
            await
                ValidateThenExecuteFaultHandledOperation<DisableImpunityEditingDto>(
                    async () => await _impunityService.DisableImpunityEditingDto(disableImpunityEditingDto),
                    disableImpunityEditingDto);
        }

        #region GivingAChance

        #endregion

        #region Renewal
        public async Task<RenewalLogDto> GetRenewalLogAsync(GetRenewalLogDto dto)
        {
            return await
                         ValidateThenExecuteFaultHandledOperation<RenewalLogDto, GetRenewalLogDto>(
                             async () => await _renewalService.GetRenewalLogAsync(dto), dto);
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
        public async Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(GetAllBranchActivityDtq getAllBranchActivityDtq){
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<AllBranchActivityDto>, GetAllBranchActivityDtq>(
                   async () => await _contractService.GetAllBranchActivityAsync(getAllBranchActivityDtq), getAllBranchActivityDtq);
        }
        public async Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto)
        {
            return await
                ValidateThenExecuteFaultHandledOperation<bool, EditRenewalLogDto>(
                    async () => await _renewalService.CheckPrivilegeEditRenewalLogAsync(dto), dto);

        }


        #endregion
        public async Task SetAllowEditToFalseForGivingAChanceLogAsync(
            DisableGivingAChanceEditingDto disableGivingAChanceEditingDto){
            await
                ValidateThenExecuteFaultHandledOperation<DisableGivingAChanceEditingDto>(
                    async () =>
                        await _givingAChanceService.DisableGivingAChanceEditingDto(disableGivingAChanceEditingDto),
                    disableGivingAChanceEditingDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(
            GetRequestImpunityLogDto getRequestImpunityLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, GetRequestImpunityLogDto>(
                    async () =>
                        await _impunityService.CheckPrivilegeEditRequestImpunityLogAsync(getRequestImpunityLogDto),
                    getRequestImpunityLogDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestGivingAChanceLogAsync(
            GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, GetRequestGivingAChanceLogDto>(
                    async () =>
                        await
                            _givingAChanceService.CheckPrivilegeEditRequestGivingAchanceLogAsync(
                                getRequestGivingAChanceLogDto), getRequestGivingAChanceLogDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestClearingLogAsync(
            GetRequestClearingLogDto getRequestClearingLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, GetRequestClearingLogDto>(
                    async () =>
                        await _clearingService.CheckPrivilegeEditRequestClearingLogAsync(getRequestClearingLogDto),
                    getRequestClearingLogDto);
        }
        public async Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, GetRequestSplitLogDto>(
                    async () => await _splitService.CheckPrivilegeEditRequestSplitLogAsync(getRequestSplitLogDto),
                    getRequestSplitLogDto);
        }

        #region Split

        public async Task<SplitLogDto> GetSplitLogAsync(GetSplitLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<SplitLogDto, GetSplitLogDto>(
                    async () => await _splitService.GetSplitLogAsync(dto), dto);
        }
        public async Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddSplitLogDto>(
                    async () => await _splitService.AddSplitLogAsync(addSplitLogDto), addSplitLogDto);
        }
        public async Task EditSplitLogAsync(EditSplitLogDto editSplitLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditSplitLogDto>(
                    async () => await _splitService.EditSplitAsync(editSplitLogDto), editSplitLogDto);
        }
        public async Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestSplitLogDto>(
                    async () => await _splitService.EditRequestSplitLogAsync(editRequestSplitLogDto),
                    editRequestSplitLogDto);
        }
        public async Task RespondRequestSplitAsyncTask(RespondRequestSplitDto respondRequestSplitDto){
            await
                ValidateThenExecuteFaultHandledOperation<RespondRequestSplitDto>(
                    async () => await _splitService.RespondRequestSplitAsync(respondRequestSplitDto),
                    respondRequestSplitDto);
        }
        public async Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, AddSplitLogDto>(
                    async () => await _splitService.CheckPrivilegeAddSplitLogAsync(addSplitLogDto),
                    addSplitLogDto);
        }
        public async Task SetAllowEditToFalseForSplitLogAsync(DisableSplitEditingDto disableSplitEditingDto){
            await
                ValidateThenExecuteFaultHandledOperation<DisableSplitEditingDto>(
                    async () => await _splitService.DisableSplitEditingDto(disableSplitEditingDto),
                    disableSplitEditingDto);
        }
        public async Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceLogAsync(GetRequestGivingAChanceLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<RequestGivingAChanceLogDto, GetRequestGivingAChanceLogDto>(
                    async () => await _givingAChanceService.GetRequestGivingAChanceAsync(dto), dto);
        }
        public async Task<RequestImpunityForCrimesLogDto> GetRequestImpunityLogAsync(GetRequestImpunityLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<RequestImpunityForCrimesLogDto, GetRequestImpunityLogDto>(
                    async () => await _impunityService.GetRequestImpunityAsync(dto), dto);
        }
        public async Task<RequestClearingLogDto> GetRequestClearingLogAsync(GetRequestClearingLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<RequestClearingLogDto, GetRequestClearingLogDto>(
                    async () => await _clearingService.GetRequestClearingAsync(dto), dto);
        }
        public async Task<RequestSplitLogDto> GetRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<RequestSplitLogDto, GetRequestSplitLogDto>(
                    async () => await _splitService.GetRequestSplitLogAsync(getRequestSplitLogDto),
                    getRequestSplitLogDto);
        }

        #endregion

        #region Clearing   

        public async Task SetAllowEditToFalseForClearingLogAsync(DisableClearingEditingDto disableClearingEditingDto){
            await
                ValidateThenExecuteFaultHandledOperation<DisableClearingEditingDto>(
                    async () => await _clearingService.DisableClearingEditingDto(disableClearingEditingDto),
                    disableClearingEditingDto);
        }
        public async Task<ClearingLogDto> GetClearingLogAsync(GetClearingLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<ClearingLogDto, GetClearingLogDto>(
                    async () => await _clearingService.GetClearingAsync(dto), dto);
        }
        public async Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, AddClearingLogDto>(
                    async () => await _clearingService.CheckPrivilegeAddClearingLogAsync(addClearingLogDto),
                    addClearingLogDto);
        }
        public async Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddClearingLogDto>(
                    async () => await _clearingService.AddClearingLogAsync(addClearingLogDto), addClearingLogDto);
        }
        public async Task EditClearingLogAsync(EditClearingLogDto editClearingLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditClearingLogDto>(
                    async () => await _clearingService.EditClearingAsync(editClearingLogDto), editClearingLogDto);
        }
        public async Task EditRequestClearingLogAsync(EditRequestClearingLogDto editClearingLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestClearingLogDto>(
                    async () => await _clearingService.EditRequestClearingAsync(editClearingLogDto), editClearingLogDto);
        }
        public async Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(GetYesterdayLogsDto yesterdayLogDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<YesterdayLogDto>, GetYesterdayLogsDto>(
                    async () => await _logService.GetYesterDayLogsAsync(yesterdayLogDto), yesterdayLogDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(
            GetCustomerDelinquentHistoryDto customerDelinquentHistoryDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetCustomerDelinquentHistoryDto>(
                        async () =>
                            await _contractService.GetCustomerDelinquentHistoryAsync(customerDelinquentHistoryDto),
                        customerDelinquentHistoryDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(
            GetCustomerDelinquentHistoryByBranchCodeDto customerDelinquentHistoryByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetCustomerDelinquentHistoryByBranchCodeDto>(
                        async () =>
                            await
                                _contractService.GetCustomerDelinquentHistoryByBranchCodeAsync(
                                    customerDelinquentHistoryByBranchCodeDto),
                        customerDelinquentHistoryByBranchCodeDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(
            GetAllCustomerDelinquentsHistoryDto getAllCustomerDelinquentsHistoryDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetAllCustomerDelinquentsHistoryDto>(
                        async () =>
                            await
                                _contractService.GetAllCustomerDelinquentsHistoryAsync(
                                    getAllCustomerDelinquentsHistoryDto),
                        getAllCustomerDelinquentsHistoryDto);
        }
        public async Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquentAsync(GetBranchDelinquentDtq branchDelinquentDtq){
          return  await
             ValidateThenExecuteFaultHandledOperation< IEnumerable<BranchDelinquentDto>, GetBranchDelinquentDtq>(
                 async () => await _delinquentService.GetBranchDelinquent(branchDelinquentDtq),
                 branchDelinquentDtq);
        }
        public async Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquentAsync(GetBranchesDelinquentDtq branchesDelinquentDtq){
            return await
                        ValidateThenExecuteFaultHandledOperation<IEnumerable<BranchesDelinquentDto>, GetBranchesDelinquentDtq>(
                            async () => await _delinquentService.GetBranchesDelinquent(branchesDelinquentDtq),
                            branchesDelinquentDtq);
        }
        public async Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto){
            await
                ValidateThenExecuteFaultHandledOperation<RespondRequestClearingDto>(
                    async () => await _clearingService.RespondRequestClearingAsync(respondRequestClearingDto),
                    respondRequestClearingDto);
        }

        #endregion

        public async Task<BranchDto> GetBranchInformationAsync(GetBranchDto getBranchDto){
            return await
               ValidateThenExecuteFaultHandledOperation<BranchDto, GetBranchDto>(
                   async () => await _branchService.GetBranchInformationAsync(getBranchDto),
                   getBranchDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(
            GetContractsByUserNameDto getContractsByUserNameDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetContractsByUserNameDto>(
                    async () => await _contractService.GetBranchContractsAsync(getContractsByUserNameDto),
                    getContractsByUserNameDto);
        }
        public async Task<CountAndSumDto> GetCountContractsAsync(GetContractsCountDto contractsCountDto){
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <CountAndSumDto, GetContractsCountDto>(
                            async () => await _contractService.GetBranchContractsCountAsync(contractsCountDto),
                            contractsCountDto);
        }
        public async Task<CustomerInformationDto> GetCustomerInformationAsync(
            GetCustomerInformationDto customerInformationDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CustomerInformationDto, GetCustomerInformationDto>(
                    async () => await _customerAccountService.GetCustomerInformationAsync(customerInformationDto),
                    customerInformationDto);
        }
        public async Task<IEnumerable<LogDto>> GetCustomerLogsAsync(GetCustomerLogsDto customerLogsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<LogDto>, GetCustomerLogsDto>(
                    async () => await _logService.GetLogsAsync(customerLogsDto), customerLogsDto);
        }
        public async Task AddWrittenNoticeLogAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddWrittenNoticeLogDto>(
                    async () => await _logService.AddWrittenNoticeLogAsync(addWrittenNoticeLogDto),
                    addWrittenNoticeLogDto);
        }

        public async Task EditWrittenNoticeLogAsync(EditWrittenNoticeLogDto editWrittenNoticeLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditWrittenNoticeLogDto>(
                    async () => await _logService.EditWrittenNoticeLogAsync(editWrittenNoticeLogDto),
                    editWrittenNoticeLogDto);
        }
        public async Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(
            GetWrittenNoticeLogDto dto)
        {
            return await
                ValidateThenExecuteFaultHandledOperation<WrittenNoticeLogDto, GetWrittenNoticeLogDto>(
                    async () => await _logService.GetWrittenNoticeLogAsync(dto),
                    dto);
        }
        public async Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddAppointmentLogDto>(
                    async () => await _logService.AddAppointmentLogAsync(addAppointmentLogDto), addAppointmentLogDto);
        }

        public async Task EditAppointmentLogAsync(EditAppointmentLogDto editAppointmentLogDto)
        {
            await
                ValidateThenExecuteFaultHandledOperation<EditAppointmentLogDto>(
                    async () => await _logService.EditAppointmentLogAsync(editAppointmentLogDto), editAppointmentLogDto);
        }
        public async Task<AppointmentLogDto> GetAppointmentLogAsync(
            GetAppointmentLogDto dto)
        {
            return await
                ValidateThenExecuteFaultHandledOperation<AppointmentLogDto, GetAppointmentLogDto>(
                    async () => await _logService.GetAppointmentLogAsync(dto),
                    dto);
        }
        public async Task<NoticeLogDto> GetNoticeLogAsync(GetNoticeLogDto dto){
            return
                await
                    ValidateThenExecuteFaultHandledOperation<NoticeLogDto, GetNoticeLogDto>(
                        async () => await _logService.GetNoticeLogAsync(dto), dto);
        }
        public async Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddNoticeLogDto>(
                    async () => await _logService.AddNoticeLogAsync(addNoticeLogDto), addNoticeLogDto);
        }
        public async Task EditNoticeLogAsync(EditNoticeLogDto editNoticeLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditNoticeLogDto>(
                    async () => await _logService.EditNoticeLogAsync(editNoticeLogDto), editNoticeLogDto);
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetCurrentContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(
            GetCurrentContractsDto getCurrentContractsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetCurrentContractsDto>(
                    async () => await _contractService.GetCurrentContractsAsync(getCurrentContractsDto),
                    getCurrentContractsDto);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetDueDateContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetExpireContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetAllDebtsContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetAllDebtsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetAllDebtsContractsCountByBranchCodeAsync(
            GetAllDebtsCountByBranchCodeDto getAllDebtsCountByBranchCodeDto){
            var getAllDebtsCountDto = new GetAllDebtsCountByBranchCodeDto
            {
                BranchCode = getAllDebtsCountByBranchCodeDto.BranchCode,
                UserName = getAllDebtsCountByBranchCodeDto.UserName
            };
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetAllDebtsCountByBranchCodeDto>(
                    async () => await _contractService.GetAllDebtsCountByBranchCodeAsync(getAllDebtsCountDto),
                    getAllDebtsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(
            GetAllDebtsContractsByBranchCodeDto getAllDebtsContractsDto){
            var getAllDebtsByBranchCodeDto = new GetAllDebtsContractsByBranchCodeDto
            {
                BranchCode = getAllDebtsContractsDto.BranchCode,
                UserName = getAllDebtsContractsDto.UserName
            };
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetAllDebtsContractsByBranchCodeDto>(
                        async () =>
                            await _contractService.GetAllDebtsContractsByBranchCodeAsync(getAllDebtsByBranchCodeDto),
                        getAllDebtsByBranchCodeDto);
        }
        public async Task<BranchClaimDto> GetBranchClaim(GetBranchClaimDto branchClaimDto){
            return await
                ValidateThenExecuteFaultHandledOperation<BranchClaimDto, GetBranchClaimDto>(
                    async () => await _branchClaimService.GetBranchClaim(branchClaimDto),
                    branchClaimDto);
        }
        public async Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(
            GetLastSevenDaysBranchClaimsDto branchClaimsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<BranchClaimDto>, GetLastSevenDaysBranchClaimsDto>(
                    async () => await _branchClaimService.GetLastSevenDaysBranchClaims(branchClaimsDto),
                    branchClaimsDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(
            GetAllDebtsContractsDto getAllDebtsContractsDto){
            var getAlldebtsContractsDto = new GetAllDebtsContractsDto {UserName = getAllDebtsContractsDto.UserName};
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetAllDebtsContractsDto>(
                    async () => await _contractService.GetAllDebtsContractsAsync(getAlldebtsContractsDto),
                    getAlldebtsContractsDto);
        }
        public async Task<CountAndSumDto> GetBaddebtContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetBadDebtContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountAsync(GetContractsCountDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetPostponedContractsCountAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(GetBadDebtDto getBadDebtDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetBadDebtDto>(
                    async () => await _contractService.GetBadDebtContractsAsync(getBadDebtDto),
                    getBadDebtDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(GetPostponedDto getPostponedDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetPostponedDto>(
                    async () => await _contractService.GetPostponedContractsAsync(getPostponedDto),
                    getPostponedDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(
            GetDueDateContractsDto getDueDateContractsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetDueDateContractsDto>(
                    async () => await _contractService.GetDueDateContractsAsync(getDueDateContractsDto),
                    getDueDateContractsDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(
            GetExpireContractsDto getExpireContracts){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetExpireContractsDto>(
                    async () => await _contractService.GetExpireContractsAsync(getExpireContracts),
                    getExpireContracts);
        }
        public async Task<GivingAChanceLogDto> GetGivingAChanceLogAsync(GetGivingAChanceLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<GivingAChanceLogDto, GetGivingAChanceLogDto>(
                    async () => await _givingAChanceService.GetGivingAChanceAsync(dto), dto);
        }
        public async Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddGivingAChanceLogDto>(
                    async () => await _givingAChanceService.AddGivingAChanceLogAsync(addGivingAChanceLogDto),
                    addGivingAChanceLogDto);
        }
        public async Task<ImpunityForCrimesLogDto> GetImpunityLogAsync(GetImpunityLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<ImpunityForCrimesLogDto, GetImpunityLogDto>(
                    async () => await _impunityService.GetImpunityAsync(dto), dto);
        }
        public async Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddImpunityForCrimesLogDto>(
                    async () => await _impunityService.AddImpunityForCrimesLogAsync(addImpunityForCrimesLogDto),
                    addImpunityForCrimesLogDto);
        }
        public async Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editImpunityForCrimesLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditImpunityForCrimesLogDto>(
                    async () => await _impunityService.EditImpunityForCrimesLogAsync(editImpunityForCrimesLogDto),
                    editImpunityForCrimesLogDto);
        }
        public async Task EditRequestImpunityForCrimesLogAsync(
            EditRequestImpunityForCrimesLogDto editImpunityForCrimesLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestImpunityForCrimesLogDto>(
                    async () => await _impunityService.EditRequestImpunityForCrimesLogAsync(editImpunityForCrimesLogDto),
                    editImpunityForCrimesLogDto);
        }
        public async Task<string> GetLastAbisUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto){
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetLastUpdateDateDto>(
                    async () => await _contractService.GetLastAbisUpdateDateAsync(getLastUpdateDateDto),
                    getLastUpdateDateDto);
        }
        public async Task<string> GetLastBankIranUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto){
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetLastUpdateDateDto>(
                    async () => await _contractService.GetLastBankIranUpdateDateAsync(getLastUpdateDateDto),
                    getLastUpdateDateDto);
        }
        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(GetGuarantorsDto getGuarantorsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<GuarantorsDto>, GetGuarantorsDto>(
                    async () => await _rptftDelinquentService.GetGuarantorsAsync(getGuarantorsDto),
                    getGuarantorsDto);
        }
        public async Task<IEnumerable<BondDto>> GetBondsAsync(GetBondsDto getBondsDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<BondDto>, GetBondsDto>(
                    async () => await _rptftDelinquentService.GetBondsAsync(getBondsDto),
                    getBondsDto);
        }
        public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(GetNotificationsDto getNotifications){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<NotificationDto>, GetNotificationsDto>(
                    async () => await _notificationService.GetNotifications(getNotifications),
                    getNotifications);
        }
        public async Task UpdateNotificationToSeenAsync(UpdateNotificationToSeenDto updateNotificationToSeenDto){
            await
                ValidateThenExecuteFaultHandledOperation<UpdateNotificationToSeenDto>(
                    async () => await _notificationService.UpdateToSeen(updateNotificationToSeenDto),
                    updateNotificationToSeenDto);
        }
        public async Task RemoveNotificationAsync(RemoveNotificationDto removeNotification){
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
        public async Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditGivingAChanceLogDto>(
                    async () => await _givingAChanceService.EditGivingAChanceLogAsync(editGivingAChanceLogDto),
                    editGivingAChanceLogDto);
        }
        public async Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editRequestGivingAChanceLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditRequestGivingAChanceLogDto>(
                    async () =>
                        await _givingAChanceService.EditRequestGivingAChanceLogAsync(editRequestGivingAChanceLogDto),
                    editRequestGivingAChanceLogDto);
        }
        public async Task RespondRequestImpunityForCrimesAsync(
            RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto){
            await
                ValidateThenExecuteFaultHandledOperation<RespondRequestImpunityForCrimesDto>(
                    async () =>
                        await _impunityService.RespondRequestImpunityForCrimesAsync(respondRequestImpunityForCrimesDto),
                    respondRequestImpunityForCrimesDto);
        }
        public async Task<CountAndSumDto> GetOneMonthToDueDateContractsCountAsync(
            GetContractsCountDto getContractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountDto>(
                    async () => await _contractService.GetOneMonthToDueDateCountAsync(getContractsCountDto),
                    getContractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(
            GetOneMonthToDueDateContractsDto getOneMonthToDueDateContractsDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetOneMonthToDueDateContractsDto>(
                        async () =>
                            await _contractService.GetOneMonthToDueDateContractsAsync(getOneMonthToDueDateContractsDto),
                        getOneMonthToDueDateContractsDto);
        }
        public async Task<CallLogDto> GetCallLogAsync(GetCallLogDto dto){
            return await
                ValidateThenExecuteFaultHandledOperation<CallLogDto, GetCallLogDto>(
                    async () => await _logService.GetCallLogDto(dto), dto);
        }
        public async Task AddCallLogAsync(AddCallLogDto addCallLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<AddCallLogDto>(
                    async () => await _logService.AddCallLogAsync(addCallLogDto),
                    addCallLogDto);
        }
        public async Task EditCallLogAsync(EditCallLogDto editCallLogDto){
            await
                ValidateThenExecuteFaultHandledOperation<EditCallLogDto>(
                    async () => await _logService.EditCallLogAsync(editCallLogDto),
                    editCallLogDto);
        }
        public async Task<IEnumerable<BranchDto>> GetAllBranch(GetAllBranchDto getAllBranchDto){
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <IEnumerable<BranchDto>, GetAllBranchDto>(
                            async () => await _branchService.GetAllBranchesAsync(getAllBranchDto), getAllBranchDto
                        );
        }

        #endregion

        #region ManagerRequest

        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(
            GetContractsByBranchCodeDto getContractsByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetContractsByBranchCodeDto>(
                        async () =>
                            await _contractService.GetBranchContractsByBranchCodeAsync(getContractsByBranchCodeDto),
                        getContractsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetCountContractsByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto){
            return
                await
                    ValidateThenExecuteFaultHandledOperation
                        <CountAndSumDto, GetContractsCountByBranchCodeDto>(
                            async () =>
                                await _contractService.GetBranchContractsCountByBranchCodeAsync(contractsCountDto),
                            contractsCountDto);
        }
        public async Task<IEnumerable<LogDto>> GetCustomerLogsByBranchCodeAsync(
            GetCustomerLogsByBranchCodeDto getCustomerLogsByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<LogDto>, GetCustomerLogsByBranchCodeDto>(
                    async () => await _logService.GetLogsByBranchCodeAsync(getCustomerLogsByBranchCodeDto),
                    getCustomerLogsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () =>
                        await _contractService.GetCurrentContractsCountByBranchCodeAsync(contractsCountByBranchCodeDto),
                    contractsCountByBranchCodeDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(
            GetCurrentContractsByBranchCodeDto getCurrentContractsDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetCurrentContractsByBranchCodeDto>(
                        async () => await _contractService.GetCurrentContractsByBranchCodeAsync(getCurrentContractsDto),
                        getCurrentContractsDto);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () => await _contractService.GetDueDateContractsCountByBranchCodeAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () => await _contractService.GetExpireContractsCountByBranchCodeAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetBaddebtContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () => await _contractService.GetBadDebtContractsCountByBranchCodeAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () => await _contractService.GetPostponedContractsCountByBranchCodeAsync(contractsCountDto),
                    contractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(
            GetBadDebtByBranchCodeDto getBadDebtDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<CustomerDelinquentDto>, GetBadDebtByBranchCodeDto>(
                    async () => await _contractService.GetBadDebtContractsByBranchCodeAsync(getBadDebtDto),
                    getBadDebtDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(
            GetPostponedByBranchCodeDto getPostponedDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetPostponedByBranchCodeDto>(
                        async () => await _contractService.GetPostponedContractsByBranchCodeAsync(getPostponedDto),
                        getPostponedDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(
            GetDueDateContractsByBranchCodeDto getDueDateContractsDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetDueDateContractsByBranchCodeDto>(
                        async () => await _contractService.GetDueDateContractsByBranchCodeAsync(getDueDateContractsDto),
                        getDueDateContractsDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(
            GetExpireContractsByBranchCodeDto getExpireContracts){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetExpireContractsByBranchCodeDto>(
                        async () => await _contractService.GetExpireContractsByBranchCodeAsync(getExpireContracts),
                        getExpireContracts);
        }
        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(
            GetGuarantorsByBranchCodeDto getGuarantorsByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<GuarantorsDto>, GetGuarantorsByBranchCodeDto>(
                    async () =>
                        await _rptftDelinquentService.GetGuarantorsByBranchCodeAsync(getGuarantorsByBranchCodeDto),
                    getGuarantorsByBranchCodeDto);
        }
        public async Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(
            GetBondsByBranchCodeDto getBondsByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation<IEnumerable<BondDto>, GetBondsByBranchCodeDto>(
                    async () => await _rptftDelinquentService.GetBondsByBranchCodeAsync(getBondsByBranchCodeDto),
                    getBondsByBranchCodeDto);
        }
        public async Task<CountAndSumDto> GetOneMonthToDueDateContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto getContractsCountDto){
            return await
                ValidateThenExecuteFaultHandledOperation<CountAndSumDto, GetContractsCountByBranchCodeDto>(
                    async () => await _contractService.GetOneMonthToDueDateCountByBranchCodeAsync(getContractsCountDto),
                    getContractsCountDto);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(
            GetOneMonthToDueDateContractsByBranchCodeDto getOneMonthToDueDateContractsByBranchCodeDto){
            return await
                ValidateThenExecuteFaultHandledOperation
                    <IEnumerable<CustomerDelinquentDto>, GetContractsCountByBranchCodeDto>(
                        async () =>
                            await
                                _contractService.GetOneMonthToDueDateContractsByBranchCodeAsync(
                                    getOneMonthToDueDateContractsByBranchCodeDto),
                        getOneMonthToDueDateContractsByBranchCodeDto);
        }

        #endregion
    }
}