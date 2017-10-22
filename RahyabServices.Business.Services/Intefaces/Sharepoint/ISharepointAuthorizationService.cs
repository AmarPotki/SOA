using System.Threading.Tasks;
namespace RahyabServices.Business.Services.Intefaces.Sharepoint{
    public interface ISharepointAuthorizationService{
        bool CheckUserInAdminGroup(string userName);
        bool CheckUserInBranchLevelGroup(string userName);
        bool IsExistInBranchService(string userName);
        bool IsValidUserName(string userName, string siteCollection);
    }
}