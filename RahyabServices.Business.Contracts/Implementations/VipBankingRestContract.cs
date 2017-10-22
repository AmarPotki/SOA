using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class VipBankingRestContract : ContractBase, IVipBankingRestContract{
        private readonly IChequeService _chequeService;
        private readonly IVipDelinquentService _delinquentService;
        private readonly IPotentialService _potentialService;
        private readonly IVipService _vipService;
        private readonly IGeneralReportService _generalReportService;
        private readonly ILastBalService _lastBalService;
        public VipBankingRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IVipService vipService,
            IPotentialService potentialService, IVipDelinquentService delinquentService, IChequeService chequeService, IGeneralReportService generalReportService, ILastBalService lastBalService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _vipService = vipService;
            _potentialService = potentialService;
            _delinquentService = delinquentService;
            _chequeService = chequeService;
            _generalReportService = generalReportService;
            _lastBalService = lastBalService;
        }
        //public async Task<AllVipDto> GetAllVip(string key, string skip, string take){
        //    var getAll = new GetAllVipDto {Key = key, Take = int.Parse(take), Skip = int.Parse(skip)};
        //    return await
        //        ValidateThenExecuteFaultHandledOperation<AllVipDto, GetAllVipDto>(
        //            async () => await _vipService.GetAll(getAll), getAll);
        //    //return await _vipService.GetAll(getAll);
        //}
        public async Task<AllVipDto> GetAllVip(string key, string skip, string take){
          var tt =  GridRequestParameters.Current;
            var getFilter = new GetAllVipDto { Key = key, Take = int.Parse(take), Skip = int.Parse(skip),Filter = tt.Filters,Sort = tt.Sortings};
            return await
                ValidateThenExecuteFaultHandledOperation<AllVipDto, GetAllVipDto>(
                    async () => await _vipService.GetAll(getFilter), getFilter);
        }
        //public async Task<AllPotentialDto> GetAllPotential(string key, string skip, string take){
        //    var getAll = new GetAllPotentialDto {Key = key, Take = int.Parse(take), Skip = int.Parse(skip)};
        //    return await
        //        ValidateThenExecuteFaultHandledOperation<AllPotentialDto, GetAllPotentialDto>(
        //            async () => await _potentialService.GetAll(getAll), getAll);
        //}

        public async Task<AllPotentialDto> GetAllPotential(string key, string skip, string take)
        {
            var tt = GridRequestParameters.Current;
            var getFilter = new GetAllPotentialDto { Key = key, Take = int.Parse(take), Skip = int.Parse(skip),Filter = tt.Filters,Sort = tt.Sortings };
            return await
                ValidateThenExecuteFaultHandledOperation<AllPotentialDto, GetAllPotentialDto>(
                    async () => await _potentialService.GetAll(getFilter), getFilter);
        }
        public async Task<AllVipDelinquentDto> GetAllDelinquent(string key, string skip, string take){
            var getAll = new GetAllVipDelinquentDto {Key = key, Take = int.Parse(take), Skip = int.Parse(skip)};
            return await
                ValidateThenExecuteFaultHandledOperation<AllVipDelinquentDto, GetAllVipDelinquentDto>(
                    async () => await _delinquentService.GetAll(getAll), getAll);
        }
        public async Task<AllVipDelinquentDto> GetDelinquents(string key, string customerNumber, string skip,
            string take){
            var getDelinquents = new GetVipDelinquentsDto
            {
                Key = key,
                CustomerNumber = customerNumber,
                Take = int.Parse(take),
                Skip = int.Parse(skip)
            };
            return await
                ValidateThenExecuteFaultHandledOperation<AllVipDelinquentDto, GetVipDelinquentsDto>(
                    async () => await _delinquentService.GetDelinquents(getDelinquents), getDelinquents);
        }
        public async Task<AllChequeDto> GetAllCheque(string key, string skip, string take){
            var getAll = new GetAllChequeDto {Key = key, Take = int.Parse(take), Skip = int.Parse(skip)};
            return await
                ValidateThenExecuteFaultHandledOperation<AllChequeDto, GetAllChequeDto>(
                    async () => await _chequeService.GetAll(getAll), getAll);
        }
        public async Task<AllChequeDto> GetCheques(string key, string customerNumber, string skip, string take){
            var getCheques = new GetChequesDto
            {
                Key = key,
                CustomerNumber = customerNumber,
                Take = int.Parse(take),
                Skip = int.Parse(skip)
            };
            return await
                ValidateThenExecuteFaultHandledOperation<AllChequeDto, GetChequesDto>(
                    async () => await _chequeService.GetCheques(getCheques), getCheques);
        }
        public async Task<GeneralReportDto> GetMaxGeneralReport(string key){
          var getMax= new GetMaxGeneralReportDtq {Key = key};
            return await
               ValidateThenExecuteFaultHandledOperation<GeneralReportDto, GetMaxGeneralReportDtq>(
                   async () => await _generalReportService.GetMax(), getMax);
        }
        public async Task<IEnumerable<LastBalDetailDto>> GetThirtyLastBal(string key, string customerNumber){
            var getLast = new GetThirtyLastBalDtq { Key = key ,CustomerNumber = customerNumber};
            return await
               ValidateThenExecuteFaultHandledOperation<IEnumerable<LastBalDetailDto>, GetThirtyLastBalDtq>(
                   async () => await _lastBalService.GetThirtyLastBal(getLast), getLast);
        }
        public async Task<VipDto> GetVipByCustomerNumber(string key, string customerNumber){
            var getVip = new GetVipByCustomerNumberDtq {Key = key,CustomerNumber = customerNumber};
            return await
               ValidateThenExecuteFaultHandledOperation<VipDto, GetVipByCustomerNumberDtq>(
                   async () => await _vipService.GetVipByCustomerNumber(getVip), getVip);
        }
        public async Task<PotentialDto> GetPotentialByCustomerNumber(string key, string customerNumber){
            var getPotential = new GetPotentialByCustomerNumberDtq {Key=key, CustomerNumber = customerNumber};
            return await
                ValidateThenExecuteFaultHandledOperation<PotentialDto, GetPotentialByCustomerNumberDtq>(
                    async () => await _potentialService.GetPotentialByCustomerNumber(getPotential), getPotential);
                       
        }
    
    }
 
}