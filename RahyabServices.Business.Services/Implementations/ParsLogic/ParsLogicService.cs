using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.ParsLogic;
using RahyabServices.Business.Facades.Interfaces;

using RahyabServices.Business.Services.Intefaces.ParsLogic;
namespace RahyabServices.Business.Services.Implementations.ParsLogic{
    public class ParsLogicService : IParsLogicService{
        private readonly IParsLogicFacade _logicFacade;
        public ParsLogicService(IParsLogicFacade logicFacade){
            _logicFacade = logicFacade;
        }
        public async Task<int> CreateNewCase(BranchInformationDtc dtc){
            return await _logicFacade.CreateNewCase(dtc);
        }
        public async Task<IEnumerable<ResultGetRowsActivityIdDto>> GetListRowsByActivityId(TaslListFilterDtc dtc){

            return await _logicFacade.GetRows(dtc);
            
        }
    
}
}