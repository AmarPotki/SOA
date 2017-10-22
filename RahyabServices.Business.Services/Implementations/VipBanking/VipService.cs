using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Kendo;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;

namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class VipService : IVipService
    {
        private readonly IVipRepository _vipRepository;
        public VipService(IVipRepository vipRepository)
        {
            _vipRepository = vipRepository;
        }
        public async Task<AllVipDto> GetAll(GetAllVipDto getAllVipDto)
        {
            var filter = new Filter
            {
                Filters = Mapper.Map<IEnumerable<FilterDto>, IEnumerable<Filter>>(getAllVipDto.Filter),
                Logic = "AND"
            };

            foreach (var fi in filter.Filters.Where(fi => fi.Field == "MeanTurnover")) { fi.Value = Convert.ToDecimal(fi.Value); }

            var sorts = Mapper.Map<IEnumerable<SortDto>, IEnumerable<Sort>>(getAllVipDto.Sort).ToList();
            if (!sorts.Any())
            {
                sorts.Add(new Sort { Field = "KeyId", Dir = "asc" });
            }

            var all = await _vipRepository.ToDataSourceResult<Vip>(getAllVipDto.Take, getAllVipDto.Skip, sorts, filter);
            return new AllVipDto { Total = all.Total, VipDtos = Mapper.Map<IEnumerable<Vip>, IEnumerable<VipDto>>((IEnumerable<Vip>)all.Data) };
        }
        public async Task<VipDto> GetVipByCustomerNumber(GetVipByCustomerNumberDtq getVipByCustomerNumberDtq)
        {
            var vip =
                await
                    _vipRepository.GetVipByCustomerNumber(getVipByCustomerNumberDtq.CustomerNumber);
            return Mapper.Map<Vip, VipDto>(vip);
        }
    }
}
