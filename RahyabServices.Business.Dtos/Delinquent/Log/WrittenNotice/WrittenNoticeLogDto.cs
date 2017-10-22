using System;
using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice{
    public class WrittenNoticeLogDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public string WarningType { get; set; }
        [DataMember]
        public DateTime WarningDate { get; set; }
        [DataMember]
        public string LetterNumber { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string DocumentUrl { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}