using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog{
    [DataContract]
    public class RemoveClearingLogDto:IDto{
        [DataMember]
        public int ClearingLogId { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}