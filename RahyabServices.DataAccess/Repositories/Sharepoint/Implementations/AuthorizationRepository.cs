using System.Linq;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
using Microsoft.SharePoint.Client;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class AuthorizationRepository : IAuthorizationRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public AuthorizationRepository(IDataContextFactory dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }
        public bool IsExistInManagerGroup(string userName){
            return CheckUserInGroup(userName, 15);
        }
        public bool IsExistInBranchLevel(string userName){
            return CheckUserInGroup(userName, 474);
        }
        public bool IsExistInBranchService(string userName){
            return CheckUserInGroup(userName, 563);
        }
        protected bool CheckUserInGroup(string userName, int groupId){
            var client = _dataContextFactory.GetSharepointDataContext("delinquent");
            var web = client.Web;
            var adminGroup = web.SiteGroups.GetById(groupId);
            var userCollection = adminGroup.Users;
            client.Load(userCollection,
                users => users.Include(
                    user => user.LoginName,
                    user => user.Id));
            client.ExecuteQuery();
            userName = "ab\\" + userName;
            return Enumerable.Any(userCollection, user => user.LoginName.ToLower() == userName);
        }
    }
}