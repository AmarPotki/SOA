using System.Runtime.Serialization;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Dto;
namespace RahyabServices.Common.Dto{
    [DataContract]
    public class SharepointRequestDto : IDto{
        public SharepointRequestDto()
        {
            Info = new CryptoInfo();
        }
        public CryptoInfo Info { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string SiteCollection { get; set; }
    }
}