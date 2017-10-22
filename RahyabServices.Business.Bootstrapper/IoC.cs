using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using RahyabServices.Business.Contracts;
using RahyabServices.Business.Contracts.Implementations;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Implementations.Bank;
using RahyabServices.Common.Extensions;
using RahyabServices.DataAccess.Core;
namespace RahyabServices.Business.Bootstrapper{
    public class IoC{
        private static IContainer _container;
        private static ILifetimeScope _lifetimeScope;
        public static ILifetimeScope GetLifetimeScope(){
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope());
        }
        public static ILifetimeScope GetLifetimeScope(object tag){
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope(tag));
        }
        public static IContainer InitializeBusiness(){
            if (_lifetimeScope != null){
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
            var builder = new ContainerBuilder();
            builder.RegisterType<Bootstrapper>().AsSelf();
            var assemblies = Assemblies.GetBusinessAssemblies.ToArray();
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("StepHandler")).AsSelf();
            builder.RegisterType<AutoFacValidatorFactory>().As<IValidatorFactory>();

            //todo: Replace by 
            //todo: builder.RegisterAssemblyTypes(assemblies.Where(x => x.GetName().Name == "RahyabServices.Business.Contracts").ToArray())
            //todo: .Where(x => x.Name.EndsWith("Contract"))
            //todo:  .AsSelf();
            //Register Concrete Classes for WebHost
            builder.RegisterType<DelinquentCoreServiceContract>().AsSelf();
            builder.RegisterType<DelinquentServiceContract>().AsSelf();
            builder.RegisterType<DelinquentServiceRestContract>().AsSelf();
            builder.RegisterType<SuppliesRestContract>().AsSelf();
            builder.RegisterType<VipBankingRestContract>().AsSelf();
            builder.RegisterType<HrRestContract>().AsSelf();
            builder.RegisterType<TatRestContract>().AsSelf();
            builder.RegisterType<ParsLogicRestContract>().AsSelf();
            builder.RegisterType<MiscRestContract>().AsSelf();
            builder.RegisterType<MiscContract>().AsSelf();
            builder.RegisterType<FinanceRestContract>().AsSelf();
            builder.RegisterType<FaraFekRestContract>().AsSelf();
            builder.RegisterType<AdanicRestContract>().AsSelf();
            builder.RegisterType<BranchMarketingRestContract>().AsSelf();
            builder.RegisterType<AbsorbResourcesRestContract>().AsSelf();
            _container = builder.Build();
            return _container;
        }
        public static IContainer InitializeClient(Action<ContainerBuilder> additions = null, Assembly webAssembly = null){
            if (_lifetimeScope != null){
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
            var builder = new ContainerBuilder();
            if (webAssembly != null){
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
        public static class Assemblies{
            private static readonly Assembly Domain = Assembly.GetAssembly(typeof (IDelinquentEntity));
            private static readonly Assembly BusinnessDto = Assembly.GetAssembly(typeof (ToDayDelinquentDto));
            private static readonly Assembly Bootstrapper = Assembly.GetAssembly(typeof (IoC));
            private static readonly Assembly DataAccess = Assembly.GetAssembly(typeof (IDataContextFactory));
            private static readonly Assembly BusinessContracts = Assembly.GetAssembly(typeof (ContractBase));
            private static readonly Assembly BusinessServices = Assembly.GetAssembly(typeof (RPTFTDelinquentService));
            private static readonly Assembly FluentValidation = Assembly.GetAssembly(typeof (IValidator));
            private static readonly Assembly Facade = Assembly.GetAssembly(typeof (ISmsFacade));
            private static readonly Assembly Common = Assembly.GetAssembly(typeof (StringExtensions));
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