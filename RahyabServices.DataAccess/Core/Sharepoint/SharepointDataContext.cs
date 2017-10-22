using System.Net;
using Microsoft.SharePoint.Client;
namespace RahyabServices.DataAccess.Core.Sharepoint{
    public class SharepointDataContext : ClientContext
    {
        public SharepointDataContext(string nameOrConnectionString, NetworkCredential credentials)
            : base(nameOrConnectionString)
        {
            Credentials = credentials;
        }
    }
}