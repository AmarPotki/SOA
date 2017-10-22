using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Ebanking;
using RahyabServices.Business.Dtos.Finance;
using RahyabServices.Business.Dtos.Misc;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class MiscRestContract : ContractBase, IMiscRestContract
    {
        private readonly IMiscService _miscService;
        private readonly IEbankingService _ebankingService;
        private readonly IOperationDepartmentService _operationDepartment;
        private readonly IHrService _hrService;
        public MiscRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IMiscService miscService, IEbankingService ebankingService, IOperationDepartmentService operationDepartment, IHrService hrService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _miscService = miscService;
            _ebankingService = ebankingService;
            _operationDepartment = operationDepartment;
            _hrService = hrService;
        }
        public async Task<double> CalculateProfit(CalculateProfitDtc calculateProfitDtc){
            return await
                ValidateThenExecuteFaultHandledOperation<double, CalculateProfitDtc>(
                    async () => await _miscService.CalculateProfitAsync(calculateProfitDtc), calculateProfitDtc);
        }
        public async Task<string> GetThursdayShift(string key, string yearId, string monthId,string workSectionId){
            var getThurs = new GetThursdayShiftDtq
            {
                Key = key,
                MonthId = monthId,
                YearId = yearId,
                WorkSectionId = workSectionId
            };
            return await
                ValidateThenExecuteFaultHandledOperation
                    <string, GetThursdayShiftDtq>(
                        async () => await _ebankingService.GetThursdayShift(getThurs), getThurs);
        }
        public bool OperattionCreateCsvFile(string key){
            return key == "theS3crectC0de" && _operationDepartment.CreateCsvFile();
        }
        
    }
}