using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class DelinquentService :IDelinquentService{
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly IDelinquentTrRepository _delinquentTrRepository;
        private readonly IVwRizeAghsatRepository _rizeAghsatRepository;
        public DelinquentService(IDateTimeConvertor dateTimeConvertor, IDelinquentTrRepository delinquentTrRepository, IVwRizeAghsatRepository rizeAghsatRepository){
            _dateTimeConvertor = dateTimeConvertor;
            _delinquentTrRepository = delinquentTrRepository;
            _rizeAghsatRepository = rizeAghsatRepository;
        }
        public async Task<IEnumerable<BranchDelinquentDto>> GetBranchDelinquent(GetBranchDelinquentDtq branchDelinquentDtq){
            var fromDate = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(branchDelinquentDtq.PersianFromDate);
            var toDate = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(branchDelinquentDtq.PersianToDate);
            var fromDateAbis = _dateTimeConvertor.InserSlashIntoStrPersianDate(branchDelinquentDtq.PersianFromDate);
            var toDateAbis = _dateTimeConvertor.InserSlashIntoStrPersianDate(branchDelinquentDtq.PersianToDate);
            var bdr =(await _delinquentTrRepository.GetDelinquentBranch(fromDate, toDate,branchDelinquentDtq.BranchCode)).ToList();
            var abisBdr =
                await
                    _rizeAghsatRepository.GetDelinquentBranch(fromDateAbis, toDateAbis, branchDelinquentDtq.BranchCode);
            bdr.AddRange(abisBdr);
            return Mapper.Map<IEnumerable<BranchDelinquentReport>,IEnumerable<BranchDelinquentDto>>(bdr);
           
        }
        public async Task<IEnumerable<BranchesDelinquentDto>> GetBranchesDelinquent(GetBranchesDelinquentDtq branchesDelinquentDtq){
            var fromDate = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(branchesDelinquentDtq.PersianFromDate);
            var toDate = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(branchesDelinquentDtq.PersianToDate);
            var fromDateAbis = _dateTimeConvertor.InserSlashIntoStrPersianDate(branchesDelinquentDtq.PersianFromDate);
            var toDateAbis = _dateTimeConvertor.InserSlashIntoStrPersianDate(branchesDelinquentDtq.PersianToDate);
            var bdr = (await _delinquentTrRepository.GetDelinquentBranch(fromDate, toDate)).ToList();
            var abisBdr =
                await
                    _rizeAghsatRepository.GetDelinquentBranch(fromDateAbis, toDateAbis);
            bdr.AddRange(abisBdr);
            return Mapper.Map<IEnumerable<BranchDelinquentReport>, IEnumerable<BranchesDelinquentDto>>(bdr);
        }
    }
}