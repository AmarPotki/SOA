using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Appointment;
using RahyabServices.Business.Dtos.Delinquent.Log.Call;
using RahyabServices.Business.Dtos.Delinquent.Log.Notice;
using RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice;
namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface ILogService
    {
        IEnumerable<LogDto> GetLogs(GetCustomerLogsDto getCustomerLogsDto);
        Task<IEnumerable<LogDto>> GetLogsAsync(GetCustomerLogsDto getCustomerLogsDto);
        void AddWrittenNoticeLog(AddWrittenNoticeLogDto addWrittenNoticeLogDto);
        Task AddWrittenNoticeLogAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto);
        Task EditWrittenNoticeLogAsync(EditWrittenNoticeLogDto dto);
        Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(GetWrittenNoticeLogDto dto);
        void AddAppointmentLog(AddAppointmentLogDto addAppointmentLogDto);
        Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto);
        Task EditAppointmentLogAsync(EditAppointmentLogDto dto);
        Task<AppointmentLogDto> GetAppointmentLogAsync(GetAppointmentLogDto dto);
        Task AddNoticeLog(AddNoticeLogDto addNoticeLogDto);
        Task<NoticeLogDto> GetNoticeLogAsync(GetNoticeLogDto dto);
        Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto);
        Task EditNoticeLogAsync(EditNoticeLogDto dto);
        Task<bool> CheckHasARequestNotRespond(int customerDelinquentId);
        Task<IEnumerable<LogDto>> GetLogsByBranchCodeAsync(GetCustomerLogsByBranchCodeDto getCustomerLogsByBranchCodeDto);
        Task<CallLogDto> GetCallLogDto(GetCallLogDto callLogDto);
        Task AddCallLogAsync(AddCallLogDto addCallLogDto);
        Task EditCallLogAsync(EditCallLogDto editCallLogDto);
        Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(GetYesterdayLogsDto getYesterdayLogsDto);
    }
}