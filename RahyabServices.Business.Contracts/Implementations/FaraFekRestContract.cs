using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Cando.FaraFek;
using RahyabServices.Business.Services.Intefaces.Cando;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class FaraFekRestContract : ContractBase, IFaraFekRestContract{
        private readonly IFaraFekService _faraFekService;
        public FaraFekRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IFaraFekService faraFekService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _faraFekService = faraFekService;
        }
        public async Task<string> GetDiagram(string key, string name){
            var box = new GetDiagramDtq {Key = key, Name = name};
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetDiagramDtq>(
                    async () => await _faraFekService.GetDiagram(name), box);
        }
        public async Task<string> GetInfo(string key){
            var info = new GetInfoDtq { Key = key };
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetInfoDtq>(
                    async () => await _faraFekService.GetInfo(), info);
        }
        public async Task<string> GetPlotCsv(string key){
            var info = new GetPlotCsvDtq { Key = key };
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetPlotCsvDtq>(
                    async () => await _faraFekService.GetPlotCsv(), info);
        }
        public async Task<string> GetDashbordHtml(string key){
            var info = new GetDashbordDtq { Key = key };
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetDashbordDtq>(
                    async () => await _faraFekService.GetDashbordHtml(), info);
        }
        public async Task<string> GetDashbordData(string key){
            var info = new GetDashbordDtq { Key = key };
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetDashbordDtq>(
                    async () => await _faraFekService.GetDashbordData(), info);
        }
        public async Task<string> GetDiagramPost(GetDiagramPostDtq getDiagramPostDtq){
            return await
                ValidateThenExecuteFaultHandledOperation<string, GetDiagramDtq>(
                    async () => await _faraFekService.GetDiagramPost(getDiagramPostDtq), getDiagramPostDtq);
        }
        public async Task<string> GetInfoPost(GetInfoPostDtq getInfoPostDtq){
            return await
                     ValidateThenExecuteFaultHandledOperation<string, GetInfoPostDtq>(
                         async () => await _faraFekService.GetInfo(getInfoPostDtq), getInfoPostDtq);
        }
        public async Task<string> GetPlotCsvPost(GetPlotCsvPostDtq getPlotCsvPostDtq){
            return await
                    ValidateThenExecuteFaultHandledOperation<string, GetPlotCsvPostDtq>(
                        async () => await _faraFekService.GetPlotCsv(getPlotCsvPostDtq), getPlotCsvPostDtq);
        }
    }
}