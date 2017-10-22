using System.Collections.Generic;
using System.Reflection.Emit;
using System.ServiceModel;
using System.ServiceModel.Web;
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
    public interface IDelinquentServiceRestContract{
        #region BranchRequest
        [OperationContract]
        [WebGet(UriTemplate = "GetBranchInformation/{userName}")]
        Task<BranchDto> GetBranchInformationAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllBranch/{userName}")]
        Task<IEnumerable<BranchDto>> GetAllBranch(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetBranchContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCountContracts/{userName}")]
        Task<CountAndSumDto> GetCountContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerInformation/{customerNumber}/{userName}")]
        Task<CustomerInformationDto> GetCustomerInformationAsync(string customerNumber, string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerLogs/{userName}/{customerDelinquentId}")]
        Task<IEnumerable<LogDto>> GetCustomerLogsAsync(string userName, string customerDelinquentId);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddWrittenNoticeLog")]
        Task AddWrittenNoticeAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditWrittenNoticeLog")]
        Task EditWrittenNoticeAsync(EditWrittenNoticeLogDto dto);
        [OperationContract]
        [WebGet(UriTemplate = "GetWrittenNoticeLogDto/{requestId}/{userName}")]
        Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(string requestId,string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddCallLog")]
        Task AddCallLogAsync(AddCallLogDto addCallLogDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetCallLogDto/{RequestId}/{userName}")]
        Task<CallLogDto> GetCallLogAsync(string requestId,string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditCallLog")]
        Task EditCallLogAsync(EditCallLogDto editCallLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddAppointmentLog")]
        Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditAppointmentLogDto")]
        Task EditAppointmentLogAsync(EditAppointmentLogDto dto);

        [OperationContract]
        [WebGet(UriTemplate = "GetAppointmentLogDto/{requestId}/{userName}")]
        Task<AppointmentLogDto> GetAppointmentLogAsync(string requestId, string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNoticeLog")]
        Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditNoticeLogDto")]
        Task EditNoticeLogAsync(EditNoticeLogDto dto);
        [OperationContract]
        [WebGet(UriTemplate = "GetNoticeLogDto/{RequestId}/{UserName}")]
        Task<NoticeLogDto> GetNoticeLogAsync(string requestId, string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCurrentContractsCount/{userName}")]
        Task<CountAndSumDto> GetCurrentContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCurrentContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetDueDateContractsCount/{userName}")]
        Task<CountAndSumDto> GetDueDateContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetExpireContractsCount/{userName}")]
        Task<CountAndSumDto> GetExpireContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetDueDateContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetOneMonthToDueDateContractsCount/{userName}")]
        Task<CountAndSumDto> GetOneMonthToDueDateContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetBaddebtContractsCount/{userName}")]
        Task<CountAndSumDto> GetBaddebtContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetPostponedContractsCount/{userName}")]
        Task<CountAndSumDto> GetPostponedContractsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllDebtsContractsCount/{userName}")]
        Task<CountAndSumDto> GetAllDebtsCountAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetOneMonthToDueDateContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllDebtsContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetExpireContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetBadDebtContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetPostponedContracts/{userName}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeAddGivingAChanceLogDto")]
        Task<bool> CheckPrivilegeAddGivingAChanceLogAsync(AddGivingAChanceLogDto givingAChanceLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddGivingAChanceLogDto")]
        Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditGivingAChanceLogDto")]
        Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditRequestGivingAChanceLogDto")]
        Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editGivingAChanceLogDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetGivingAChanceLogDto/{RequestId}/{userName}")]
        Task<GivingAChanceLogDto> GetGivingAChanceLogAsync(string requestId, string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetRequestGivingAChanceLogDto/{RequestId}/{userName}")]
        Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceLogAsync(string requestId, string userName);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeAddImpunityForCrimesLogDto")]
        Task<bool> CheckPrivilegeAddImpunityLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddImpunityForCrimesLogDto")]
        Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetImpunityLogDto/{RequestId}/{userName}")]
        Task<ImpunityForCrimesLogDto> GetImpunityLogAsync(string requestId, string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetRequestImpunityForCrimesLogDto/{RequestId}/{userName}")]
        Task<RequestImpunityForCrimesLogDto> GetRequestImpunityLogAsync(string requestId, string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditImpunityForCrimesLogDto")]
        Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editGivingAChanceLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditRequestImpunityForCrimesLogDto")]
        Task EditRequestImpunityForCrimesLogAsync(EditRequestImpunityForCrimesLogDto editGivingAChanceLogDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetGuarantors/{customerDelinquentId}/{userName}")]
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(string userName, string customerDelinquentId);
        [OperationContract]
        [WebGet(UriTemplate = "GetBonds/{customerDelinquentId}/{userName}")]
        Task<IEnumerable<BondDto>> GetBondsAsync(string userName, string customerDelinquentId);
        [OperationContract]
        [WebGet(UriTemplate = "GetNotifications/{userName}")]
        Task<IEnumerable<NotificationDto>> GetNotificationsAsync(string userName);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateNotificationToSeenDto")]
        Task UpdateNotificationToSeenAsync(UpdateNotificationToSeenDto updateNotificationToSeenDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RemoveNotificationDto")]
        Task RemoveNotificationAsync(RemoveNotificationDto removeNotification);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RespondRequestGivingAChance")]
        Task RespondRequestGivingAChanceAsync(RespondRequestGivingAChanceDto respondRequestGivingAChanceDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RespondRequestImpunityForCrimes")]
        Task RespondRequestImpunityForCrimesAsync(RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto);
        [OperationContract]
        [WebGet(UriTemplate = "GetYesterDayLogs/{userName}")]
        Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerDelinquentHistory/{userName}/{persianDate}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(string userName,string persianDate);

        #region Clearing
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RespondRequestClearing")]
        Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddClearingLogDto")]
        Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetClearingLogDto/{ClearingRequestId}/{userName}")]
        Task<ClearingLogDto> GetClearingLogAsync(string clearingRequestId, string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetRequestClearingLogDto/{RequestId}/{userName}")]
        Task<RequestClearingLogDto> GetRequestClearingLogAsync(string requestId, string userName);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditClearingLogDto")]
        Task EditClearingLogAsync(EditClearingLogDto editClearingLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditRequestClearingLogDto")]
        Task EditRequestClearingLogAsync(EditRequestClearingLogDto editClearingLogDto);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeAddClearingLog")]
        Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetAllowEditToFalseForClearing")]
        Task SetAllowEditToFalseForClearingLogAsync(DisableClearingEditingDto disableClearingEditingDto);

        #endregion
        
        #region Renewal
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeAddRenewalLog")]
        Task<bool> CheckPrivilegeAddRenewalLogAsync(AddRenewalLogDto dto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddRenewalLogDto")]
        Task AddRenewalLogAsync(AddRenewalLogDto dto);

        [OperationContract]
        [WebGet(UriTemplate = "GetRenewalLogDto/{RenewalRequestId}/{userName}")]
        Task<RenewalLogDto> GetRenewalLogAsync(string renewalRequestId, string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeEditRenewalLog")]
        Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditRenewalLogDto")]
        Task EditRenewalLogAsync(EditRenewalLogDto dto);


        #endregion

        #region Split
        [OperationContract]
        [WebGet(UriTemplate = "GetSplitLogDto/{RequestId}/{userName}")]
        Task<SplitLogDto> GetSplitLogAsync(string requestId, string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetRequestSplitLogDto/{RequestId}/{userName}")]
        Task<RequestSplitLogDto> GetRequestSplitLogAsync(string requestId, string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddSplitLogDto")]
        Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditSplitLogDto")]
        Task EditSplitLogAsync(EditSplitLogDto editSplitLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditRequestSplitLogDto")]
        Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RespondRequestSplit")]
        Task RespondRequestSplitAsyncTask(RespondRequestSplitDto respondRequestSplitDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeAddSplitLog")]
        Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetAllowEditToFalseForSplit")]
        Task SetAllowEditToFalseForSplitLogAsync(DisableSplitEditingDto editSplitLogDto);

        #endregion

        #endregion

        #region ManagerRequest
        [OperationContract]
        [WebGet(UriTemplate = "GetAllBranchActivity/{userName}/{fromPersianDate}/{toPersianDate}")]
        Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(string userName,string fromPersianDate,string toPersianDate);
        [OperationContract]
        [WebGet(UriTemplate = "GetLastAbisUpdateDateAsync/{userName}")]
        Task<string> GetLastAbisUpdateDateAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetLastBankIranUpdateDateAsync/{userName}")]
        Task<string> GetLastBankIranUpdateDateAsync(string userName);
        [OperationContract]
        [WebGet(UriTemplate = "GetBranchContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetCountContractsByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetCountContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerLogsByBranchCode/{userName}/{BranchCode}/{customerDelinquentId}")]
        Task<IEnumerable<LogDto>> GetCustomerLogsByBranchCodeAsync(string userName, string branchCode,
            string customerDelinquentId);
        [OperationContract]
        [WebGet(UriTemplate = "GetCurrentContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetCurrentContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetDueDateContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetExpireContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetBaddebtContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetBaddebtContractsCountByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetPostponedContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetBadDebtContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetPostponedContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(string userName,
            string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetDueDateContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetExpireContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(string userName, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetGuarantorsByBranchCode/{userName}/{BranchCode}/{customerDelinquentId}")]
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(string userName, string branchCode,
            string customerDelinquentId);
        [OperationContract]
        [WebGet(UriTemplate = "GetBondsByBranchCode/{userName}/{BranchCode}/{customerDelinquentId}")]
        Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(string userName, string branchCode,
            string customerDelinquentId);
        [OperationContract]
        [WebGet(UriTemplate = "GetOneMonthToDueDateContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetOneMonthToDueDateContractsCountByBranchCodeAsync(string userName, string branchCode);
        
        [OperationContract]
        [WebGet(UriTemplate = "GetOneMonthToDueDateContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(string userName,
            string branchCode);
        
        [OperationContract]
        [WebGet(UriTemplate = "GetAllDebtsContractsCountByBranchCode/{userName}/{BranchCode}")]
        Task<CountAndSumDto> GetAllDebtsCountByBranchCodeAsync(string userName, string branchCode);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllDebtsContractsByBranchCode/{userName}/{BranchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(string userName,
            string branchCode);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeEditRequestClearingLogDto")]
        Task<bool> CheckPrivilegeEditRequestClearingLogAsync(GetRequestClearingLogDto getRequestClearingLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeEditRequestSplitLogDto")]
        Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeEditRequestImpunityForCrimesLogDto")]
        Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(GetRequestImpunityLogDto getRequestImpunityLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CheckPrivilegeEditRequestGivingAChanceLogDto")]
        Task<bool> CheckPrivilegeEditRequestGivingAChanceLogAsync(GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetAllowEditToFalseForImpunity")]
        Task SetAllowEditToFalseForImpunityLogAsync(DisableImpunityEditingDto disableImpunityEditingDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetAllowEditToFalseForGivingAChance")]
        Task SetAllowEditToFalseForGivingAChanceLogAsync(DisableGivingAChanceEditingDto disableGivingAChanceEditingDto);

        [OperationContract]
        [WebGet(UriTemplate = "GetBranchClaim/{userName}/{branchId}/{DateOnly}")]
        Task<BranchClaimDto> GetBranchClaim(string userName, string branchId, string dateOnly);
        [OperationContract]
        [WebGet(UriTemplate = "GetLastSevenDaysBranchClaims/{userName}/{branchId}")]
        Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(string userName, string branchId);
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerDelinquentHistoryByBranchCode/{userName}/{persianDate}/{branchCode}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(string userName,
            string persianDate, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllCustomerDelinquentsHistory/{userName}/{persianDate}")]
        Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(string userName,
            string persianDate);
        [OperationContract]
        [WebGet(UriTemplate = "GetBranchDelinquent/{userName}/{branchCode}/{persianFromDate}/{persianToDate}")]
        Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquent(string userName,string branchCode,string persianFromDate,string persianToDate);
        [OperationContract]
        [WebGet(UriTemplate = "GetBranchesDelinquent/{userName}/{persianFromDate}/{persianToDate}")]
        Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquent(string userName, string persianFromDate, string persianToDate);

        #endregion
    }
}