using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog
{
    [DataContract]
    public class AddClearingLogDto : IDto
    {
        [DataMember]
        public int CustomerDelinquentId { get; set; }

        [DataMember]
        public string AuthorUserName { get; set; }

        [DataMember]
        public string DocumentUrl { get; set; }
        
        [DataMember]
        public DateTime LegislationDate { get; set; }

        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}