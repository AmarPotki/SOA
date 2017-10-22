using System.Collections.Generic;
using Newtonsoft.Json;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
  
    public class AccountConditionDto:IDto{
        [JsonProperty("alertCode")]
        public string AlertCode { get; set; }
        [JsonProperty("messageOut")]
        public string MessageOut { get; set; }
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
        [JsonProperty("customerSurname")]
        public string CustomerSurname { get; set; }
        [JsonProperty("conditionNo")]
        public string ConditionNo { get; set; }
        [JsonProperty("conditionDescriptions")]
        public string ConditionDescriptions { get; set; }
        [JsonProperty("conditionLimit")]
        public string ConditionLimit { get; set; }
        [JsonProperty("conditionMinSignatures")]
        public string ConditionMinSignatures { get; set; }
        [JsonProperty("conditionExpireDate")]
        public string ConditionExpireDate { get; set; }
        [JsonProperty("lastConditionNo")]
        public string LastConditionNo { get; set; }
        [JsonProperty("withdrawIndividually")]
        public string WithdrawIndividually { get; set; }
        [JsonProperty("signers")]
        public List<AccountSignerDto> Signers { get; set; }
        public string UserName { get; set; }
    }
}