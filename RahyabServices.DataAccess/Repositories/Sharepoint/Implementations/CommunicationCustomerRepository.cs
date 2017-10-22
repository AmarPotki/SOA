using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint.BranchMarketing;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
using Microsoft.SharePoint.Client;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations
{
  public  class CommunicationCustomerRepository : SharepointRepositoryBase<CommunicationCustomer>, ICommunicationCustomerRepository
    {
        private readonly IDataContextFactory _databaseFactory;
        public CommunicationCustomerRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "BranchMarketing";
            _databaseFactory = databaseFactory;
        }


    }
}
