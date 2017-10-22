

using System.Net;
using RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class DailyliquidityReportRepository : SharepointRepositoryBase<DailyliquidityReport>, IDailyliquidityReportRepository{
        public DailyliquidityReportRepository(IDataContextFactory databaseFactory) :
            base(databaseFactory){
            SiteCollection = "Financial";
            Url = "http://portal.ab.net/";
            SharepointCredential = new NetworkCredential("","","");
        }
    }
}