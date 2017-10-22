using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Dtos.Bank;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IDelinquentService{
        Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquent(GetBranchDelinquentDtq branchDelinquentDtq);
        Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquent(GetBranchesDelinquentDtq branchesDelinquentDtq);
    }
}