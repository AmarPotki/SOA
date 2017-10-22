using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Appointment;
using RahyabServices.Business.Dtos.Delinquent.Log.Call;
using RahyabServices.Business.Dtos.Delinquent.Log.Notice;
using RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Exceptions;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class LogService : ILogService{
        private readonly ICryptographer _cryptographer;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly INotificationRepository _notificationRepository;
        public LogService(ILogBaseRepository logBaseRepository, IDateTimeConvertor dateTimeConvertor, IHrFacade hrFacade,
            ICustomerDelinquentRepository customerDelinquentRepository, INotificationRepository notificationRepository,
            ICryptographer cryptographer){
            _logBaseRepository = logBaseRepository;
            _dateTimeConvertor = dateTimeConvertor;
            _hrFacade = hrFacade;
            _customerDelinquentRepository = customerDelinquentRepository;
            _notificationRepository = notificationRepository;
            _cryptographer = cryptographer;
        }
        public IEnumerable<LogDto> GetLogs(GetCustomerLogsDto getCustomerLogsDto){
            var logs = _logBaseRepository.GetLogs(getCustomerLogsDto.CustomerDelinquentId);
            return
                logs.Select(
                    logBase =>
                        new LogDto
                        {
                            Created = _dateTimeConvertor.GetPersianDate(logBase.Created),
                            Id = logBase.Id,
                            Type = logBase.GetType().Name,
                            FullName = logBase.Author == "spfarm"  || logBase.Author == "System" ? "سیستم" : _hrFacade.GetFullName(logBase.Author)
                        }).ToList();
        }
        public async Task<IEnumerable<LogDto>> GetLogsAsync(GetCustomerLogsDto getCustomerLogsDto){
            var logs = await _logBaseRepository.GetLogsAsync(getCustomerLogsDto.CustomerDelinquentId);
            return
                logs.Select(
                    logBase =>
                        new LogDto
                        {
                            Created = _dateTimeConvertor.GetPersianDate(logBase.Created),
                            Id = logBase.Id,
                            Type = logBase.GetType().Name,
                            FullName = logBase.Author == "spfarm" || logBase.Author == "System" ? "سیستم" : _hrFacade.GetFullName(logBase.Author)
                        }).ToList();
        }
        public void AddWrittenNoticeLog(AddWrittenNoticeLogDto addWrittenNoticeLogDto){
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addWrittenNoticeLogDto.AuthorUserName));
            var writtenNoticeLog = new WrittenNoticeLog
            {
                Author = personnelCode,
                LetterNumber = addWrittenNoticeLogDto.LetterNumber,
                Description = addWrittenNoticeLogDto.Description,
                WarningDate = addWrittenNoticeLogDto.WarningDate,
                WarningType = addWrittenNoticeLogDto.WarningType,
                CustomerDelinquentId = addWrittenNoticeLogDto.CustomerDelinquentId,
                Created = DateTime.Now,
                DocumentUrl = addWrittenNoticeLogDto.DocumentUrl
            };
            writtenNoticeLog.SetCustomerDelinquentId(addWrittenNoticeLogDto.CustomerDelinquentId);
            _logBaseRepository.Save(writtenNoticeLog);
        }

        public async Task EditWrittenNoticeLogAsync(EditWrittenNoticeLogDto dto){
            var log = Mapper.Map<EditWrittenNoticeLogDto, WrittenNoticeLog>(dto);
            await _logBaseRepository.SaveAsync(log);
        }

        public async Task<WrittenNoticeLogDto> GetWrittenNoticeLogAsync(GetWrittenNoticeLogDto dto){
            var log = await _logBaseRepository.OneAsync(dto.RequestId);
            var noticeLog = (WrittenNoticeLog)log;
            return new WrittenNoticeLogDto {Description = noticeLog.Description,LetterNumber = noticeLog.LetterNumber,WarningDate = noticeLog.WarningDate,WarningType = noticeLog.WarningType};        
        }
        public async Task AddWrittenNoticeLogAsync(AddWrittenNoticeLogDto addWrittenNoticeLogDto){
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(addWrittenNoticeLogDto.CustomerDelinquentId);
            var state = customerDelinquent.CurrentState;
            if (state is ThirdAnnounceState){
                var firstNoticeCallState = new FirstWrittenNoticeDtoStateHandler(DateTime.Now.Date.AddDays(10),
                    addWrittenNoticeLogDto);
                customerDelinquent.SetState(firstNoticeCallState.Id);
                await _customerDelinquentRepository.SaveAsync(customerDelinquent);
            }
            else if (state is FirstWrittenNoticeState){
                var secondNoticeCallState = new SecondWrittenNoticeStateHandler(DateTime.Now.Date.AddDays(10),
                    addWrittenNoticeLogDto);
                customerDelinquent.SetState(secondNoticeCallState.Id);
                await _customerDelinquentRepository.SaveAsync(customerDelinquent);
            }
            else if (state is SecondWrittenNoticeState){
                var thirdNoticeCallState = new ThirdWrittenNoticeStateHandler(DateTime.Now.Date.AddDays(10),
                    addWrittenNoticeLogDto);
                customerDelinquent.SetState(thirdNoticeCallState.Id);
                await _customerDelinquentRepository.SaveAsync(customerDelinquent);
            }
            else{
                throw new FaultException("در وضعیت فعلی تسهیلات شما قادر به ثبت اخطاریه نمی باشید");
            }
            var lastNotification =
                await
                    _notificationRepository.GetNotificationByTypeAndCustomerDelinquent(customerDelinquent.Id,
                        NotificationType.Call);
            if (lastNotification != null){
                lastNotification.IsDone = true;
                await _notificationRepository.SaveAsync(lastNotification);
            }
        }
        public void AddAppointmentLog(AddAppointmentLogDto addAppointmentLogDto){
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addAppointmentLogDto.AuthorUserName));
            var appointmentLog = new AppointmentLog
            {
                Author = personnelCode,
                Result = addAppointmentLogDto.Result,
                Address = addAppointmentLogDto.Address,
                CalledPersonType = addAppointmentLogDto.CalledPersonType,                
                PersonFullName = addAppointmentLogDto.PersonFullName,
                Created = DateTime.Now,
                AgentFullName = addAppointmentLogDto.AgentFullName,
                DateAction = addAppointmentLogDto.DateAction
            };
            appointmentLog.SetCustomerDelinquentId(addAppointmentLogDto.CustomerDelinquentId);
            _logBaseRepository.Save(appointmentLog);
        }
        public async Task AddAppointmentLogAsync(AddAppointmentLogDto addAppointmentLogDto){
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addAppointmentLogDto.AuthorUserName));
            var callLog = new AppointmentLog
            {
                Author = personnelCode,
                Result = addAppointmentLogDto.Result,
                Created = DateTime.Now,
                AgentFullName = addAppointmentLogDto.AgentFullName,
                DateAction = addAppointmentLogDto.DateAction
            };
            callLog.SetCustomerDelinquentId(addAppointmentLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(callLog);
            //var addAppointmentState = new AppointmentStateHandler(DateTime.Now.Date.AddDays(10), addAppointmentLogDto);
            //  customerDelinquent.SetState(addAppointmentState.Id);
            //  await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task EditAppointmentLogAsync(EditAppointmentLogDto dto){
            var log = Mapper.Map<EditAppointmentLogDto, AppointmentLog>(dto);
            await _logBaseRepository.SaveAsync(log);
        }
        public async Task<AppointmentLogDto> GetAppointmentLogAsync(GetAppointmentLogDto dto){
            var log = await _logBaseRepository.OneAsync(dto.RequestId);
            var noticeLog = (AppointmentLog)log;
            return new AppointmentLogDto {Address = noticeLog.Address,AgentFullName = noticeLog.AgentFullName,CalledPersonType = noticeLog.CalledPersonType,DateAction = noticeLog.DateAction,PersonFullName = noticeLog.PersonFullName,Result = noticeLog.Result};        
        }
        public async Task<NoticeLogDto> GetNoticeLogAsync(GetNoticeLogDto dto){
            var log =await _logBaseRepository.OneAsync(dto.RequestId);
            var noticeLog = (NoticeLog) log;
            return new NoticeLogDto {Description = noticeLog.Description,LetterDate = noticeLog.LetterDate,LetterNumber = noticeLog.LetterNumber};
        }       
        public async Task AddNoticeLog(AddNoticeLogDto addNoticeLogDto){
            var personnelCode = _hrFacade.GetPersonnelCode(addNoticeLogDto.AuthorUserName);
            var noticeLog = new NoticeLog
            {
                Author = personnelCode,
                Description = addNoticeLogDto.Description,
                Created = DateTime.Now,
                DocumentUrl = addNoticeLogDto.DocumentUrl,
                LetterDate = addNoticeLogDto.LetterDate,
                LetterNumber = addNoticeLogDto.LetterNumber
            };
            noticeLog.SetCustomerDelinquentId(addNoticeLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(noticeLog);
        }
        public async Task AddNoticeLogAsync(AddNoticeLogDto addNoticeLogDto){
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(addNoticeLogDto.CustomerDelinquentId);
            var state = new LetterStateHandler(addNoticeLogDto);
            customerDelinquent.SetState(state.Id);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task EditNoticeLogAsync(EditNoticeLogDto dto){
            var notice = Mapper.Map<EditNoticeLogDto, NoticeLog>(dto);
            await _logBaseRepository.SaveAsync(notice);
        }

        public async Task<NoticeLogDto> GetNoticeLogDto(GetNoticeLogDto dto){
            var log = await _logBaseRepository.OneAsync(dto.RequestId);
            var noticeLog = (NoticeLog)log;
            return new NoticeLogDto { Description = noticeLog.Description,LetterDate = noticeLog.LetterDate,LetterNumber = noticeLog.LetterNumber};
        
        }
        public async Task<bool> CheckHasARequestNotRespond(int customerDelinquentId){
            if (await _logBaseRepository.IsExistRequestClearingLog(customerDelinquentId)) return false;
            if (await _logBaseRepository.IsExistRequestGivinAChanceLog(customerDelinquentId)) return false;
            return !await _logBaseRepository.IsExistRequestSplitLog(customerDelinquentId);
        }
        public async Task<IEnumerable<LogDto>> GetLogsByBranchCodeAsync(
            GetCustomerLogsByBranchCodeDto getCustomerLogsByBranchCodeDto){
            var logs = await _logBaseRepository.GetLogsAsync(getCustomerLogsByBranchCodeDto.CustomerDelinquentId);
            return
                logs.Select(
                    logBase =>
                        new LogDto
                        {
                            Created = _dateTimeConvertor.GetPersianDate(logBase.Created),
                            Id = logBase.Id,
                            Type = logBase.GetType().Name,
                            FullName = logBase.Author == "spfarm" || logBase.Author == "System" ? "سیستم" : _hrFacade.GetFullName(logBase.Author)
                        }).ToList();
        }
        public async Task<CallLogDto> GetCallLogDto(GetCallLogDto callLogDto){
            var log = await _logBaseRepository.OneAsync(callLogDto.RequestId);
            var callLog = (CallLog) log;
            return new CallLogDto{AgentFullName = callLog.AgentFullName,CallDateTime = callLog.CallDateTime,CalledPersonType = callLog.CalledPersonType,PersonFullName = callLog.PersonFullName,Result = callLog.CallResult,Telephone = callLog.Telephone};
        }
        public async Task AddCallLogAsync(AddCallLogDto addCallLogDto){
            var callLog = Mapper.Map<AddCallLogDto, CallLog>(addCallLogDto);
            callLog.Created = DateTime.Now;
            callLog.Author = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addCallLogDto.AuthorUserName));
            await _logBaseRepository.SaveAsync(callLog);
        }
        public async Task EditCallLogAsync(EditCallLogDto editCallLogDto){            
            var callLog = Mapper.Map<EditCallLogDto, CallLog>(editCallLogDto);
            await _logBaseRepository.SaveAsync(callLog);
        }
        public async Task<IEnumerable<YesterdayLogDto>> GetYesterDayLogsAsync(GetYesterdayLogsDto getYesterdayLogsDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(getYesterdayLogsDto.UserName));
            var logs = await _logBaseRepository.GetYesterDayActions(branchCode, DateTime.Now.Date.AddDays(-1));
            return logs.Select(logBase => new YesterdayLogDto
            {
                Created = _dateTimeConvertor.GetPersianDate(logBase.Created),
                CustomerDelinquentId = logBase.CustomerDelinquentId,
                Type = logBase.GetType().Name,
                ContractCode = logBase.CustomerDelinquent.ContractCode,
                CustomerCode = logBase.CustomerDelinquent.CustomerNumber
            });
        }
    }
}