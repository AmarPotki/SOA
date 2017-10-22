using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.ParsLogic;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.ParsLogic;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class ParsLogicRestContract : ContractBase, IParsLogicRestContract{
        private readonly IParsLogicService _parsLogicService;
        public ParsLogicRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IParsLogicService parsLogicService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _parsLogicService = parsLogicService;
        }
        public async Task<int> CreateNewCase(BranchInformationDtc dtc){
            return await
               ValidateThenExecuteFaultHandledOperation<int, BranchInformationDtc>(
                   async () => await _parsLogicService.CreateNewCase(dtc), dtc);
        }

        public async Task<IEnumerable<ResultGetRowsActivityIdDto>> GetListRowsByActivityId(string key, string activityType, string activityId){
            var dtc = new TaslListFilterDtc
            {
                Key = key,
                ActivityId = int.Parse(activityId),
                ActivityType = int.Parse(activityType)
            };
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<ResultGetRowsActivityIdDto>, TaslListFilterDtc>(
                   async () => await _parsLogicService.GetListRowsByActivityId(dtc),dtc);
        }
    }
}