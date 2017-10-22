using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Finance;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class FinanceRestContract : ContractBase, IFinanceRestContract{
        private readonly IFinanceService _financeService;
        public FinanceRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IFinanceService financeService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _financeService = financeService;
        }
        public async Task<string> GetDailyliquidityReport(string key, string fromDate,
            string toDate){
            var getDaily = new GetDailyliquidityReportDtq {Key = key,FromPersianDate = fromDate,
                ToPersianDate = toDate};
            return await
                ValidateThenExecuteFaultHandledOperation
                    <string, GetDailyliquidityReportDtq>(
                        async () => await _financeService.GetDailyReport(getDaily), getDaily);
        }
        public async Task<string> GetReportFacilityDetail(string key, string fromDate, string toDate){
            var getDaily = new GetReportFacilityDetailDtq
            {
                Key = key,
                FromPersianDate = fromDate,
                ToPersianDate = toDate
            };
            return await
                ValidateThenExecuteFaultHandledOperation
                    <string, GetReportFacilityDetailDtq>(
                        async () => await _financeService.GetReportFacility(getDaily), getDaily);
        }
    }
}