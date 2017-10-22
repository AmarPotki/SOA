using RahyabServices.Business.Domain.Models.Delinquent.Types;

namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public class SmsTemplate : IDelinquentEntity
    {
        public int Id { get; set; }
        public TemplateType Type { get; set; }
        public string MessageBody { get; set; }
    }
}