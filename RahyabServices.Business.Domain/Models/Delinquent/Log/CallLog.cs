using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class CallLog:LogBase{
        [Column("CallResult")]
        public string CallResult { get; set; }
        [Column("Telephone")]
        public string Telephone { get; set; }
        [Column("PersonFullName")]
        public string PersonFullName { get; set; }
        [Column("AgentFullName")]
        public string AgentFullName { get; set; }
        [Column("CalledPersonType")]
        public string CalledPersonType { get; set; }
        [Column("CallDateTime")]
        public DateTime CallDateTime { get; set; }
    }
}