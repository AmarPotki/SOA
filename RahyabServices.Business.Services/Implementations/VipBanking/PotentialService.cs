using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Kendo;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;

namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class PotentialService : IPotentialService
    {
        private readonly IPotentialRepository _potentialRepository;
        public PotentialService(IPotentialRepository potentialRepository)
        {
            _potentialRepository = potentialRepository;
        }
        //public async Task<AllPotentialDto> GetAll(GetAllPotentialDto getAllPotentialDto){

        //    if (!string.IsNullOrEmpty(getAllPotentialDto.Filter)){
        //        var filtered = await _potentialRepository.GetFiltered(getAllPotentialDto.Skip, getAllPotentialDto.Take, getAllPotentialDto.Filter);
        //        var filterDto = Mapper.Map<IEnumerable<Potential>, IEnumerable<PotentialDto>>(filtered).ToList();
        //        return new AllPotentialDto { PotentialDtos = filterDto.Skip(getAllPotentialDto.Skip).Take(getAllPotentialDto.Take), Total = filterDto.Count() };
        //    }
        //    else{
        //        var all = await _potentialRepository.GetAll(getAllPotentialDto.Skip, getAllPotentialDto.Take);
        //        var allDto = Mapper.Map<IEnumerable<Potential>, IEnumerable<PotentialDto>>(all);
        //        return new AllPotentialDto {PotentialDtos = allDto, Total = await _potentialRepository.CountAsync()};
        //    }
        //}

        public async Task<AllPotentialDto> GetAll(GetAllPotentialDto getAllPotentialDto)
        {
            var filter = new Filter
            {
                Filters = Mapper.Map<IEnumerable<FilterDto>, IEnumerable<Filter>>(getAllPotentialDto.Filter),
                Logic = "AND"
            };
            foreach (var fi in filter.Filters.Where(fi => fi.Field == "MeanTurnover")) { fi.Value = System.Convert.ToDecimal(fi.Value); }
            var sorts = Mapper.Map<IEnumerable<SortDto>, IEnumerable<Sort>>(getAllPotentialDto.Sort).ToList();
            if (!sorts.Any())
            {
                sorts.Add(new Sort { Field = "KeyId", Dir = "asc" });
            }
            var all = await _potentialRepository.ToDataSourceResult<Potential>(getAllPotentialDto.Take, getAllPotentialDto.Skip, sorts, filter);
            return new AllPotentialDto { Total = all.Total, PotentialDtos = Mapper.Map<IEnumerable<Potential>, IEnumerable<PotentialDto>>((IEnumerable<Potential>)all.Data) };

        }

        public async Task<PotentialDto> GetPotentialByCustomerNumber(GetPotentialByCustomerNumberDtq getPotential)
        {
            var potential = await _potentialRepository.GetPotentialByCustomerNumber(getPotential.CustomerNumber);
            return Mapper.Map<Potential, PotentialDto>(potential);
        }
    }
}