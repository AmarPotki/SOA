using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.FaraFek;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Exceptions;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Facades.Implementations
{
    public class FaraFekFacade : IFaraFekFacade
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private string _baseUrl;
        public FaraFekFacade(ILogger logger)
        {
            _logger = logger;
            _baseUrl = "https://";
            var timeOut = new TimeSpan(0, 1, 0, 0);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = timeOut
            };
        }
        public async Task<string> Diagram(string name)
        {
            return await Get(name);
        }
        public async Task<string> Diagram(GetDiagramPostDtq dtq)
        {
            return await Post(dtq.Name, dtq.Keys, dtq.Values);
        }
        public async Task<string> GetInfo()
        {
            return await Get("getInfo");
        }
        public async Task<string> GetInfo(GetInfoPostDtq dtq)
        {
            return await Post("getInfo", dtq.Keys, dtq.Values);
        }
        public async Task<string> GetPlotCsv()
        {
            return await Get("getPlotCSV");
        }
        public async Task<string> GetPlotCsv(GetPlotCsvPostDtq getPlotCsvPostDtq)
        {
            return await Post("getPlotCSV", getPlotCsvPostDtq.Keys, getPlotCsvPostDtq.Values);
        }
        public async Task<string> GetDashbordHtml()
        {
            return await Get("");
        }
        public async Task<string> GetDashbordData()
        {
            return await Get("data");
        }
        public void SetNewUrl(string url)
        {
            _baseUrl = url;
        }
        private async Task<string> Post(string name, string[] keys, string[] values)
        {
            try
            {
                var url =
                    new Uri(_baseUrl + name);
                var content =
                    new FormUrlEncodedContent(keys.Select((t, i) => new KeyValuePair<string, string>(t, values[i])));
                var response = _client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                _logger.Trace(new FaultDto("FaraFekFacade", response.ToString(), "", FaultSource.Endpoint));
                throw new FaultException("ارتباط با سرور مقصد امکان پذیر نیست");
            }
            catch (Exception ex)
            {
                _logger.Error(new FaultDto("FaraFekFacade", ex.GetMessage(), ex.StackTrace, FaultSource.Endpoint));
                throw new FaultException("خطای پیش بینی نشده :" + ex.Message);
            }
        }
        private async Task<string> Get(string name)
        {
            try
            {
                var url =
                    new Uri(_baseUrl + name);
                var response = _client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                _logger.Trace(new FaultDto("FaraFekFacade", response.ToString(), "", FaultSource.Endpoint));
                throw new FaultException("ارتباط با سرور مقصد امکان پذیر نیست");
            }
            catch (Exception ex)
            {
                _logger.Error(new FaultDto("FaraFekFacade", ex.GetMessage(), ex.StackTrace, FaultSource.Endpoint));
                throw new FaultException("خطای پیش بینی نشده" + ex.Message);
            }
        }
    }
}