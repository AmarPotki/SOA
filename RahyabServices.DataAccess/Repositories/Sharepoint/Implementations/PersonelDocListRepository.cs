using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class PersonelDocListRepository : SharepointRepositoryBase<PersonelDoc>, IPersonelDocListRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public PersonelDocListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "ABHRDoc";
            _dataContextFactory = databaseFactory;
        }
        public void SetItemPermission(string[] accountName, string folderName){
             SetPermission(accountName, folderName);
        }
        public void ResetItemPermission(string[] accountName, string folderName){
            RemoveOrResetPermission(accountName,folderName);
        }

    }
}