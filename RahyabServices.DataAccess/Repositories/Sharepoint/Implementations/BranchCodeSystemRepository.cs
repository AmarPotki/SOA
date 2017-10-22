using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations
{
   public class BranchCodeSystemRepository : SharepointRepositoryBase<BranchCodeSystem>, IBranchCodeSystemRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public BranchCodeSystemRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "ABSupplies";
            _dataContextFactory = databaseFactory;
        }
    }
}
