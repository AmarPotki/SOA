using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Supplies;
using RahyabServices.DataAccess.Core.Supplies;
namespace RahyabServices.DataAccess.Repositories.Supplies.Interfaces{
    public interface IIranNaraChequeRequestRepository :IIRanNaraRepository<IranNaraChequeRequest>{
         int SaveAdo(IranNaraChequeRequest instance);
    }
}
