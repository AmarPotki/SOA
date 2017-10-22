

using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class SuppliesRequestRepository : SharepointRepositoryBase<SuppliesRequest>, ISuppliesRequestRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public SuppliesRequestRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "ABSupplies";
            _dataContextFactory = databaseFactory;
        }
        public bool IsValid(int id,string state){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    $"<View><Query><Where><And><Eq><FieldRef Name='ID' /><Value Type='Counter'>{id}</Value>"+
         $"</Eq><Eq><FieldRef Name='State' /><Value Type='Number'>{state}</Value></Eq></And></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
           return items.Count > 0;
        }
        public IEnumerable<SuppliesRequest> GetRequestByState(double state){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection);
            var list = GetList(client);
            var req = new SuppliesRequest();
            var caml = req.BuildCamlQuery<SuppliesRequest>(x => x.State == state);
            var query = new CamlQuery {ViewXml = caml};
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<SuppliesRequest>();
        }
    }
}