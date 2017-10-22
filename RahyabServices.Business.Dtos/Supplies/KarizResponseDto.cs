using System.Collections.Generic;
using Newtonsoft.Json;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class KarizResponseDto:IDto{
        [JsonProperty("result")]
        public KarizResultDto Result { get; set; }
        [JsonProperty("cicsTransId")]
        public string CicsTransId { get; set; }
        [JsonProperty("seqNo")]
        public string SeqNo { get; set; }
        [JsonProperty("dataType")]
        public string DataType { get; set; }
        [JsonProperty("acknowledge")]
        public string Acknowledge { get; set; }
        [JsonProperty("msgnbr")]
        public string Msgnbr { get; set; }
        [JsonProperty("conditions")]
        public List<AccountConditionDto> Conditions { get; set; }
        public string UserName { get; set; }
    }
}