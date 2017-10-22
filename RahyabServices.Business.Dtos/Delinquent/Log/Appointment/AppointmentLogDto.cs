using System;
using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Appointment{
    public class AppointmentLogDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public DateTime DateAction { get; set; }
        [DataMember]
        public string AgentFullName { get; set; }
        [DataMember]
        public string CalledPersonType { get; set; }
        [DataMember]
        public string PersonFullName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Result { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}