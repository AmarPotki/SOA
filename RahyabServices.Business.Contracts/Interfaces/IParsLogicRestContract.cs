using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log.Call;
using RahyabServices.Business.Dtos.ParsLogic;
namespace RahyabServices.Business.Contracts.Interfaces
{
    [ServiceContract]
    public interface IParsLogicRestContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateNewCase")]
        Task<int> CreateNewCase(BranchInformationDtc dtc);
        [OperationContract]
        [WebGet(UriTemplate = "GetListRowsByActivityId/{key}/{activityType}/{activityId}")]
        Task<IEnumerable<ResultGetRowsActivityIdDto>> GetListRowsByActivityId(string key,string activityType,string activityId);
    }
}
