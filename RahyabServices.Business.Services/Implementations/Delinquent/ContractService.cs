using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Contracts;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Dtos.Delinquent.Factories.Intefaces;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Exceptions;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;

namespace RahyabServices.Business.Services.Implementations.Delinquent
{
    public class ContractService : IContractService
    {
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IHrFacade _hrFacade;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly ICryptographer _cryptographer;
        private readonly IRPTFTRepository _rptftRepository;
        private readonly ICustomerDelinquentDtoFactory _customerDelinquentDtoFactory;
        private readonly IBranchRepository _branchRepository;
        private readonly Dictionary<int, string> _contractType;
        private readonly IVariableConventor _variableConventor ;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly IGuaranteeDetailRepository _guaranteeDetailRepository;
        private readonly ISharepointAuthorizationService _sharepointAuthorizationService;
        public ContractService(ICustomerDelinquentRepository customerDelinquentRepository, IHrFacade hrFacade, IDateTimeConvertor dateTimeConvertor, ICustomerInfoRepository customerInfoRepository, ICryptographer cryptographer, IRPTFTRepository rptftRepository, ICustomerDelinquentDtoFactory customerDelinquentDtoFactory, IBranchRepository branchRepository, IVariableConventor variableConventor, ILogBaseRepository logBaseRepository, IGuaranteeDetailRepository guaranteeDetailRepository, ISharepointAuthorizationService sharepointAuthorizationService)
        {
            _customerDelinquentRepository = customerDelinquentRepository;
            _hrFacade = hrFacade;
            _dateTimeConvertor = dateTimeConvertor;
            _customerInfoRepository = customerInfoRepository;
            _cryptographer = cryptographer;
            _rptftRepository = rptftRepository;
            _customerDelinquentDtoFactory = customerDelinquentDtoFactory;
            _branchRepository = branchRepository;
            _variableConventor = variableConventor;
            _logBaseRepository = logBaseRepository;
            _guaranteeDetailRepository = guaranteeDetailRepository;
            _sharepointAuthorizationService = sharepointAuthorizationService;
            _contractType = new Dictionary<int, string>
            {
                {60, "ضمانت نامه"},
                {61,"قرض الحسنه"},
                {62,"جعاله"},
            {63,"فروش اقساطی"},{64,"اجاره به شرط تملیک"},{65,"مضاربه"},{66,"مشارکت مدنی"},{67,"مرابه"},{68,"خرید دین"},{0,"نا مشخص"},
            };
        }

