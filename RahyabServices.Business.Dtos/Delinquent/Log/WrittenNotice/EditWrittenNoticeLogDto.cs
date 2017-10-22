using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice{
    [DataContract]
    public class EditWrittenNoticeLogDto : IDto
    {
        [DataMember]
        public int Id { get; set; }
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
        
        public string UserName{
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}