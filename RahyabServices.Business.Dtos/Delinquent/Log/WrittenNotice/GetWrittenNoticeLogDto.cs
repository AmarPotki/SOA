using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice{
    public class GetWrittenNoticeLogDto{
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}