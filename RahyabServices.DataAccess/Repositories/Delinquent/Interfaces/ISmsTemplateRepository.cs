using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces{
    public interface ISmsTemplateRepository : IDelinquentRepository<SmsTemplate>{
        Task<SmsTemplate> GetSmsTemplateByTypeAsync(TemplateType type);
        SmsTemplate GetSmsTemplateByType(TemplateType type);
    }
}