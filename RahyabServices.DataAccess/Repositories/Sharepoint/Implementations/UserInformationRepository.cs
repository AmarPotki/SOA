using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
using RahyabServices.Business.SharepointAutoMapper;

namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations
{
    public class UserInformationRepository : SharepointRepositoryBase<UserInformation>, IUserInformationRepository
    {
        private readonly IDataContextFactory _databaseFactory;
        public UserInformationRepository(IDataContextFactory databaseFactory) : base(databaseFactory)
        {

            _databaseFactory = databaseFactory;
        }
        public new UserInformation GetItemById(int id)
        {
            ServicePointManager
   .ServerCertificateValidationCallback +=
   (sender, cert, chain, sslPolicyErrors) => true;
            var client = _databaseFactory.GetSharepointDataContext(SiteCollection);
            var list = GetListByName(client);
            var query = new CamlQuery
            {
                ViewXml = "<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value></Eq></Where></Query></View>"

            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToEntity<UserInformation>();
        }
        public ListItem GetUser(string userName, string siteCollection = null)
        {
            siteCollection = siteCollection ?? SiteCollection;
            var client = _databaseFactory.GetSharepointDataContext(siteCollection);
            var list = GetListByName(client);
            var query = new CamlQuery
            {
                ViewXml =
                  "<View><Query><Where><Eq><FieldRef Name='Name' /><Value Type='Text'>" + userName + "</Value></Eq></Where></Query></View>"
            };
            ListItemCollection items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items[0];
        }
        public bool IsValid(string userName, string siteCollection)
        {
            siteCollection = siteCollection ?? SiteCollection;
            var client = _databaseFactory.GetSharepointDataContext(siteCollection);
            var list = GetListByName(client);
            var query = new CamlQuery
            {
                ViewXml =
                 "<View><Query><Where><Eq><FieldRef Name='Name' /><Value Type='Text'>" + userName + "</Value></Eq></Where></Query></View>"
            };
            ListItemCollection items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.Count > 0;
        }


    }
}
