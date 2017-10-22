using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Supplies;
namespace RahyabServices.Business.Facades.Interfaces{
    public interface IKarizFacade{
        Task<KarizResponseDto> GetInfomationFromChannel(string account);
    }
}