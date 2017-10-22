using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Services.Intefaces.Bank{
    public interface IRPTFTDelinquentService{
        Task<ToDayDelinquentDto> GetToDayDelinquentAsync(GetToDayDelinquentDto getAccountDto);
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(GetGuarantorsDto getGuarantorsDto);
        Task<IEnumerable<BondDto>> GetBondsAsync(GetBondsDto getBondsDto);
        Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(GetGuarantorsByBranchCodeDto getGuarantorsByBranchCodeDto);
        Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(GetBondsByBranchCodeDto getBondsByBranchCodeDto);
    }
}