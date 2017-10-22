using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Exceptions;
namespace RahyabServices.Business.Facades.Implementations{
    public class KarizFacade : IKarizFacade{
        public async Task<KarizResponseDto> GetInfomationFromChannel(string account){
            var channel = "";
            var secret = "";
            var baseUrl = "https://";
            var strhash = channel + ";account-conditions;" + account + ";" + secret;
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(strhash), 0, Encoding.UTF8.GetByteCount(strhash));
            foreach (var theByte in crypto) { hash.Append(theByte.ToString("x2")); }
            try{
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var url =
                    new Uri(baseUrl + "accounts/" + account + "?channel=" + channel +
                            "&conditionNo=00&client=10.100.129.66&hash=" + hash);
                var client = new HttpClient {BaseAddress = new Uri(baseUrl)};
                var response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode) throw new FaultException("مشکل در اطلاعات کاریز");
                var result = await response.Content.ReadAsStringAsync();
                   
                return JsonConvert.DeserializeObject<KarizResponseDto>(result);
            }
            catch (Exception ex) {
                throw new FaultException("مشکل در اطلاعات کاریز :" + ex.Message);
            }
        }
    }
}