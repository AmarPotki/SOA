using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces{
    public interface IPersonelDocListRepository : ISharepointRepository<PersonelDoc>{
        void SetItemPermission(string[] accountName, string folderName);
        void ResetItemPermission(string[] accountName, string folderName);
    }
}