using Newtonsoft.Json;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class AccountSignerDto:IDto{
        [JsonProperty("cif")]
        public string Cif { get; set; }
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
        [JsonProperty("customerSurname")]
        public string CustomerSurname { get; set; }
        [JsonProperty("signSerial")]
        public string SignSerial { get; set; }
        [JsonProperty("andFlag")]
        public string AndFlag { get; set; }
        public string UserName { get; set; }
        public string Identifier { get; set; }
        public string IdNum { get; set; }
        public int BirthDate { get; set; }
        public string CityCode { get; set; }
        public IdentifierType IdentifierType { get; set; }
    }
}