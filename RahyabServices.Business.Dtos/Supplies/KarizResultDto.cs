using Newtonsoft.Json;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class KarizResultDto:IDto{
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}