using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.TatCharity.Interfaces;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Implementations{
    public class PortalPensionFundsListRepository : SharepointRepositoryBase<PortalPensionFunds>,
        IPortalPensionFundsListRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public PortalPensionFundsListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "tatCharity";
            _dataContextFactory = databaseFactory;
        }
    }
}