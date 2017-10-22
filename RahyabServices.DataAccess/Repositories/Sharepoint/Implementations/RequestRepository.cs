

using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
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
        public bool IsValid(int id){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='Title' Ascending='TRUE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='ID'  ></FieldRef><Value Type='Number'>" +
                    id + "</Value></Eq></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
           return items.Count > 0;
        }
    }
}