using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.TatCharity.Interfaces;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Implementations{
    public class TatLoanListRepository : SharepointRepositoryBase<TatLoan>, ITatLoanListRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public TatLoanListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){          
            Url = "http:///";
            _dataContextFactory = databaseFactory;
            SharepointCredential = new NetworkCredential("", "", "");
        }
        
        public IEnumerable<TatLoan> GetApplicantLoans(string id){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml = @"<View><Query>
         <Where> <Eq>
                     <FieldRef Name = 'Applicant' LookupId = 'TRUE' />
                     <Value Type = 'integer'>" + id + @"</Value>
                </Eq>
         </Where>
                    </Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToCollectionEntity<TatLoan>();
        }
    }
}