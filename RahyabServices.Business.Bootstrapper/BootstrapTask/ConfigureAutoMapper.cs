using AutoMapper;
using RahyabServices.Business.Domain.Kendo;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Delinquent.BranchClaim;
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
using RahyabServices.Business.Dtos.Finance;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Dtos.TatCharity;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Extensions;
using Filter = NLog.Filters.Filter;
namespace RahyabServices.Business.Bootstrapper.BootstrapTask{
    public class ConfigureAutoMapper : IBootstrapTask
    {
        public void Execute(){
            Mapper.Initialize(cfg =>{
                cfg.CreateMap<Branch, BranchDto>()
                    .ForMember(x => x.BranchRate, br => br.MapFrom(t => (int) t.BranchRate));

                cfg.CreateMap<RegisterStateHandler, RegisterState>().ReverseMap();
                cfg.CreateMap<FirstAnnounceStateHandler, FirstAnnounceState>().ReverseMap();
                cfg.CreateMap<SecondAnnounceStateHandler, SecondAnnounceState>().ReverseMap();
                cfg.CreateMap<ThirdAnnounceState, ThirdAnnounceStateHandler>().ReverseMap();
                cfg.CreateMap<FirstWrittenNoticeDtoStateHandler, FirstWrittenNoticeState>().ReverseMap();
                cfg.CreateMap<SecondWrittenNoticeStateHandler, SecondWrittenNoticeState>().ReverseMap();
                cfg.CreateMap<ThirdWrittenNoticeStateHandler, ThirdWrittenNoticeState>().ReverseMap();
                cfg.CreateMap<AppointmentState, AppointmentStateHandler>().ReverseMap();
                cfg.CreateMap<LetterState, LetterStateHandler>().ReverseMap();
                //
                cfg.CreateMap<AddGivingAChanceLogDto, GivingAChanceLog>();
                cfg.CreateMap<AddGivingAChanceLogDto, GivingAChanceLogList>();
                cfg.CreateMap<GivingAChanceStateHandler, GivingAChanceState>();
                cfg.CreateMap<AddGivingAChanceLogDto, RequestGivingAChanceLog>();
                cfg.CreateMap<RequestGivingAChanceStateHandler, RequestGivingAChanceState>();
                cfg.CreateMap<RequestGivingAChanceLog, GivingAChanceLog>();
                cfg.CreateMap<GivingAChanceStateHandler, GivingAChanceState>();
              //
                cfg.CreateMap<ClearingState, ClearingStateHandler>().ReverseMap();
                cfg.CreateMap<AddClearingLogDto, ClearingLog>();
                cfg.CreateMap<AddClearingLogDto, ClearingLogList>();
                cfg.CreateMap<AddClearingLogDto, RequestClearingLog>();
                cfg.CreateMap<RequestClearingStateHandler, RequestClearingState>();
                cfg.CreateMap<RequestClearingLog, ClearingLog>();
                cfg.CreateMap<ClearingStateHandler, ClearingState>();
                cfg.CreateMap<EditClearingLogDto, ClearingLog>();
                cfg.CreateMap<EditRequestClearingLogDto, RequestClearingLog>();
                cfg.CreateMap<EditSplitLogDto, SplitLog>();
                cfg.CreateMap<EditImpunityForCrimesLogDto, ImpunityForCrimesLog>();
                cfg.CreateMap<EditGivingAChanceLogDto, GivingAChanceLog>();
                cfg.CreateMap<EditRequestSplitLogDto, RequestSplitLog>();
                cfg.CreateMap<AddImpunityForCrimesLogDto, ImpunityForCrimesLog>();
                cfg.CreateMap<ImpunityForCrimesStateHandler, ImpunityForCrimesState>().ReverseMap();
                cfg.CreateMap<AddImpunityForCrimesLogDto, RequestImpunityForCrimesLog>();
                cfg.CreateMap<AddImpunityForCrimesLogDto, ImpunityLogList>();
                cfg.CreateMap<RequestImpunityForCrimesStateHandler, RequestImpunityForCrimesState>();
                cfg.CreateMap<RequestImpunityForCrimesLog, ImpunityForCrimesLog>();
                cfg.CreateMap<RequestImpunityForCrimesLog, ImpunityForCrimesLog>();
                cfg.CreateMap<AddSplitLogDto, SplitLog>();
                cfg.CreateMap<AddSplitLogDto, RequestSplitLog>();
                cfg.CreateMap<AddSplitLogDto, SplitLogList>();
                cfg.CreateMap<EditRequestSplitLogDto, SplitLogList>();
                cfg.CreateMap<SplitStateHandler, SplitState>().ReverseMap();
                cfg.CreateMap<RequestSplitStateHandler, RequestSplitState>().ReverseMap();
                cfg.CreateMap<Notification, NotificationDto>();
                cfg.CreateMap<RequestSplitLog, SplitLog>();
                cfg.CreateMap<RenewalStateHandler, RenewalState>();
                cfg.CreateMap<AddCallLogDto, CallLog>();
                cfg.CreateMap<EditCallLogDto, CallLog>();
                cfg.CreateMap<EditWrittenNoticeLogDto, WrittenNoticeLog>();
                cfg.CreateMap<EditAppointmentLogDto, AppointmentLog>();
                cfg.CreateMap<EditNoticeLogDto, NoticeLog>();
                cfg.CreateMap<EditRenewalLogDto, RenewalLog>();
                cfg.CreateMap<AddRenewalLogDto, RenewalLog>();
                cfg.CreateMap<BranchDelinquentReport, BranchDelinquentDto>();
                cfg.CreateMap<BranchDelinquentReport, BranchesDelinquentDto>();

                var dateConvertor = new DateTimeConvertor();
                cfg.CreateMap<BranchClaim, BranchClaimDto>().ForMember(dest => dest.Created,
                    opts => opts.MapFrom(src => dateConvertor.GetPersianDate(src.Created)));
                
                // supplies
                cfg.CreateMap<KarizResponseDto, AccountInformationDto>();
                cfg.CreateMap<AccountInformationDto, KarizResponseDto>();
                                        cfg.CreateMap<Vip, VipDto>();
                cfg.CreateMap<Potential, PotentialDto>();
               
                cfg.CreateMap<VipDelinquent, VipDelinquentDto>();
                cfg.CreateMap<Cheque, VipChequeDto>();
                cfg.CreateMap<GeneralReport, GeneralReportDto>();
                cfg.CreateMap<LastBalDetail, LastBalDetailDto>();
                cfg.CreateMap<FilterDto,Domain.Kendo.Filter>();
                cfg.CreateMap<SortDto,Sort >();

                // Tat Charity
                cfg.CreateMap<TatApplicant, TatApplicantDto>();
                cfg.CreateMap<AddPortalFundDtc, PortalLoanFunds>();
                cfg.CreateMap<AddPortalPensionFundDtc, PortalPensionFunds>();
                //cfg.CreateMap<AddTatFundDtc, TatLoanFunds>()
                //    .AfterMap((src, dest) => dest.Loan.ID = src.LoanId)
                //   .AfterMap((src, dest) => dest.Applicant.ID = src.ApplicantId);

                //HR
                cfg.CreateMap<PersonInfo, PersonInfoDto>()
                .ForMember(dest => dest.WorkSectionTitleFormated,
                    opts => opts.MapFrom(src => src.WorkSectionTitle.RemoveZerowidthNonjoiner()));
                cfg.CreateMap<WorkSection,WorkSectionDto>();

                //financial
                cfg.CreateMap<DailyliquidityReport, DailyliquidityReportDto>().ForMember(dest => dest.PersianDate,
                    opts => opts.MapFrom(src => dateConvertor.GetPersianDate(src.Date))).ForMember(dest => dest.Amount,
                    opts => opts.MapFrom(src => src.Value)).ForMember(dest => dest.Description,
                    opts => opts.MapFrom(src => src.Description));

            });
        }
        public int Priority => 3;
    }
}