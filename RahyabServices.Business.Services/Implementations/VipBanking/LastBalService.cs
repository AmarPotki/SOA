using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;

namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class LastBalService : ILastBalService
    {
        private readonly ILastBalRepository _lastBalRepository;
        public LastBalService(ILastBalRepository lastBalRepository)
        {
            _lastBalRepository = lastBalRepository;
        }
        public async Task<IEnumerable<LastBalDetailDto>> GetThirtyLastBal(GetThirtyLastBalDtq getThirtyLastBalDtq)
        {
            var items = await _lastBalRepository.GetThirtyLastBal(getThirtyLastBalDtq.CustomerNumber);
            return AutoMapper.Mapper.Map<IEnumerable<LastBalDetail>, IEnumerable<LastBalDetailDto>>(items);
        }

    }
}