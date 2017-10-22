using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core.Sharepoint;

namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces
{
    public interface IUserInformationRepository : ISharepointRepository<UserInformation>
    {
        ListItem GetUser(string userName, string siteCollection = null);
        bool IsValid(string userName, string siteCollection);
    }
}
