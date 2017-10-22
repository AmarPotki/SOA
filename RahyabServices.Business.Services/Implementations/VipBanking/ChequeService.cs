using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.Business.Services.Intefaces.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;
namespace RahyabServices.Business.Services.Implementations.VipBanking
{
    public class ChequeService : IChequeService
    {
        private readonly IChequeRepository _chequeRepository;
        public ChequeService(IChequeRepository chequeRepository)
        {
            _chequeRepository = chequeRepository;
        }
        public async Task<AllChequeDto> GetAll(GetAllChequeDto getAllChequeDto)
        {
            var all = await _chequeRepository.GetAll(getAllChequeDto.Skip, getAllChequeDto.Take);
            var allDto = Mapper.Map<IEnumerable<Cheque>, IEnumerable<VipChequeDto>>(all);
            return new AllChequeDto { ChequeDtos = allDto, Total = await _chequeRepository.CountAsync() };
        }
        public async Task<AllChequeDto> GetCheques(GetChequesDto getCheques)
        {
            var all = await _chequeRepository.GetCheques(getCheques.CustomerNumber, getCheques.Skip, getCheques.Take);
            var allDto = Mapper.Map<IEnumerable<Cheque>, IEnumerable<VipChequeDto>>(all);
            return new AllChequeDto { ChequeDtos = allDto, Total = await _chequeRepository.GetChequesCount(getCheques.CustomerNumber) };
        }
    }
}