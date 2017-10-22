using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Notice
{
    public class NoticeLogDto:IDto
    {
        [DataMember]
        public string LetterNumber { get; set; }

        [DataMember]
        public int CustomerDelinquentId { get; set; }

        [DataMember]
        public string AuthorUserName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DocumentUrl { get; set; }

        [DataMember]
        public DateTime LetterDate { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}
