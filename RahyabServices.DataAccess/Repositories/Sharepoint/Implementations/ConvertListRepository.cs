using System.Net;
using RahyabServices.Business.Domain.Models.Sharepoint.OperationDepartment;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class ConvertListRepository : SharepointRepositoryBase<ConvertList>, IConvertListRepository{
        public ConvertListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "";
            Url = "http://";
            SharepointCredential = new NetworkCredential("", "", "");
        }
    }
}