using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.TatCharity.Interfaces;
namespace RahyabServices.DataAccess.Repositories.TatCharity.Implementations{
    public class TatLoanFundsListRepository : SharepointRepositoryBase<TatLoanFunds>, ITatLoanFundsListRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public TatLoanFundsListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){          
            Url = "http://";
            _dataContextFactory = databaseFactory;
            SharepointCredential = new NetworkCredential("", "", "");
        }
        
        public int GetPaidLoanFundsCount(string id){
            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml = @"<View><Query>
         <Where><Eq>
                     <FieldRef Name = 'Loan' LookupId = 'TRUE' />
                     <Value Type = 'integer'>" + id + @"</Value>
                </Eq>
         </Where>
                  </And>  </Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.Count;
        }
    }
}