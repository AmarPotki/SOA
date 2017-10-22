namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces{
    public interface IAuthorizationRepository{
        bool IsExistInManagerGroup(string userName);
        bool IsExistInBranchLevel(string userName);
        bool IsExistInBranchService(string userName);
    }
}