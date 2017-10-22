using System.Collections.Generic;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Interfaces{
    public interface ITatApplicantListRepository : ISharepointRepository<TatApplicant>{
        IEnumerable<TatApplicant> GetTatApplicantByTitle(string title);
        IEnumerable<TatApplicant> GetTatApplicantByNationalId(string nId);
        IEnumerable<TatApplicant> GetTatApplicantByFileNo(string fileNo);
    }
}