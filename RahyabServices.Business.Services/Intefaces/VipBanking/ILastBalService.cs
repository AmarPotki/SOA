using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking{
    public interface ILastBalService{
        Task<IEnumerable<LastBalDetailDto>> GetThirtyLastBal(GetThirtyLastBalDtq getThirtyLastBalDtq);
    }
}