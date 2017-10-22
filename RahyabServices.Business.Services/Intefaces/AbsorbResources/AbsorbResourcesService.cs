using System.Threading.Tasks;
using RahyabServices.Business.Dtos.AbsorbResources;
namespace RahyabServices.Business.Services.Intefaces.AbsorbResources{
    public interface IAbsorbResourcesService{
        Task<CustomerInformationDto> GetBrifCustomerInformation(GetCustomerInformationDtq customerInformationDto);
    }
}