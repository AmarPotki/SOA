using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.TatCharity.Interfaces;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Implementations{
    public class TatApplicantListRepository : SharepointRepositoryBase<TatApplicant>, ITatApplicantListRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public TatApplicantListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){          
            Url = "http://t/";
            _dataContextFactory = databaseFactory;
            SharepointCredential = new NetworkCredential("", "", "");
        }
        public IEnumerable<TatApplicant> GetTatApplicantByTitle(string title){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection,Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='Title' Ascending='TRUE'></FieldRef></OrderBy><Where><Contains><FieldRef Name='Title'/><Value Type='Text'>" +
                    title + "</Value></Eq></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<TatApplicant>();
        }
        public IEnumerable<TatApplicant> GetTatApplicantByNationalId(string nId){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='NationalID' Ascending='TRUE'></FieldRef></OrderBy><Where><Contains><FieldRef Name='NationalID'/><Value Type='Text'>" +
                    nId + "</Value></Eq></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<TatApplicant>();
        }
        public IEnumerable<TatApplicant> GetTatApplicantByFileNo(string fileNo){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='FileNo' Ascending='TRUE'></FieldRef></OrderBy><Where><Contains><FieldRef Name='FileNo'/><Value Type='Text'>" +
                    fileNo + "</Value></Eq></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<TatApplicant>();
        }
    }
}