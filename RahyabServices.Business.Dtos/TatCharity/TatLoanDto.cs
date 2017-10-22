using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.TatCharity{
    [DataContract]
    public class TatLoanDto : IDto{
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public double InstallmentCount { get; set; }
        [DataMember]
        public double InstallmentPrice { get; set; }
        [DataMember]
        public string InstallmentStartDate { get; set; }
        [DataMember]
        public string InstallmentDueDate { get; set; }
    }
}