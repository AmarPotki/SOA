using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.Bank.Factories.Intefaces;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Bank
{
    public class RPTFTDelinquentService : IRPTFTDelinquentService
    {
        private readonly IRptftBondDelinquentRepository _bondDelinquentRepository;
        private readonly IRptftBondRepository _bondRepository;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IRptftGuarantorRepository _guarantorRepository;
        private readonly IRPTFTRepository _rptftRepository;
        private readonly IToDayDelinquentDtoFactory _toDayDelinquentDtoFactory;
        public RPTFTDelinquentService(IRPTFTRepository rptftRepository,
            IToDayDelinquentDtoFactory toDayDelinquentDtoFactory, IRptftGuarantorRepository guarantorRepository,
            ICustomerDelinquentRepository customerDelinquentRepository, ICustomerInfoRepository customerInfoRepository,
            IRptftBondDelinquentRepository bondDelinquentRepository, IRptftBondRepository bondRepository)
        {
            _rptftRepository = rptftRepository;
            _toDayDelinquentDtoFactory = toDayDelinquentDtoFactory;
            _guarantorRepository = guarantorRepository;
            _customerDelinquentRepository = customerDelinquentRepository;
            _customerInfoRepository = customerInfoRepository;
            _bondDelinquentRepository = bondDelinquentRepository;
            _bondRepository = bondRepository;
        }
        public async Task<ToDayDelinquentDto> GetToDayDelinquentAsync(GetToDayDelinquentDto getAccountDto)
        {
            var rptft = await _rptftRepository.
                QueryAsync(async q => await q.Where(x => x.Id == getAccountDto.Id).FirstAsync());
            return _toDayDelinquentDtoFactory.Create(rptft.BranchCode, rptft.BranchName);
        }
        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsAsync(GetGuarantorsDto getGuarantorsDto)
        {
            var customerDelinq =
                await _customerDelinquentRepository.OneAsync(getGuarantorsDto.CustomerDelinquentId);
            var guarantors =
                await _guarantorRepository.GetGuarantor(customerDelinq.ContractCode);
            var guarantorsDto = new List<GuarantorsDto>();
            foreach (RptftGuarantor guaran in guarantors)
            {
                var customer = await _customerInfoRepository.GetByCustomerNumberAsync(guaran.CustomerCode);
                guarantorsDto.Add(new GuarantorsDto
                {
                    FullName = customer.FullNameManaged,
                    NationalId = customer.EconomicCode,
                    GuarantyRemaining = guaran.GuarantyRemaining
                });
            }
            return guarantorsDto;
        }
        public async Task<IEnumerable<BondDto>> GetBondsAsync(GetBondsDto getBondsDto)
        {
            var customerDelinq =
                await _customerDelinquentRepository.OneAsync(getBondsDto.CustomerDelinquentId);
            var bondDelinquens =
                await _bondDelinquentRepository.GetBondDelinquent(customerDelinq.ContractCode);
            var bondtoes = new List<BondDto>();
            foreach (RptftBondDelinquent bondDelinq in bondDelinquens)
            {
                var bond = await _bondRepository.GetBond(bondDelinq.CollatNo);
                bondtoes.Add(new BondDto
                {
                    BondType = bond.CollatType,
                    RegisterDate = bond.TarikhSabtTazmin.ToString(),
                    Value = bond.ArzesheTazmin
                });
            }
            return bondtoes;
        }
        public async Task<IEnumerable<GuarantorsDto>> GetGuarantorsByBranchCodeAsync(GetGuarantorsByBranchCodeDto getGuarantorsByBranchCodeDto){
            var customerDelinq =
                await _customerDelinquentRepository.OneAsync(getGuarantorsByBranchCodeDto.CustomerDelinquentId);
            var guarantors =
                await _guarantorRepository.GetGuarantor(customerDelinq.ContractCode);
            var guarantorsDto = new List<GuarantorsDto>();
            foreach (RptftGuarantor guaran in guarantors)
            {
                var customer = await _customerInfoRepository.GetByCustomerNumberAsync(guaran.CustomerCode);
                guarantorsDto.Add(new GuarantorsDto
                {
                    FullName = customer.FullNameManaged,
                    NationalId = customer.EconomicCode,
                    GuarantyRemaining = guaran.GuarantyRemaining
                });
            }
            return guarantorsDto;
        }
        public async Task<IEnumerable<BondDto>> GetBondsByBranchCodeAsync(GetBondsByBranchCodeDto getBondsByBranchCodeDto)
        {
            var customerDelinq =
                  await _customerDelinquentRepository.OneAsync(getBondsByBranchCodeDto.CustomerDelinquentId);
            var bondDelinquens =
                await _bondDelinquentRepository.GetBondDelinquent(customerDelinq.ContractCode);
            var bondtoes = new List<BondDto>();
            foreach (RptftBondDelinquent bondDelinq in bondDelinquens)
            {
                var bond = await _bondRepository.GetBond(bondDelinq.CollatNo);
                bondtoes.Add(new BondDto
                {
                    BondType = bond.CollatType,
                    RegisterDate = bond.TarikhSabtTazmin.ToString(),
                    Value = bond.ArzesheTazmin
                });
            }
            return bondtoes;
        }
    }
}