using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.TatCharity{
    [DataContract]
    public class TatPensionDto : IDto{
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public double PensionCount { get; set; }
        [DataMember]
        public double PensionPrice { get; set; }
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public string PaymentDate { get; set; }
    }
}