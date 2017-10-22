using System;
using System.ServiceModel;
using System.Threading.Tasks;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
using FluentValidation;
namespace RahyabServices.Business.Contracts.Implementations
{
    public class DelinquentCoreServiceContract : ContractBase, IDelinquentCoreServiceContract
    {
        private readonly IDailyDelinquentUpdateService _dailyDelinquentUpdateService;
        private readonly ISmsService _smsService;
        private readonly IStateOperationService _stateOperationService;
        private readonly IHrFacade _hrFacade;
        private readonly ISharepointAuthorizationService _sharepointAuthorizationService;
        public DelinquentCoreServiceContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, IDailyDelinquentUpdateService dailyDelinquentUpdateService, ISmsService smsService,
            IStateOperationService stateOperationService , ISharepointAuthorizationService sharepointAuthorizationService) :
            base(validatorFactory, cryptographer, logger, sharepointAuthorizationService)
        {
            _dailyDelinquentUpdateService = dailyDelinquentUpdateService;
            _smsService = smsService;
            _stateOperationService = stateOperationService;

            _sharepointAuthorizationService = sharepointAuthorizationService;
        }

        public async Task UpdateWorkingCopyOfDatabaseAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _dailyDelinquentUpdateService.UpdateWorkingCopyOfDatabaseAsync();
                result.Message = (result.Result is bool && (bool)result.Result) ? "Operation Compeleted Successfully" : "An error has occured.\nSee error log for more information";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }
        public async Task UpdateWorkingCopyOfAbisDatabaseAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _dailyDelinquentUpdateService.UpdateWorkingCopyOfAbisDatabaseAsync(DateTime.Now.Date.AddDays(-2));
                result.Message = (result.Result is bool && (bool)result.Result) ? "Operation Compeleted Successfully" : "An error has occured.\nSee error log for more information";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task UpdateWorkingCopyOfBankIranDatabaseAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _dailyDelinquentUpdateService.UpdateWorkingCopyOfBankIranDatabaseAsync(DateTime.Now.Date.AddDays(-2));
                result.Message = (result.Result is bool && (bool)result.Result) ? "Operation Compeleted Successfully" : "An error has occured.\nSee error log for more information";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task UpdateWorkingCopyOfGuaranteeDatabaseAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _dailyDelinquentUpdateService.UpdateWorkingCopyOfGuaranteeDatabaseAsync(DateTime.Now.Date.AddDays(-1));
                result.Message = (result.Result is bool && (bool)result.Result) ? "Operation Compeleted Successfully" : "An error has occured.\nSee error log for more information";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task UpdateBranchClaim()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                await _dailyDelinquentUpdateService.UpdateBranchClaim();
                result.Result = true;
                result.Message = ((bool)result.Result) ? "Operation Compeleted Successfully" : "An error has occured.\nSee error log for more information";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        //public async Task SendSmsAsync(string number, string message)
        //{
        //    var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
        //    var result = new CallbackEventArgs();

        //    try
        //    {
        //        result.Result = _smsService.SendSms("09126488794", TemplateType.Week);
        //        result.Message = "SMS Sent Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Result = false;
        //        result.Message = ex.Message;
        //    }

        //    callback.Callback(result);
        //}

        public async Task SendDayRemainSmsAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _smsService.SendDayRemainSmsAsync();
                result.Message = "SMS Sent Successfully";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task SendWeekRemainSmsAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _smsService.SendWeekRemainSmsAsync();
                result.Message = "SMS Sent Successfully";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task SendMonthRemainSmsAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                result.Result = await _smsService.SendMonthRemainSmsAsync();
                result.Message = "SMS Sent Successfully";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task CheckAndUpdateSmsDeliveryStatusAsync()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                await _smsService.CheckAndUpdateSmsDeliveryStatusAsync();
                result.Result = true;
                result.Message = "SMS Status Checked Successfully";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }

        public async Task CheckExpireDateAndHandleOperationAsync(DateTime date, BankType bankType)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IDelinquentCoreServiceCallback>();
            var result = new CallbackEventArgs();

            try
            {
                await _stateOperationService.CheckExpireDateAndHandleOperationAsync(date);
                result.Result = true;
                result.Message = "SMS && Call States Checked Successfully";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }

            callback.Callback(result);
        }
    }
}
