using System.Collections.Generic;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Interfaces{
    public interface ITatLoanListRepository : ISharepointRepository<TatLoan>{
        IEnumerable<TatLoan> GetApplicantLoans(string id);
    }
}