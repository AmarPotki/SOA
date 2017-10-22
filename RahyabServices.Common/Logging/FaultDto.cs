using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace RahyabServices.Common.Logging
{
    [DataContract]
    public class FaultDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        [ScriptIgnore]
        public FaultSource FaultSource { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public WebCallContextData WebCallContextData { get; set; }

        public EndpointCallContextData Endpoint { get; set; }

        [DataMember]
        public DeviceCallContextData DeviceCallContextData { get; set; }

        public string FaultSourceName
        {
            get
            {
                return Enum.GetName(typeof(FaultSource), FaultSource);
            }
        }

        public FaultDto()
        {
            Id = Guid.NewGuid();
        }

        public FaultDto(string location, string message, string stackTrace, FaultSource faultSource)
        {
            Id = Guid.NewGuid();
            StackTrace = stackTrace;
            Message = message;
            FaultSource = faultSource;
            Location = location;
        }

        public FaultDto(string location, string message, FaultSource faultSource)
        {
            Id = Guid.NewGuid();
            Message = message;
            FaultSource = faultSource;
            Location = location;
        }

        public string ToJsonString()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}