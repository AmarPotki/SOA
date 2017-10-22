using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.Adanic;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Exceptions;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Facades.Implementations{
    public class AdanicFacade : IAdanicFacade{
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private readonly TimeSpan _timeOut;
        public AdanicFacade(ILogger logger){
            _logger = logger;
            _baseUrl = "https://";
            _timeOut = new TimeSpan(0, 1, 0, 0);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = _timeOut
            };
        }
        public async Task<string> CallService(CallServiceDtq dtq){
            try{
                var url =
                    new Uri(_baseUrl + dtq.Name + "/" + dtq.Variables);
                var response = _client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                _logger.Trace(new FaultDto("AdanicFacade", response.ToString(), "", FaultSource.Endpoint));
                throw new FaultException("ارتباط با سرور مقصد امکان پذیر نیست");
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("AdanicFacade", ex.GetMessage(), ex.StackTrace, FaultSource.Endpoint));
                throw new FaultException("خطای پیش بینی نشده" + ex.Message);
            }
        }
    }
}