using System;
using System.Threading.Tasks;
using Autofac;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.BranchMarketing;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.BranchMarketing;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
using RahyabServices.ServiceTest.VipBankingRest;
namespace RahyabServices.ServiceTest{
    internal class Program{
        private static IContainer _container;
        private static void Main(string[] args){
            Start();
            //CallResetService();
            RunAsync().Wait();
        }
        private static void CallResetService(){
            var dc = new VipBankingRestContractClient();
            //  var tt = dc.GetAll("", "0", "10");
        }
        private static async Task RunAsync(){
            try{
               
            }
            catch (Exception ex) {
                var tt = ex.Message;
            }
        }
        //private static
        private static void Start(){
            var container = IoC.InitializeBusiness();
            // AutofacHostFactory.Container = container;
            _container = container;
            Bootstrapper.ConfigureApplication(container);
        }
    }
}