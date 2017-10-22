using System.Collections.Generic;
using System.IO;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using Microsoft.SharePoint.Client;
namespace RahyabServices.DataAccess.Core.Sharepoint{
    public interface ISharepointRepository<TEntity> where TEntity : class, IEntitySharepointMapper
    {
        TEntity GetItemById(int id);
        TEntity GetItem(int id);
        TEntity GetLastItem();
        TEntity GetItem(string query);
        IEnumerable<TEntity> GetItems(string camlQuery);
        int Save(TEntity instance);
       
        void Update(TEntity instance);
        void UploadFile(string fileName, Stream fs);

        void Update(EditRequestSplitLogDto editRequestSplitLogDto, int id);
        void Update(EditRequestClearingLogDto editRequestSplitLogDto, int id);
        void Update(EditRequestGivingAChanceLogDto editRequestSplitLogDto, int id);
        void Update(EditRequestImpunityForCrimesLogDto editRequestSplitLogDto, int id);

    }
}