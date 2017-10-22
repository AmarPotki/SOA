using System.Net;
using RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class ReportFacilityDetailRepository : SharepointRepositoryBase<ReportFacilityDetail>, IReportFacilityDetailRepository{
        public ReportFacilityDetailRepository(IDataContextFactory databaseFactory) :
            base(databaseFactory){
            SiteCollection = "Financial";
            Url = "http://";
            SharepointCredential = new NetworkCredential("","","");
        }
    }
}