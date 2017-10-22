using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
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
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IDelinquentServiceContract{
        #region BranchRequest

        [OperationContract]
        Task<BranchDto> GetBranchInformationAsync(GetBranchDto getBranchDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(
            GetContractsByUserNameDto getContractsByUserNameDto);
        [OperationContract]
        Task<CountAndSumDto> GetCountContractsAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<CustomerInformationDto> GetCustomerInformationAsync(GetCustomerInformationDto customerInformationDto);
        [OperationContract]
        Task<IEnumerable<LogDto>> GetCustomerLogsAsync(GetCustomerLogsDto customerLogsDto);
        [OperationContract]
        Task AddWrittenNoticeLogAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto);
        [OperationContract]
        Task EditWrittenNoticeLogAsync(EditWrittenNoticeLogDto dto);
        [OperationContract]
        Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(GetWrittenNoticeLogDto dto);
        [OperationContract]
        Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto);
        [OperationContract]
        Task EditAppointmentLogAsync(EditAppointmentLogDto dto);
        [OperationContract]
        Task<AppointmentLogDto> GetAppointmentLogAsync(GetAppointmentLogDto dto);
        [OperationContract]
        Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto);
        [OperationContract]
        Task EditNoticeLogAsync(EditNoticeLogDto dto);
        [OperationContract]
        Task<NoticeLogDto> GetNoticeLogAsync(GetNoticeLogDto dto);
        [OperationContract]
        Task<CountAndSumDto> GetCurrentContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(GetCurrentContractsDto getCurrentContractsDto);
        [OperationContract]
        Task<CountAndSumDto> GetDueDateContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetExpireContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetBaddebtContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetPostponedContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetAllDebtsContractsCountAsync(GetContractsCountDto contractsCountDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(GetBadDebtDto getBadDebtDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(GetPostponedDto getPostponedDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(GetDueDateContractsDto getDueDateContractsDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(GetExpireContractsDto getExpireContracts);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(
            GetAllDebtsContractsDto getAllDebtsContractsDto);
        [OperationContract]
        Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto);
        [OperationContract]
        Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto);
        [OperationContract]
        Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto);
        [OperationContract]
        Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editImpunityForCrimesLogDto);
        [OperationContract]
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(GetGuarantorsDto getGuarantorsDto);
        [OperationContract]
        Task<IEnumerable<BondDto>> GetBondsAsync(GetBondsDto getBondsDto);
        [OperationContract]
        Task<IEnumerable<NotificationDto>> GetNotificationsAsync(GetNotificationsDto getNotifications);
        [OperationContract]
        Task UpdateNotificationToSeenAsync(UpdateNotificationToSeenDto updateNotificationToSeenDto);
        [OperationContract]
        Task RemoveNotificationAsync(RemoveNotificationDto removeNotification);
        [OperationContract]
        Task RespondRequestGivingAChanceAsync(RespondRequestGivingAChanceDto respondRequestGivingAChanceDto);
        [OperationContract]
        Task RespondRequestImpunityForCrimesAsync(RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto);
        [OperationContract]
        Task<CountAndSumDto> GetOneMonthToDueDateContractsCountAsync(GetContractsCountDto getContractsCountDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(
            GetOneMonthToDueDateContractsDto getOneMonthToDueDateContractsDto);
        [OperationContract]
        Task<CallLogDto> GetCallLogAsync(GetCallLogDto dto);
        [OperationContract]
        Task AddCallLogAsync(AddCallLogDto addCallLogDto);

        [OperationContract]
        Task EditCallLogAsync(EditCallLogDto editCallLogDto);

        [OperationContract]
        Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(GetYesterdayLogsDto yesterdayLogDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(GetCustomerDelinquentHistoryDto customerDelinquentHistoryDto);

            #region Clearing

        [OperationContract]
        Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto);
        [OperationContract]
        Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto);
        [OperationContract]
        Task<ImpunityForCrimesLogDto> GetImpunityLogAsync(GetImpunityLogDto dto);
        [OperationContract]
        Task<GivingAChanceLogDto> GetGivingAChanceLogAsync(GetGivingAChanceLogDto dto);
        [OperationContract]
        Task<SplitLogDto> GetSplitLogAsync(GetSplitLogDto dto);
        [OperationContract]
        Task<ClearingLogDto> GetClearingLogAsync(GetClearingLogDto dto);
        [OperationContract]
        Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto);
        [OperationContract]
        Task EditClearingLogAsync(EditClearingLogDto editClearingLogDto);
        [OperationContract]
        Task SetAllowEditToFalseForClearingLogAsync(DisableClearingEditingDto disableClearingEditingDto);

        #endregion
