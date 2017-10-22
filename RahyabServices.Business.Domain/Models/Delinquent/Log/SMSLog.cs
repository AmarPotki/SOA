using System;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class SmsLog : LogBase
    {
        public bool IsDelivered { get; set; }
        public long SmsId { get; set; }
        public SmsTemplate SmsTemplate { get; set; }
        public int SmsTemplateId { get; set; }

        public LogBase SetSmsTemplateId(int id)
        {
            SmsTemplateId = id;
            return this;
        }
    }
}