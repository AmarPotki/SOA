using System.Runtime.Serialization;

namespace RahyabServices.Common.Logging
{
    [DataContract]
    public class WebCallContextData
    {
        [DataMember]
        public string Machine { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string UrlReferrer { get; set; }
        [DataMember]
        public string BrowserType { get; set; }
        [DataMember]
        public string BrowserVersion { get; set; }
        [DataMember]
        public string ClientId { get; set; }
        [DataMember]
        public string AuthHash { get; set; }
        [DataMember]
        public string UserAgent { get; set; }
    }
}