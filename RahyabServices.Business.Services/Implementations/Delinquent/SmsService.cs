using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;

namespace RahyabServices.Business.Services.Implementations.Delinquent
{
    public class SmsService : ISmsService
    {
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly ISmsTemplateRepository _smsTemplateRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly ISmsFacade _smsFacade;

        public SmsService(ICustomerDelinquentRepository customerDelinquentRepository, ISmsFacade smsFacade,
            ISmsTemplateRepository smsTemplateRepository, IDateTimeConvertor dateTimeConvertor,
            ICustomerInfoRepository customerInfoRepository, ILogBaseRepository logBaseRepository)
        {
            _customerDelinquentRepository = customerDelinquentRepository;
            _smsFacade = smsFacade;
            _smsTemplateRepository = smsTemplateRepository;
            _dateTimeConvertor = dateTimeConvertor;
            _customerInfoRepository = customerInfoRepository;
            _logBaseRepository = logBaseRepository;
        }

        public async Task SendSms(CustomerDelinquent customerDelinquent, TemplateType type)
        {
            var template =await _smsTemplateRepository.GetSmsTemplateByTypeAsync(type);
            var customerInfo = _customerInfoRepository.GetByCustomerNumber(customerDelinquent.CustomerNumber);
            //set smsID at LogBase
            var result = _smsFacade.Send(customerDelinquent.MobileNumber, FormatMessage(template, customerDelinquent, customerInfo));
           await _logBaseRepository.SaveAsync(new SmsLog { Author = "System", Created = DateTime.Now.Date, SmsId =result}.SetSmsTemplateId(template.Id).SetCustomerDelinquentId(customerDelinquent.Id));
        
        }

        public long SendSms(string mobileNumber, string messageBody)
        {
            return _smsFacade.Send(mobileNumber, messageBody);
        }

        public async Task<bool> SendDayRemainSmsAsync()
        {
            var targetContractsDaily = await _customerDelinquentRepository.GetContractsWithDesireRemainingTimeAsync(TimeBase.Day);

            foreach (var customerDelinquent in targetContractsDaily)
            {
                var template = await _smsTemplateRepository.GetSmsTemplateByTypeAsync(TemplateType.Week);
                var customerInfo = await _customerInfoRepository.GetByCustomerNumberAsync(customerDelinquent.CustomerNumber);

               // var result = _smsFacade.Send(customerDelinquent.MobileNumber, FormatMessage(template, customerDelinquent, customerInfo));
               // _logBaseRepository.Save(new SmsLog { Author = "spfarm", Created = DateTime.Now, SmsId = result }.SetSmsTemplateId(template.Id).SetCustomerDelinquentId(customerDelinquent.Id));
            }

            return true;
        }

        public async Task<bool> SendWeekRemainSmsAsync()
        {
            var targetContractsWeekly = await _customerDelinquentRepository.GetContractsWithDesireRemainingTimeAsync(TimeBase.Week);

            foreach (var customerDelinquent in targetContractsWeekly)
            {
                var template = await _smsTemplateRepository.GetSmsTemplateByTypeAsync(TemplateType.Week);
                var customerInfo = await _customerInfoRepository.GetByCustomerNumberAsync(customerDelinquent.CustomerNumber);

               //  var result =_smsFacade.Send(customerDelinquent.MobileNumber, FormatMessage(template, customerDelinquent, customerInfo));
              //  _logBaseRepository.Save(new SmsLog { Author = "spfarm", Created = DateTime.Now, SmsId = result }.SetSmsTemplateId(template.Id).SetCustomerDelinquentId(customerDelinquent.Id));
            }

            return true;
        }

        public async Task<bool> SendMonthRemainSmsAsync()
        {
            var targetContractsMonthly = await _customerDelinquentRepository.GetContractsWithDesireRemainingTimeAsync(TimeBase.Month);

            foreach (var customerDelinquent in targetContractsMonthly)
            {
                var template = await _smsTemplateRepository.GetSmsTemplateByTypeAsync(TemplateType.Month);
                var customerInfo = await _customerInfoRepository.GetByCustomerNumberAsync(customerDelinquent.CustomerNumber);

               //  var result = _smsFacade.Send(customerDelinquent.MobileNumber, FormatMessage(template, customerDelinquent, customerInfo));
              //  _logBaseRepository.Save(new SmsLog { Author = "spfarm", Created = DateTime.Now, SmsId = result }.SetSmsTemplateId(template.Id).SetCustomerDelinquentId(customerDelinquent.Id));
            }

            return true;
        }

        public async Task CheckAndUpdateSmsDeliveryStatusAsync()
        {
            var sentSmses = await _logBaseRepository.GetPendingSmsesAsync(TimeSpan.FromDays(7));
            foreach (var smsLog in sentSmses)
            {
                smsLog.IsDelivered = _smsFacade.IsDelivered(smsLog.SmsId);
                await _logBaseRepository.SaveAsync(smsLog);
            }
        }

        private string FormatMessage(SmsTemplate template, CustomerDelinquent customerDelinquent, CustomerInfo customerInfo){
            var finalMessage = template.MessageBody.Replace("{0}", customerDelinquent.ContractCode)
                .Replace("{1}", _dateTimeConvertor.GetPersianDate(customerDelinquent.MaturityDate));
            return finalMessage;
        }
    }
}