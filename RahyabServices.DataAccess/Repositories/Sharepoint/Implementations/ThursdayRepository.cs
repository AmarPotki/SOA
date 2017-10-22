using RahyabServices.Business.Domain.Models.Sharepoint.Ebanking;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class ThursdayRepository : SharepointRepositoryBase<Thursday>, IThursdayRepository{
        public ThursdayRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "ABEbankingSub";
        }
    }
}