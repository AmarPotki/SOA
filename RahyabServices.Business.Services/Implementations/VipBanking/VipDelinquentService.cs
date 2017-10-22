using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;

namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class VipDelinquentService : IVipDelinquentService
    {
        private readonly IVipDelinquentRepository _vipDelinquentRepository;
        public VipDelinquentService(IVipDelinquentRepository vipDelinquentRepository)
        {
            _vipDelinquentRepository = vipDelinquentRepository;
        }
        public async Task<AllVipDelinquentDto> GetAll(GetAllVipDelinquentDto getAllVipDelinquentDto)
        {
            var all = await _vipDelinquentRepository.GetAll(getAllVipDelinquentDto.Skip, getAllVipDelinquentDto.Take);
            var allDto = Mapper.Map<IEnumerable<VipDelinquent>, IEnumerable<VipDelinquentDto>>(all);
            return new AllVipDelinquentDto
            {
                DelinquentDtos = allDto,
                Total = await _vipDelinquentRepository.CountAsync()
            };
        }
        public async Task<AllVipDelinquentDto> GetDelinquents(GetVipDelinquentsDto getDelinquents)
        {
            var all = await _vipDelinquentRepository.GetDelinquents(getDelinquents.CustomerNumber, getDelinquents.Skip, getDelinquents.Take);
            var allDto = Mapper.Map<IEnumerable<VipDelinquent>, IEnumerable<VipDelinquentDto>>(all);
            return new AllVipDelinquentDto
            {
                DelinquentDtos = allDto,
                Total = await _vipDelinquentRepository.GetDelinquentsCount(getDelinquents.CustomerNumber)
            };
        }
    }
}