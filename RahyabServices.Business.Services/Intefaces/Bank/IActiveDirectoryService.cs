using System.Threading.Tasks;
namespace RahyabServices.Business.Services.Intefaces.Bank
{
    public interface IActiveDirectoryService{
        Task UpdateUsersThatMustBeAtive();
        Task UpdateActiveDirectoryUsers();
        Task UpdateDeActiveUsers();
        Task UpdateActiveDirectoryGroups();
        Task CreateOrUpdateGroupName();
        Task UpdateActiveUser(string userName);
        Task UpdateActiveDirectoryGroup(string adUserName);
    }
}