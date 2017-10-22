using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.FaraFek;
namespace RahyabServices.Business.Services.Intefaces.Cando{
    public interface IFaraFekService{
        Task<string> GetDiagram(string name);
        Task<string> GetDiagramPost(GetDiagramPostDtq dtq);
        Task<string> GetInfo();
        Task<string> GetInfo(GetInfoPostDtq dtq);
        Task<string> GetPlotCsv();
        Task<string> GetPlotCsv(GetPlotCsvPostDtq dtq);
        Task<string> GetDashbordHtml();
        Task<string> GetDashbordData();
    }
}