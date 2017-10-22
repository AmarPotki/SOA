namespace RahyabServices.Business.Facades.Interfaces
{
    public interface IHrFacade
    {
        string GetBranchCode(string userName);
        bool IsExsitPersonnelCode(string personnelCode);
        bool IsExsitBranchCode(string branchCode);
        bool IsValidUserName(string userName);
        string GetPersonnelCode(string userName);
        string GetFullName(string personnelCode);
        string GetBranchRegionId(string userName);

    }
}