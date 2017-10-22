using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Call{
    [DataContract]
    public class AddCallLogDto : IDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }

        [DataMember]
        public string AgentFullName { get; set; }
        [DataMember]
        public string CalledPersonType { get; set; }
        [DataMember]
        public string PersonFullName { get; set; }
        [DataMember]
        public DateTime CallDateTime { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public string CallResult { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        public string UserName{
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}