#region Split

        [OperationContract]
        Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto);
        [OperationContract]
        Task EditSplitLogAsync(EditSplitLogDto editSplitLogDto);
        [OperationContract]
        Task RespondRequestSplitAsyncTask(RespondRequestSplitDto respondRequestSplitDto);
        [OperationContract]
        Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto);
        [OperationContract]
        Task SetAllowEditToFalseForSplitLogAsync(DisableSplitEditingDto editSplitLogDto);

        #endregion
        #endregion

        

        [OperationContract]
        Task SetAllowEditToFalseForImpunityLogAsync(DisableImpunityEditingDto disableImpunityEditingDto);
        [OperationContract]
        Task SetAllowEditToFalseForGivingAChanceLogAsync(DisableGivingAChanceEditingDto disableGivingAChanceEditingDto);
        [OperationContract]
        Task<bool> CheckPrivilegeEditRequestClearingLogAsync(GetRequestClearingLogDto getRequestClearingLogDto);
        [OperationContract]
        Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto);
        [OperationContract]
        Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(GetRequestImpunityLogDto getRequestImpunityLogDto);
        [OperationContract]
        Task<bool> CheckPrivilegeEditRequestGivingAChanceLogAsync(
            GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto);
        [OperationContract]
        Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceLogAsync(GetRequestGivingAChanceLogDto dto);
        [OperationContract]
        Task<RequestImpunityForCrimesLogDto> GetRequestImpunityLogAsync(GetRequestImpunityLogDto dto);
        [OperationContract]
        Task<RequestClearingLogDto> GetRequestClearingLogAsync(GetRequestClearingLogDto dto);
        [OperationContract]
        Task<RequestSplitLogDto> GetRequestSplitLogAsync(GetRequestSplitLogDto dto);
        [OperationContract]
        Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editGivingAChanceLogDto);
        [OperationContract]
        Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto);
        [OperationContract]
        Task EditRequestClearingLogAsync(EditRequestClearingLogDto editClearingLogDto);
        [OperationContract]
        Task EditRequestImpunityForCrimesLogAsync(EditRequestImpunityForCrimesLogDto editGivingAChanceLogDto);
        [OperationContract]
        Task EditRenewalLogAsync(EditRenewalLogDto dto);
        [OperationContract]
        Task AddRenewalLogAsync(AddRenewalLogDto dto);
        [OperationContract]
        Task<RenewalLogDto> GetRenewalLogAsync(GetRenewalLogDto dto);
        [OperationContract]
        Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto);
        [OperationContract]
        Task<bool> CheckPrivilegeAddRenewalLogAsync(AddRenewalLogDto dto);


        #region ManagerRequest
        [OperationContract]
        Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(
            GetAllBranchActivityDtq getAllBranchActivityDtq);
        [OperationContract]
        Task<string> GetLastAbisUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto);
        [OperationContract]
         Task<string> GetLastBankIranUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto);
        [OperationContract]
        Task<IEnumerable<BranchDto>> GetAllBranch(GetAllBranchDto getAllBranchDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(
            GetContractsByBranchCodeDto getContractsByBranchCodeDto);
        [OperationContract]
        Task<CountAndSumDto> GetCountContractsByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<IEnumerable<LogDto>> GetCustomerLogsByBranchCodeAsync(GetCustomerLogsByBranchCodeDto customerLogsDto);
        [OperationContract]
        Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(
            GetCurrentContractsByBranchCodeDto getCurrentContractsDto);
        [OperationContract]
        Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetBaddebtContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto contractsCountDto);
        [OperationContract]
        Task<CountAndSumDto> GetAllDebtsContractsCountByBranchCodeAsync(
            GetAllDebtsCountByBranchCodeDto getAllDebtsCountByBranchCodeDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(
            GetBadDebtByBranchCodeDto getBadDebtDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(
            GetPostponedByBranchCodeDto getPostponedDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(
            GetDueDateContractsByBranchCodeDto getDueDateContractsDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(
            GetExpireContractsByBranchCodeDto getExpireContracts);
        [OperationContract]
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(
            GetGuarantorsByBranchCodeDto getGuarantorsByBranchCodeDto);
        [OperationContract]
        Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(GetBondsByBranchCodeDto getBondsByBranchCodeDto);
        [OperationContract]
        Task<CountAndSumDto> GetOneMonthToDueDateContractsCountByBranchCodeAsync(
            GetContractsCountByBranchCodeDto getContractsCountDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(
            GetOneMonthToDueDateContractsByBranchCodeDto getOneMonthToDueDateContractsByBranchCodeDto);
        [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(
            GetAllDebtsContractsByBranchCodeDto getAllDebtsContractsByBranchCodeDto);
        [OperationContract]
        Task<BranchClaimDto> GetBranchClaim(GetBranchClaimDto branchClaimDto);
        [OperationContract]
        Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(GetLastSevenDaysBranchClaimsDto branchClaimsDto);
            [OperationContract]
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(
            GetCustomerDelinquentHistoryByBranchCodeDto customerDelinquentHistoryByBranchCodeDto);
            [OperationContract]
            Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(
                GetAllCustomerDelinquentsHistoryDto getAllCustomerDelinquentsHistoryDto);

        [OperationContract]
        Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquentAsync(GetBranchDelinquentDtq branchDelinquentDtq);
        [OperationContract]
        Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquentAsync(GetBranchesDelinquentDtq branchesDelinquentDtq);
        #endregion
    }
}