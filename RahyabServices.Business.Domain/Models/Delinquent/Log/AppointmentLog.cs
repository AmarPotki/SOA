using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class AppointmentLog : LogBase
    {
        [Column("DateAction")]
        public DateTime DateAction { get; set; }
        [Column("Result")]
        public string Result { get; set; }
        [Column("AgentFullName")]
        public string AgentFullName { get; set; }
        [Column("CalledPersonType")]
        public string CalledPersonType { get; set; }
        [Column("PersonFullName")]
        public string PersonFullName { get; set; }
        [Column("Address")]
        public string Address { get; set; }
    }
}