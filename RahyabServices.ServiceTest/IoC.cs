using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using RahyabServices.Business.Contracts;
using RahyabServices.Business.Contracts.Implementations;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Bank;
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
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Implementations.Bank;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Extensions;
using RahyabServices.DataAccess.Core;
using FluentValidation;
using NLog.Filters;
using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Dtos.VipBanking;

namespace RahyabServices.ServiceTest
{
    public class IoC
    {
        private static IContainer _container;
        private static ILifetimeScope _lifetimeScope;

        public static ILifetimeScope GetLifetimeScope()
        {
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope());
        }

        public static ILifetimeScope GetLifetimeScope(object tag)
        {
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope(tag));
        }

        public static IContainer InitializeBusiness()
        {
            if (_lifetimeScope != null)
            {
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
            ConfigureAutoMapper();
            var builder = new ContainerBuilder();
            builder.RegisterType<Bootstrapper>().AsSelf();

            var assemblies = Assemblies.GetBusinessAssemblies.ToArray();
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("StepHandler")).AsSelf();
            builder.RegisterType<AutoFacValidatorFactory>().As<IValidatorFactory>();

            //Register Concrete Classes for WebHost
            builder.RegisterType<DelinquentCoreServiceContract>().AsSelf();
            builder.RegisterType<DelinquentServiceContract>().AsSelf();
            builder.RegisterType<DelinquentServiceRestContract>().AsSelf();
            builder.RegisterType<VipBankingRestContract>().AsSelf();
            builder.RegisterType<ParsLogicRestContract>().AsSelf();

            _container = builder.Build();

            return _container;
        }
        public static void ConfigureAutoMapper(){
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Branch, BranchDto>()
                    .ForMember(x => x.BranchRate, br => br.MapFrom(t => (int)t.BranchRate));

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
                cfg.CreateMap<Business.Domain.Models.VipBanking.Potential, PotentialDto>();
                cfg.CreateMap<PersonInfo, PersonInfoDto>().ForMember(dest => dest.WorkSectionTitleFormated,
                    opts => opts.MapFrom(src => src.WorkSectionTitle.RemoveZerowidthNonjoiner())); 
                cfg.CreateMap<VipDelinquent, VipDelinquentDto>();
                cfg.CreateMap<Cheque, VipChequeDto>();
                cfg.CreateMap<Business.Domain.Models.VipBanking.GeneralReport, GeneralReportDto>();
                cfg.CreateMap<LastBalDetail, LastBalDetailDto>();
                cfg.CreateMap<FilterDto, Business.Domain.Kendo.Filter>();

                cfg.CreateMap<WorkSection, WorkSectionDto>();
            });

        }
        public static IContainer InitializeClient(Action<ContainerBuilder> additions = null, Assembly webAssembly = null)
        {
            if (_lifetimeScope != null)
            {
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
            var builder = new ContainerBuilder();

            if (webAssembly != null)
            {
                //builder.RegisterControllers(webAssembly);
                //builder.RegisterModelBinders(webAssembly);
                //builder.RegisterModelBinderProvider();
                //builder.RegisterFilterProvider();
                builder.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces();
                //builder.RegisterApiControllers(webAssembly);
            }

            builder.RegisterType<Bootstrapper>().AsSelf();

            _container = builder.Build();

            return _container;
        }

        public static class Assemblies
        {
            private static readonly Assembly Domain = Assembly.GetAssembly(typeof(IDelinquentEntity));
            private static readonly Assembly BusinnessDto = Assembly.GetAssembly(typeof(ToDayDelinquentDto));
            private static readonly Assembly Bootstrapper = Assembly.GetAssembly(typeof(IoC));
            private static readonly Assembly DataAccess = Assembly.GetAssembly(typeof(IDataContextFactory));
            private static readonly Assembly BusinessContracts = Assembly.GetAssembly(typeof(ContractBase));
            private static readonly Assembly BusinessServices = Assembly.GetAssembly(typeof(RPTFTDelinquentService));
            private static readonly Assembly FluentValidation = Assembly.GetAssembly(typeof(IValidator));
            private static readonly Assembly Facade = Assembly.GetAssembly(typeof(ISmsFacade));

            private static readonly Assembly Common = Assembly.GetAssembly(typeof(StringExtensions));

            public static IEnumerable<Assembly> GetBusinessAssemblies
            {
                get
                {
                    yield return Domain;
                    yield return Bootstrapper;
                    yield return BusinessServices;
                    yield return DataAccess;
                    yield return BusinessContracts;
                    yield return BusinnessDto;
                    yield return Common;
                    yield return FluentValidation;
                    yield return Facade;
                }
            }
        }
    }
}
