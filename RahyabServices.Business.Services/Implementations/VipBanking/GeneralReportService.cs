using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;

namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class GeneralReportService : IGeneralReportService
    {
        private readonly IGeneralReportRepository _generalReportRepository;
        public GeneralReportService(IGeneralReportRepository generalReportRepository)
        {
            _generalReportRepository = generalReportRepository;
        }
        public async Task<GeneralReportDto> GetMax()
        {
            var max = await _generalReportRepository.GetMax();
            return Mapper.Map<GeneralReport, GeneralReportDto>(max);
        }
    }
}