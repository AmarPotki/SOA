
using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class HrDocPermissionListRepository : SharepointRepositoryBase<HrDocPermission>, IHrDocPermissionListRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public HrDocPermissionListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "ABHRDoc";
            _dataContextFactory = databaseFactory;
        }
        public IEnumerable<HrDocPermission> GetNotStartedItems(){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='FileNo' Ascending='TRUE'></FieldRef></OrderBy>" +
                    "<Where><And>" +
                    "<Eq><FieldRef Name='State'/><Value Type='Number'>0</Value></Eq>" +
                    "<Eq><FieldRef Name='StartDate'/><Value Type='DateTime'><Today/></Value></Eq>" +
                    "</And></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<HrDocPermission>();
        }
        public IEnumerable<HrDocPermission> GetMustBeDoneItems()
        {
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='FileNo' Ascending='TRUE'></FieldRef></OrderBy>" +
                    "<Where><And>" +
                    "<Eq><FieldRef Name='State'/><Value Type='Number'>1</Value></Eq>" +
                    "<Lt><FieldRef Name='EndDate'/><Value Type='DateTime'><Today /></Value></Lt>" +
                    "</And></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<HrDocPermission>();
        }
        public new void Update(HrDocPermission instance)
        {
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var item = list.GetItemById(instance.Id.Value);
            client.Load(item);
           // client.ExecuteQuery();
            item["State"] = instance.State;
            item.Update();
            client.ExecuteQuery();
        }
    }
}