        #region BranchRequest
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(GetContractsByUserNameDto getContractsByUserNameDto){
            var decUserName = _cryptographer.Decrypt(getContractsByUserNameDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
              var branch =await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if(branch.Level==0)
             contracts = await _customerDelinquentRepository.GetContractsByBranchAsync(strBranchCode);
            else if (branch.Level==1) {
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAsync(branches.Select(x => x.OldCode));
            }
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<CountAndSumDto> GetBranchContractsCountAsync(GetContractsCountDto getContractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(getContractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                 count = await _customerDelinquentRepository.GetTotalCountAsync(strBranchCode);
                if (count > 0) sum = await _customerDelinquentRepository.GetTotalSumAsync(strBranchCode);
            }
            else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetTotalCountByOldBranchesAsync(branches.Select(x => x.OldCode));
                if (count > 0) sum = await _customerDelinquentRepository.GetTotalSumByOldBranchesAsync(branches.Select(x => x.OldCode));
            }

            return new CountAndSumDto(count,sum);
        }
        public IEnumerable<CustomerDelinquentDto> GetBranchContracts(GetContractsByUserNameDto getContractsByUserNameDto)
        {
            throw new System.NotImplementedException();
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountAsync(GetContractsCountDto contractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetCurrentCountAsync(strBranchCode); 
                 if(count >0)
                     sum = await _customerDelinquentRepository.GetCurrentSumAsync(strBranchCode);
            }
               else if (branch.Level == 1){
                   CheckUserInBranchServiceGroup(decUserName);
                   var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                   count = await _customerDelinquentRepository.GetCurrentCountByOldBranchesAsync(branches.Select(x => x.OldCode)); 
                 if(count >0)
                     sum = await _customerDelinquentRepository.GetCurrentSumByOldBranchesAsync(branches.Select(x => x.OldCode));
               }

            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountAsync(GetContractsCountDto contractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetExpireCountAsync(strBranchCode);
            if(count >0)
                sum = await _customerDelinquentRepository.GetExpireSumAsync(strBranchCode);
            }else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetExpireCountByOldBranchesAsync(branches.Select(x => x.OldCode));
                if (count > 0)
                    sum = await _customerDelinquentRepository.GetExpireSumByOldBranchesAsync(branches.Select(x => x.OldCode));
            }

            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountAsync(GetContractsCountDto contractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetDueDateCountAsync(strBranchCode);
                if (count > 0) sum = await _customerDelinquentRepository.GetDueDateSumAsync(strBranchCode);
            }else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetDueDateCountByOldBranchesAsync(branches.Select(x => x.OldCode));
                if (count > 0) sum = await _customerDelinquentRepository.GetDueDateSumByOldBranchesAsync(branches.Select(x => x.OldCode));
            }
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetBadDebtContractsCountAsync(GetContractsCountDto contractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetBadDebtAsync(strBranchCode);
                if (count > 0) sum = await _customerDelinquentRepository.GetBadDebtSumAsync(strBranchCode);
            }else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetBadDebtByOldBranchesAsync(branches.Select(x => x.OldCode));
                if (count > 0) sum = await _customerDelinquentRepository.GetBadDebtSumByOldBranchesAsync(branches.Select(x => x.OldCode));
            }

            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountAsync(GetContractsCountDto contractsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetPostponedCountAsync(strBranchCode);
                if (count > 0) sum = await _customerDelinquentRepository.GetPostponedSumAsync(strBranchCode);
            }
            else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetPostponedByOldBranchesCountAsync(branches.Select(x => x.OldCode));
                if (count > 0) sum = await _customerDelinquentRepository.GetPostponedByOldBranchesSumAsync(branches.Select(x => x.OldCode));
            }
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetOneMonthToDueDateCountAsync(GetContractsCountDto contractsCountDto){
            var decUserName = _cryptographer.Decrypt(contractsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
            decimal sum = 0;
            if (branch.Level == 0){
                count = await _customerDelinquentRepository.GetOneMonthToDueDateCountAsync(strBranchCode);
                if (count > 0) sum = await _customerDelinquentRepository.GetSumOneMonthToDueDateAsync(strBranchCode);
            }           else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetOneMonthToDueDateByOldBranchesCountAsync(branches.Select(x => x.OldCode));
                if (count > 0) sum = await _customerDelinquentRepository.GetSumOneMonthToDueDateByOldBranchesAsync(branches.Select(x => x.OldCode));
            }
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetAllDebtsCountAsync(GetContractsCountDto allDebtsCountDto)
        {
            var decUserName = _cryptographer.Decrypt(allDebtsCountDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            var count = 0;
             decimal sum = 0;
            if (branch.Level == 0){
                 count = await _customerDelinquentRepository.GetAllDebtsCountAsync(strBranchCode); 
                  if (count > 0)
                      sum = await _customerDelinquentRepository.GetAllDebtsSumAsync(strBranchCode);
            }
            else if(branch.Level==1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                count = await _customerDelinquentRepository.GetAllDebtsByOldBranchCountAsync(branches.Select(x => x.OldCode));
                if (count > 0)
                    sum = await _customerDelinquentRepository.GetAllDebtsByOldBranchSumAsync(branches.Select(x => x.OldCode));
            }
          
            return new CountAndSumDto(count, sum);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(GetCurrentContractsDto getCurrentContractsDto){
            var decUserName = _cryptographer.Decrypt(getCurrentContractsDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch =await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if(branch.Level==0)
            contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(strBranchCode, "0");
            else if (branch.Level == 1){
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAndStatusAsync(branches.Select(x => x.OldCode), "0");
            }
           
            //  var customerInformations = new List<CustomerInformationDto>();
            return await ConvertToCustomerDelinquentDto(contracts);
        }
      
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(GetBadDebtDto getBadDebtDto)
        {
            var decUserName = _cryptographer.Decrypt(getBadDebtDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch =await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if(branch.Level==0)
             contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(strBranchCode, "2");
             else if (branch.Level == 1){
                 CheckUserInBranchServiceGroup(decUserName);
                 var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                 contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAndStatusAsync(branches.Select(x => x.OldCode), "2");
             }
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(GetPostponedDto postponedDto)
        {
            var decUserName = _cryptographer.Decrypt(postponedDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch =await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if(branch.Level==0)
                contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(strBranchCode, "5");
             else if (branch.Level == 1){
                 CheckUserInBranchServiceGroup(decUserName);
                 var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                 contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAndStatusAsync(branches.Select(x => x.OldCode), "5");
             }
            //  var customerInformations = new List<CustomerInformationDto>();
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(GetDueDateContractsDto getDueDateContractsDto)
        {
            var decUserName = _cryptographer.Decrypt(getDueDateContractsDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if (branch.Level == 0)
                contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(strBranchCode, "6");
            else if (branch.Level == 1)
            {
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAndStatusAsync(branches.Select(x => x.OldCode), "6");
            }
            //  var customerInformations = new List<CustomerInformationDto>();
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(GetOneMonthToDueDateContractsDto getOneMonthToDueDateContractsDto)
        {
            var decUserName = _cryptographer.Decrypt(getOneMonthToDueDateContractsDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if (branch.Level == 0)
             contracts = await _customerDelinquentRepository.GetOneMonthToDueDateAsync(strBranchCode);
            else if (branch.Level == 1)
            {
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetOneMonthToDueDateByBranchesAsync(branches.Select(x => x.OldCode));
            }
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(GetExpireContractsDto expireContractsDto)
        {
            var decUserName = _cryptographer.Decrypt(expireContractsDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName); 
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if (branch.Level == 0)
                contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(strBranchCode, "1");
            else if (branch.Level == 1)
            {
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetContractsByOldBranchesAndStatusAsync(branches.Select(x => x.OldCode), "1");
            }
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(GetAllDebtsContractsDto allDebtsContractsDto)
        {
            var decUserName = _cryptographer.Decrypt(allDebtsContractsDto.UserName);
            var strBranchCode = _hrFacade.GetBranchCode(decUserName);
            var branch = await _branchRepository.GetBranchByCode(strBranchCode);
            IEnumerable<CustomerDelinquent> contracts = new List<CustomerDelinquent>();
            if (branch.Level == 0)
                contracts = await _customerDelinquentRepository.GetAllDebtsAsync(strBranchCode);
            else if (branch.Level == 1)
            {
                CheckUserInBranchServiceGroup(decUserName);
                var branches = await _branchRepository.GetMergBranchChildren(branch.Id);
                contracts = await _customerDelinquentRepository.GetAllDebtsByOldBranchesAsync(branches.Select(x => x.OldCode));
            }

            return await ConvertToCustomerDelinquentDto(contracts);
        }
      
        private async Task<IEnumerable<CustomerDelinquentDto>> ConvertToCustomerDelinquentDto(IEnumerable<CustomerDelinquent> customerDelinquents)
        {
            var cDs = new List<CustomerDelinquentDto>();
            var customers = await _customerInfoRepository.GetCustomersAsync(customerDelinquents.Select(x=>x.CustomerNumber));
            foreach (var cdt in customerDelinquents)
            {

                var customerInformation = (customers.FirstOrDefault(x=>x.CustomerNumber== cdt.CustomerNumber));

                cDs.Add(new CustomerDelinquentDto
                {
                    FullName = customerInformation == null ? string.Empty : customerInformation.FullNameManaged,
                    BranchName = cdt.BranchName,
                    BranchCode = cdt.BranchCode,
                    ContractCode = cdt.ContractCode,
                    CustomerNumber = cdt.CustomerNumber,
                    HistoryDate = cdt.HistoryDate,
                    MaturityDate = _dateTimeConvertor.GetPersianDate(cdt.MaturityDate),
                    StartDate = _dateTimeConvertor.GetPersianDate(cdt.StartDate),
                    Status = cdt.Status,
                    CustomerDelinquentId = cdt.Id,
                    ApprovedAmount = cdt.ApprovedAmount,
                    InterestRate = cdt.InterestRate,
                    RemainingPenalty = cdt.RemainingPenalty,
                    IsArchived = cdt.IsArchived,
                    BankType = (int)cdt.BankType,
                    ContractType = _contractType[(int)cdt.ContractType],
                    GuaranteeStatus = cdt.GuaranteeStatus,
                    ContractDescription = cdt.ContractDescription,
                    DebitBalance = cdt.DebitBalance,
                    Remaining = cdt.Remaining,
                    RemainingProfit = cdt.RemainingProfit,
                    MandehGheirJari = cdt.MandehGheireJary,
                    MandehJary = cdt.MandehJari,
                    DebitorAmount = cdt.DebitorAmount,
                    Jarimeh = cdt.Jarimeh,
                    PaymentDate = cdt.PaymentDate,
                    OldBranchCode = cdt.OldBranchCode,
                    OldBranchName = cdt.OldBranchName
                });
            }
            return cDs;
        }
        #endregion

        #region ManagerRequest

        public async Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(
            GetAllBranchActivityDtq getAllBranchActivityDtq){
                var formDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(getAllBranchActivityDtq.FromPersianDate);
                var toDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(getAllBranchActivityDtq.ToPersianDate);
            return await _logBaseRepository.GetAllBranchActivity(formDate, toDate);
        }

        public async Task<string> GetLastAbisUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto){
           return await _customerDelinquentRepository.GetMaxHisDate(BankType.Abis);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(GetContractsByBranchCodeDto getContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAsync(getContractsByBranchCodeDto.BranchCode);
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public IEnumerable<CustomerDelinquentDto> GetBranchContractsByBranchCode(GetContractsByBranchCodeDto getContractsByBranchCodeDto)
        {
            throw new System.NotImplementedException();
        }
        public async Task<CountAndSumDto> GetBranchContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto getContractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetTotalCountAsync(getContractsCountByBranchCodeDto.BranchCode);
              decimal sum = 0;
            if(count >0)
             sum = await _customerDelinquentRepository.GetTotalSumAsync(getContractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetCurrentCountAsync(contractsCountByBranchCodeDto.BranchCode);
              decimal sum = 0;
            if(count >0)
             sum = await _customerDelinquentRepository.GetCurrentSumAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetExpireCountAsync(contractsCountByBranchCodeDto.BranchCode);
            decimal sum = 0;
            if (count > 0)
             sum = await _customerDelinquentRepository.GetExpireSumAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetDueDateCountAsync(contractsCountByBranchCodeDto.BranchCode);
              decimal sum = 0;
            if(count >0)
             sum = await _customerDelinquentRepository.GetDueDateSumAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetBadDebtContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetBadDebtAsync(contractsCountByBranchCodeDto.BranchCode);
              decimal sum = 0;
            if(count >0)
             sum = await _customerDelinquentRepository.GetBadDebtSumAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetPostponedCountAsync(contractsCountByBranchCodeDto.BranchCode);
              decimal sum = 0;
            if(count >0)
             sum = await _customerDelinquentRepository.GetPostponedSumAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(GetPostponedByBranchCodeDto postponedByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(postponedByBranchCodeDto.BranchCode, "5");
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(GetBadDebtByBranchCodeDto getBadDebtByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(getBadDebtByBranchCodeDto.BranchCode, "2");
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(
            GetOneMonthToDueDateContractsByBranchCodeDto getOneMonthToDueDateContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetOneMonthToDueDateAsync(getOneMonthToDueDateContractsByBranchCodeDto.BranchCode);
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(GetExpireContractsByBranchCodeDto expireContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(expireContractsByBranchCodeDto.BranchCode, "1");
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(GetDueDateContractsByBranchCodeDto getDueDateContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(getDueDateContractsByBranchCodeDto.BranchCode, "6");
            return await ConvertToCustomerDelinquentDto(contracts);
        }

        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(
            GetAllDebtsContractsByBranchCodeDto allDebtsContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetAllDebtsAsync(allDebtsContractsByBranchCodeDto.BranchCode);
            return await ConvertToCustomerDelinquentDto(contracts);
        }

      

        public async Task<CountAndSumDto> GetOneMonthToDueDateCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetOneMonthToDueDateCountAsync(contractsCountByBranchCodeDto.BranchCode);
            decimal sum = 0;
            if (count > 0)
                sum = await _customerDelinquentRepository.GetSumOneMonthToDueDateAsync(contractsCountByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(GetCurrentContractsByBranchCodeDto getCurrentContractsByBranchCodeDto)
        {
            var contracts = await _customerDelinquentRepository.GetContractsByBranchAndStatusAsync(getCurrentContractsByBranchCodeDto.BranchCode, "0");
            return await ConvertToCustomerDelinquentDto(contracts);
        }
        public async Task<string> GetLastBankIranUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto){
            return await _customerDelinquentRepository.GetMaxHisDate(BankType.BankIran);
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(GetCustomerDelinquentHistoryDto customerDelinquentHistoryDto){
            var branch = _hrFacade.GetBranchCode(_cryptographer.Decrypt(customerDelinquentHistoryDto.UserName));
            var cDLists = new List<CustomerDelinquentDto>();
            var dateWithOutSlash = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(customerDelinquentHistoryDto.PersianDate);
            var customerDelinquents = (await _rptftRepository.GetBankIranCustomerDelinquentByBranchAsync(dateWithOutSlash, branch)).ToArray();
            var customerDelinquentsAbis = (await _rptftRepository.GetAbisCustomerDelinquentByBranchAsync(dateWithOutSlash, branch)).ToArray();
            var customerNumberArray =
                customerDelinquents.Select(x => x.Rptft.CustomerNumber)
                    .Concat(customerDelinquentsAbis.Select(x => x.Rptft.CustomerNumber))
                    .ToArray();
             var branches = await _branchRepository.GetAllAsNoTracking();
             var customers = await _customerInfoRepository.GetCustomersAsync(customerNumberArray);
            Parallel.ForEach(customerDelinquents, rptf =>{

                var item = rptf.Rptft;
                var contractDetail = rptf.BankIranDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, 0,
                    item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation = (customers.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber));
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullNameManaged;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) {
                    status = "0";
                }
                else{
                    if (contractDetail.CodeSarFaslGherjari != "0965" && contractDetail.CodeSarFaslGherjari != "1010" &&
                        contractDetail.CodeSarFaslGherjari != "0960") status = "6";
                    else if (contractDetail.CodeSarFaslGherjari == "0965") {
                        status = "2";
                    }
                    else if (contractDetail.CodeSarFaslGherjari == "1010") {
                        status = "5";
                    }
                    else if (contractDetail.CodeSarFaslGherjari == "0960") { status = "1"; }
                }
                cD.Status = status;
                cD.Remaining = contractDetail.MandeAghsatGherjari + contractDetail.MandeJari;
                cD.RemainingProfit = contractDetail.MandesudeJari + contractDetail.MandeAghsatGherjari;
                cD.RemainingPenalty = Convert.ToDecimal(contractDetail.Mandevajheltezamdaryafti);
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + cD.RemainingPenalty;
                cD.MandehJary = contractDetail.MandesudeJari;
                cD.MandehGheirJari = contractDetail.MandeAghsatGherjari;
                cD.BankType = (int) BankType.BankIran;
                cDLists.Add(cD);
            });
            Parallel.ForEach(customerDelinquentsAbis, rptf =>{

                var item = rptf.Rptft;
                var contractDetail = rptf.AbisDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate,
                    item.RemainingPenalty, item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation = (customers.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber));
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullNameManaged;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) {
                    status = "0";
                }
                else{
                    if (contractDetail.OverDue1 == 0 && contractDetail.OverDue2 == 0 && contractDetail.OverDue3 == 0) status = "6";
                    else if (contractDetail.OverDue3 > 0) {
                        status = "2";
                    }
                    else if (contractDetail.OverDue2 > 0) {
                        status = "5";
                    }
                    else if (contractDetail.OverDue1 > 0) { status = "1"; }
                }
                item.Status = status;
                cD.Remaining = (decimal) (_variableConventor.ConvertDoubleDecimal(contractDetail.RemainingAmount) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue1) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue2) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue3));
                cD.RemainingProfit = (decimal) (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                                contractDetail.SoudeOverDue3 + contractDetail.SoudeTashilateJari);
                // cD.RemainingPenalty = item.RemainingPenalty;
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + item.RemainingPenalty;
                decimal mandehJari = 0;
                decimal mandehGheirJari = 0;
                if (date > _dateTimeConvertor.GetGregorianFromPersian("1394/09/01")){
                    mandehJari = contractDetail.RemainingAmount;
                    mandehGheirJari = contractDetail.OverDue1 + contractDetail.OverDue2 +
                                      contractDetail.OverDue3;
                }
                else{
                    mandehJari = contractDetail.RemainingAmount +
                                 (contractDetail.SoudeTashilateJari.HasValue
                                     ? (decimal) contractDetail.SoudeTashilateJari
                                     : 0);
                    mandehGheirJari = (contractDetail.OverDue1 + contractDetail.OverDue2 +
                                       contractDetail.OverDue3) +
                                      (decimal)
                                          (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                           contractDetail.SoudeOverDue3);
                }

                cD.MandehJary = mandehJari;
                cD.MandehGheirJari = mandehGheirJari;
                cDLists.Add(cD);
            });

            return cDLists;
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(
            GetCustomerDelinquentHistoryByBranchCodeDto customerDelinquentHistoryByBranchCodeDto){
            var branch = customerDelinquentHistoryByBranchCodeDto.BranchCode;
                var cDLists = new ConcurrentBag<CustomerDelinquentDto>();
                var dateWithOutSlash = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(customerDelinquentHistoryByBranchCodeDto.PersianDate);
                var customerDelinquents = (await _rptftRepository.GetBankIranCustomerDelinquentByBranchAsync(dateWithOutSlash, branch)).ToArray();
                var branches = await _branchRepository.GetAllAsNoTracking();
                var customersDelinquents = await _customerDelinquentRepository.GetAllAsync();
                var customerDelinquentsAbis = (await _rptftRepository.GetAbisCustomerDelinquentByBranchAsync(dateWithOutSlash, branch)).ToArray();
                var customerNumberArray =
                   customerDelinquents.Select(x => x.Rptft.CustomerNumber)
                       .Concat(customerDelinquentsAbis.Select(x => x.Rptft.CustomerNumber))
                       .ToArray();
               var contractArray =
                       customerDelinquents.Select(x => x.Rptft.ContractCode)
                           .Concat(customerDelinquentsAbis.Select(x => x.Rptft.ContractCode))
                           .ToArray();
               // var customers = await _customerInfoRepository.GetCustomersAsync(customerNumberArray);
                var geoDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(customerDelinquentHistoryByBranchCodeDto.PersianDate);
                var lastActions = await _logBaseRepository.GetLastLogAsync(contractArray, geoDate);
            Parallel.ForEach(customerDelinquents, t =>{
                var item = t.Rptft;
                var contractDetail = t.BankIranDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, 0,
                    item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation =
                    customersDelinquents.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber);
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) {
                    status = "0";
                }
                else{
                    if (contractDetail.CodeSarFaslGherjari != "0965" && contractDetail.CodeSarFaslGherjari != "1010" &&
                        contractDetail.CodeSarFaslGherjari != "0960") status = "6";
                    else if (contractDetail.CodeSarFaslGherjari == "0965") {
                        status = "2";
                    }
                    else if (contractDetail.CodeSarFaslGherjari == "1010") {
                        status = "5";
                    }
                    else if (contractDetail.CodeSarFaslGherjari == "0960") { status = "1"; }
                }
                cD.Status = status;
                cD.Remaining = contractDetail.MandeAghsatGherjari + contractDetail.MandeJari;
                cD.RemainingProfit = contractDetail.MandesudeJari + contractDetail.Mandesudegherjari;
                cD.RemainingPenalty = Convert.ToDecimal(contractDetail.Mandevajheltezamdaryafti);
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + cD.RemainingPenalty;
                cD.MandehJary = contractDetail.MandesudeJari;
                cD.MandehGheirJari = contractDetail.MandeAghsatGherjari;
                cD.BankType = (int) BankType.BankIran;

                var lastAction = lastActions.FirstOrDefault(x => x.ContractCode == item.ContractCode);
                if (lastAction != null) cD.LastAction = lastAction.GetType().Name;
                cDLists.Add(cD);
            });

            Parallel.ForEach(customerDelinquentsAbis, rptf =>{
                var item = rptf.Rptft;
                var contractDetail = rptf.AbisDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate,
                    item.RemainingPenalty, item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation =
                    customersDelinquents.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber);
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) {
                    status = "0";
                }
                else{
                    if (contractDetail.OverDue1 == 0 && contractDetail.OverDue2 == 0 && contractDetail.OverDue3 == 0) status = "6";
                    else if (contractDetail.OverDue3 > 0) {
                        status = "2";
                    }
                    else if (contractDetail.OverDue2 > 0) {
                        status = "5";
                    }
                    else if (contractDetail.OverDue1 > 0) { status = "1"; }
                }
                item.Status = status;
                cD.Remaining = (decimal) (_variableConventor.ConvertDoubleDecimal(contractDetail.RemainingAmount) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue1) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue2) +
                                          _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue3));
                cD.RemainingProfit = (decimal) (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                                contractDetail.SoudeOverDue3 + contractDetail.SoudeTashilateJari);
                // cD.RemainingPenalty = item.RemainingPenalty;
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + item.RemainingPenalty;
                decimal mandehJari = 0;
                decimal mandehGheirJari = 0;
                if (geoDate > _dateTimeConvertor.GetGregorianFromPersian("1394/09/01")){
                    mandehJari = contractDetail.RemainingAmount;
                    mandehGheirJari = contractDetail.OverDue1 + contractDetail.OverDue2 +
                                      contractDetail.OverDue3;
                }
                else{
                    mandehJari = contractDetail.RemainingAmount +
                                 (contractDetail.SoudeTashilateJari.HasValue
                                     ? (decimal) contractDetail.SoudeTashilateJari
                                     : 0);
                    mandehGheirJari = (contractDetail.OverDue1 + contractDetail.OverDue2 +
                                       contractDetail.OverDue3) +
                                      (decimal)
                                          (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                           contractDetail.SoudeOverDue3);
                }

                cD.MandehJary = mandehJari;
                cD.MandehGheirJari = mandehGheirJari;
                var lastAction = lastActions.FirstOrDefault(x => x.ContractCode == item.ContractCode);
                if (lastAction != null) cD.LastAction = lastAction.GetType().Name;
                cDLists.Add(cD);
            });
            //guarantee
                var details = await _guaranteeDetailRepository.GetByPersianDate(dateWithOutSlash);
                var guarantees = await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(dateWithOutSlash);
            Parallel.ForEach(guarantees, guarantee =>{

                var existsItem = cDLists.FirstOrDefault(o => o.ContractCode == guarantee.ContractCode);
                var guarantee1 = guarantee;
                var detail = details.FirstOrDefault(x => x.ContractCode == guarantee1.ContractCode);
                if (existsItem != null){
                    existsItem.GuaranteeStatus = guarantee.GuaranteeStatus;
                    existsItem.DebitorAmount = guarantee.Guarantee.DebitorAmount;
                    if (detail != null){
                        existsItem.PaymentDate = detail.PaymentDate;
                        existsItem.Jarimeh = detail.Jarimeh;
                    }
                }
            });
                return cDLists;
        }
        public async Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(
           GetAllCustomerDelinquentsHistoryDto allCustomerDelinquentsHistoryDto)
        {
            var cDLists = new ConcurrentBag<CustomerDelinquentDto>();
            var dateWithOutSlash = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(allCustomerDelinquentsHistoryDto.PersianDate);
            var customerDelinquents = (await _rptftRepository.GetBankIranCustomerDelinquentAsync(dateWithOutSlash)).ToArray();
            var branches = await _branchRepository.GetAllAsNoTracking();
            var customersDelinquents =await _customerDelinquentRepository.GetAllAsync();
           
            var customerDelinquentsAbis = (await _rptftRepository.GetAbisCustomerDelinquentAsync(dateWithOutSlash)).ToArray();
            //var customerNumberArray =
            //   customerDelinquents.Select(x => x.Rptft.CustomerNumber)
            //       .Concat(customerDelinquentsAbis.Select(x => x.Rptft.CustomerNumber))
            //       .ToArray();
           // var customers = await _customerInfoRepository.GetCustomersAsync(customerNumberArray);

            var geoDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(allCustomerDelinquentsHistoryDto.PersianDate);

            Parallel.ForEach(customerDelinquents, t => {
                var item = t.Rptft;
                var contractDetail = t.BankIranDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, 0, item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation = customersDelinquents.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber);
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) { status = "0"; }
                else
                {
                    if (contractDetail.CodeSarFaslGherjari != "0965" && contractDetail.CodeSarFaslGherjari != "1010" && contractDetail.CodeSarFaslGherjari != "0960")
                        status = "6";
                    else if (contractDetail.CodeSarFaslGherjari == "0965") { status = "2"; }
                    else if (contractDetail.CodeSarFaslGherjari == "1010") { status = "5"; }
                    else if (contractDetail.CodeSarFaslGherjari == "0960") { status = "1"; }
                }
                cD.Status = status;
                cD.Remaining = contractDetail.MandeAghsatGherjari + contractDetail.MandeJari;
                cD.RemainingProfit = contractDetail.MandesudeJari + contractDetail.MandeAghsatGherjari;
                cD.RemainingPenalty = Convert.ToDecimal(contractDetail.Mandevajheltezamdaryafti);
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + cD.RemainingPenalty;
                cD.MandehJary = contractDetail.MandesudeJari;
                cD.MandehGheirJari = contractDetail.MandeAghsatGherjari;
                cD.BankType = (int)BankType.BankIran;

                //  var lastAction = await _logBaseRepository.GetLastLogAsync(item.ContractCode, geoDate);
                // if (lastAction != null)
                // cD.LastAction = lastAction.GetType().Name;
                cDLists.Add(cD);
            });

            #region OldCode

          
            //foreach (var t in customerDelinquents.AsParallel()) {
            //    var item = t.Rptft;
            //    var contractDetail = t.BankIranDetail;
            //    var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
            //        branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
            //        item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, 0, item.ContrantDescription);
            //    cD.ContractType = GetContractType(item.ContractCode);
            //    var customerInformation = customersDelinquents.FirstOrDefault(x=>x.CustomerNumber== item.CustomerNumber);
            //    cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
            //    var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
            //    var status = "0";
            //    if (date.Date >= DateTime.Now.Date) { status = "0"; }
            //    else
            //    {
            //        if (contractDetail.CodeSarFaslGherjari != "0965" && contractDetail.CodeSarFaslGherjari != "1010" && contractDetail.CodeSarFaslGherjari != "0960")
            //            status = "6";
            //        else if (contractDetail.CodeSarFaslGherjari == "0965") { status = "2"; }
            //        else if (contractDetail.CodeSarFaslGherjari == "1010") { status = "5"; }
            //        else if (contractDetail.CodeSarFaslGherjari == "0960") { status = "1"; }
            //    }
            //    cD.Status = status;
            //    cD.Remaining = contractDetail.MandeAghsatGherjari + contractDetail.MandeJari;
            //    cD.RemainingProfit = contractDetail.MandesudeJari + contractDetail.MandeAghsatGherjari;
            //    cD.RemainingPenalty = Convert.ToDecimal(contractDetail.Mandevajheltezamdaryafti);
            //    cD.DebitBalance = cD.Remaining + cD.RemainingProfit + cD.RemainingPenalty;
            //    cD.MandehJary = contractDetail.MandesudeJari;
            //    cD.MandehGheirJari = contractDetail.MandeAghsatGherjari;
            //    cD.BankType = (int)BankType.BankIran;

            //    //  var lastAction = await _logBaseRepository.GetLastLogAsync(item.ContractCode, geoDate);
            //    // if (lastAction != null)
            //    // cD.LastAction = lastAction.GetType().Name;
            //    cDLists.Add(cD);
            //}
            


            //foreach (var t in customerDelinquentsAbis) {
            //    var item = t.Rptft;
            //    var contractDetail = t.AbisDetail;
            //    var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
            //        branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
            //        item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, item.RemainingPenalty, item.ContrantDescription);
            //    cD.ContractType = GetContractType(item.ContractCode);
            //    var customerInformation = customersDelinquents.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber);
            //    cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
            //    var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
            //    var status = "0";
            //    if (date.Date >= DateTime.Now.Date) { status = "0"; }
            //    else
            //    {
            //        if (contractDetail.OverDue1 == 0 && contractDetail.OverDue2 == 0 && contractDetail.OverDue3 == 0) status = "6";
            //        else if (contractDetail.OverDue3 > 0) { status = "2"; }
            //        else if (contractDetail.OverDue2 > 0) { status = "5"; }
            //        else if (contractDetail.OverDue1 > 0) { status = "1"; }
            //    }
            //    item.Status = status;
            //    cD.Remaining = (decimal)(_variableConventor.ConvertDoubleDecimal(contractDetail.RemainingAmount) +
            //                             _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue1) +
            //                             _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue2) +
            //                             _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue3));
            //    cD.RemainingProfit = (decimal)(contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
            //                                   contractDetail.SoudeOverDue3 + contractDetail.SoudeTashilateJari);
            //    // cD.RemainingPenalty = item.RemainingPenalty;
            //    cD.DebitBalance = cD.Remaining + cD.RemainingProfit + item.RemainingPenalty;
            //    decimal mandehJari = 0;
            //    decimal mandehGheirJari = 0;
            //    if (date > _dateTimeConvertor.GetGregorianFromPersian("1394/09/01"))
            //    {
            //        mandehJari = contractDetail.RemainingAmount;
            //        mandehGheirJari = contractDetail.OverDue1 + contractDetail.OverDue2 +
            //                          contractDetail.OverDue3;
            //    }
            //    else
            //    {
            //        mandehJari = contractDetail.RemainingAmount + (contractDetail.SoudeTashilateJari.HasValue ? (decimal)contractDetail.SoudeTashilateJari : 0);
            //        mandehGheirJari = (contractDetail.OverDue1 + contractDetail.OverDue2 +
            //                           contractDetail.OverDue3) + (decimal)(contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 + contractDetail.SoudeOverDue3);
            //    }

            //    cD.MandehJary = mandehJari;
            //    cD.MandehGheirJari = mandehGheirJari;
            //    //  var lastAction = await _logBaseRepository.GetLastLogAsync(item.ContractCode, geoDate);
            //    //  if(lastAction !=null)
            //    // cD.LastAction = lastAction.GetType().Name;
            //    cDLists.Add(cD);
            //}
            //var details = await _guaranteeDetailRepository.GetByPersianDate(dateWithOutSlash);
            //var guarantees = await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(dateWithOutSlash);
            //foreach (var guarantee in guarantees)
            //{
            //    var existsItem = cDLists.FirstOrDefault(o => o.ContractCode == guarantee.ContractCode);
            //    var guarantee1 = guarantee;
            //    var detail = details.FirstOrDefault(x => x.ContractCode == guarantee1.ContractCode);
            //    if (existsItem == null) continue;
            //    existsItem.GuaranteeStatus = guarantee.GuaranteeStatus;
            //    existsItem.DebitorAmount = guarantee.Guarantee.DebitorAmount;

            //    if (detail != null)
            //    {
            //        existsItem.PaymentDate = detail.PaymentDate;
            //        existsItem.Jarimeh = detail.Jarimeh;
            //    }
            //}
            #endregion

            Parallel.ForEach(customerDelinquentsAbis, t => {
                var item = t.Rptft;
                var contractDetail = t.AbisDetail;
                var cD = _customerDelinquentDtoFactory.Create(item.BranchCode,
                    branches.FirstOrDefault(x => x.Code == item.BranchCode).Name, item.MaturityDate, item.StartDate,
                    item.CustomerNumber, "", item.ContractCode, "", false, item.ApprovedAmount, item.InterestRate, item.RemainingPenalty, item.ContrantDescription);
                cD.ContractType = GetContractType(item.ContractCode);
                var customerInformation = customersDelinquents.FirstOrDefault(x => x.CustomerNumber == item.CustomerNumber);
                cD.FullName = customerInformation == null ? string.Empty : customerInformation.FullName;
                var date = _dateTimeConvertor.GetGregorianFromPersian(item.MaturityDate);
                var status = "0";
                if (date.Date >= DateTime.Now.Date) { status = "0"; }
                else
                {
                    if (contractDetail.OverDue1 == 0 && contractDetail.OverDue2 == 0 && contractDetail.OverDue3 == 0) status = "6";
                    else if (contractDetail.OverDue3 > 0) { status = "2"; }
                    else if (contractDetail.OverDue2 > 0) { status = "5"; }
                    else if (contractDetail.OverDue1 > 0) { status = "1"; }
                }
                item.Status = status;
                cD.Remaining = (decimal)(_variableConventor.ConvertDoubleDecimal(contractDetail.RemainingAmount) +
                                         _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue1) +
                                         _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue2) +
                                         _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue3));
                cD.RemainingProfit = (decimal)(contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                               contractDetail.SoudeOverDue3 + contractDetail.SoudeTashilateJari);
                // cD.RemainingPenalty = item.RemainingPenalty;
                cD.DebitBalance = cD.Remaining + cD.RemainingProfit + item.RemainingPenalty;
                decimal mandehJari = 0;
                decimal mandehGheirJari = 0;
                if (date > _dateTimeConvertor.GetGregorianFromPersian("1394/09/01"))
                {
                    mandehJari = contractDetail.RemainingAmount;
                    mandehGheirJari = contractDetail.OverDue1 + contractDetail.OverDue2 +
                                      contractDetail.OverDue3;
                }
                else
                {
                    mandehJari = contractDetail.RemainingAmount + (contractDetail.SoudeTashilateJari.HasValue ? (decimal)contractDetail.SoudeTashilateJari : 0);
                    mandehGheirJari = (contractDetail.OverDue1 + contractDetail.OverDue2 +
                                       contractDetail.OverDue3) + (decimal)(contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 + contractDetail.SoudeOverDue3);
                }

                cD.MandehJary = mandehJari;
                cD.MandehGheirJari = mandehGheirJari;
                //  var lastAction = await _logBaseRepository.GetLastLogAsync(item.ContractCode, geoDate);
                //  if(lastAction !=null)
                // cD.LastAction = lastAction.GetType().Name;
                cDLists.Add(cD);

            });
            //guarantee
            var details = await _guaranteeDetailRepository.GetByPersianDate(dateWithOutSlash);
            var guarantees = await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(dateWithOutSlash);

            Parallel.ForEach(guarantees, guarantee =>
            {
                var existsItem = cDLists.FirstOrDefault(o => o.ContractCode == guarantee.ContractCode);
                var guarantee1 = guarantee;
                var detail = details.FirstOrDefault(x => x.ContractCode == guarantee1.ContractCode);
                if (existsItem != null)
                {
                    existsItem.GuaranteeStatus = guarantee.GuaranteeStatus;
                    existsItem.DebitorAmount = guarantee.Guarantee.DebitorAmount;

                    if (detail != null)
                    {
                        existsItem.PaymentDate = detail.PaymentDate;
                        existsItem.Jarimeh = detail.Jarimeh;
                    }
                } 
              
            });

            return cDLists;
        }

       

        public async Task<CountAndSumDto> GetAllDebtsCountByBranchCodeAsync(GetAllDebtsCountByBranchCodeDto getAllDebtsByBranchCodeDto)
        {
            var count = await _customerDelinquentRepository.GetAllDebtsCountAsync(getAllDebtsByBranchCodeDto.BranchCode);
            decimal sum = 0;
            if (count > 0)
                sum = await _customerDelinquentRepository.GetAllDebtsSumAsync(getAllDebtsByBranchCodeDto.BranchCode);
            return new CountAndSumDto(count, sum);
        }

        #endregion
        // check user in sharepoint list
        protected void CheckUserInBranchServiceGroup(string userName){
            if(!_sharepointAuthorizationService.IsExistInBranchService(userName))
                throw  new FaultException("کاربری نامعتبر است");
        }

        protected string GetContractType(string contractCode){            
            var strCode = contractCode.Substring(0, 2);
            var type= (ContractType)int.Parse(strCode);
           return _contractType[(int) type];
        }
    }
}