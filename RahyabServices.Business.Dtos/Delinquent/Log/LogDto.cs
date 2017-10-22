using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log
{
    [DataContract]
    public class LogDto : IDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Created { get; set; }
        [DataMember]
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}