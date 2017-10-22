using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.FaraFek;
namespace RahyabServices.Business.Facades.Interfaces{
    public interface IFaraFekFacade{
        Task<string> Diagram(string name);
        Task<string> Diagram(GetDiagramPostDtq dtq);
        Task<string> GetInfo();
        Task<string> GetInfo(GetInfoPostDtq dtq);
        Task<string> GetPlotCsv();
        Task<string> GetPlotCsv(GetPlotCsvPostDtq getPlotCsvPostDtq);
        Task<string> GetDashbordHtml();
        Task<string> GetDashbordData();
        void SetNewUrl(string url);
    }
}