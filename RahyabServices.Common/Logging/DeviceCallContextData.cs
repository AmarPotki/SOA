using System.Runtime.Serialization;

namespace RahyabServices.Common.Logging
{
    [DataContract]
    public class DeviceCallContextData
    {
        public string OperatingSystem { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public bool IsSimulator { get; set; }
        public string AppVersion { get; set; }
    }
}