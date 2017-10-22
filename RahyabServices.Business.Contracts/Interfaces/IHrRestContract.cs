using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Delinquent.Branch;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IHrRestContract
    {
        [OperationContract]
        [WebGet( UriTemplate = "GetUserInfo/{key}")]
        Task<PersonInfoDto> GetUserInfo(string key);
        [OperationContract]
        [WebGet(UriTemplate = "GetUserInfoByPersonnelNo/{key}/{personnelNo}")]
        Task<PersonInfoDto> GetUserInfoByPersonnelNo(string key,string personnelNo);

        [OperationContract]
        [WebGet(UriTemplate = "IsBranchManager/{key}")]
        Task<BranchManagerDto> IsBranchManager(string key);

        [OperationContract]
        [WebGet(UriTemplate = "GetWorkStations")]
        Task<IEnumerable<WorkSectionDto>> GetWorkSections();
        [OperationContract]
        [WebGet(UriTemplate = "getBranches")]
        Task<IEnumerable<BranchDto>> GetBranches();
    }
}