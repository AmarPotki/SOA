using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;

namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface ISmsService
    {
        Task SendSms(CustomerDelinquent customerDelinquent, TemplateType type);
        long SendSms(string mobileNumber, string messageBody);
        Task<bool> SendDayRemainSmsAsync();
        Task<bool> SendWeekRemainSmsAsync();
        Task<bool> SendMonthRemainSmsAsync();
        Task CheckAndUpdateSmsDeliveryStatusAsync();
    }
}