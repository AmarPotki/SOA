using System.Threading.Tasks;
using RahyabServices.Business.Dtos.ParsLogic;
using RahyabServices.Business.Facades.Proxy.wPMI;
using System.Collections.Generic;

namespace RahyabServices.Business.Facades.Interfaces{
    public interface IParsLogicFacade{
        Task<int> CreateNewCase(BranchInformationDtc dtc);

        Task<IEnumerable<ResultGetRowsActivityIdDto>> GetRows(TaslListFilterDtc dtc);
    }
}