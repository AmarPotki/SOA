using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.FaraFek;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Cando;
namespace RahyabServices.Business.Services.Implementations.Cando{
    public class FaraFekService : IFaraFekService{
        private readonly IFaraFekFacade _faraFekFacade;
        public FaraFekService(IFaraFekFacade faraFekFacade){
            _faraFekFacade = faraFekFacade;
        }
        public async Task<string> GetDiagram(string name){
            return await _faraFekFacade.Diagram(name);
        }
        public async Task<string> GetDiagramPost(GetDiagramPostDtq dtq){
            return await _faraFekFacade.Diagram(dtq);
        }
        public async Task<string> GetInfo(){
            return await _faraFekFacade.GetInfo();
        }
        public async Task<string> GetInfo(GetInfoPostDtq dtq){
            return await _faraFekFacade.GetInfo(dtq);
        }
        public async Task<string> GetPlotCsv(){
            return await _faraFekFacade.GetPlotCsv();
        }
        public async Task<string> GetPlotCsv(GetPlotCsvPostDtq dtq){
            return await _faraFekFacade.GetPlotCsv(dtq);
        }
        public async Task<string> GetDashbordHtml(){
            _faraFekFacade.SetNewUrl("https://10.100.136.47:5000/");
            return await _faraFekFacade.GetDashbordHtml();
        }
        public async Task<string> GetDashbordData(){
            _faraFekFacade.SetNewUrl("https://10.100.136.47:5000/");
            return await _faraFekFacade.GetDashbordData();
        }
    }
}