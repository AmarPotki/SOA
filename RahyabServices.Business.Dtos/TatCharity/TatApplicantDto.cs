using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.TatCharity{
    [DataContract]
    public class TatApplicantDto : IDto{
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string NationalID { get; set; }
        [DataMember]
        public string FatherTitle { get; set; }
        [DataMember]
        public double FileNo { get; set; }
        [DataMember]
        public string Birthday { get; set; }
    }
}