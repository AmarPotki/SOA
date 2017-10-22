using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.ParsLogic;
namespace RahyabServices.Business.Services.Intefaces.ParsLogic{
    public interface IParsLogicService{
        Task<int> CreateNewCase(BranchInformationDtc dtc);

        Task<IEnumerable<ResultGetRowsActivityIdDto>> GetListRowsByActivityId(TaslListFilterDtc dtc);
    }
}