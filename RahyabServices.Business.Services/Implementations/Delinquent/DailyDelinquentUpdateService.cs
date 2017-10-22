using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class DailyDelinquentUpdateService : IDailyDelinquentUpdateService{
        private readonly IAbisDetailRepository _abisDetailRepository;
        private readonly IBankIranDetailRepository _bankIranDetailRepository;
        private readonly ILogBaseRepository _baseRepository;
        private readonly IBranchClaimRepository _branchClaimRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IChangeStatusLogFactory _changeStatusLogFactory;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly ICustomerDelinquentFactory _customerDelinquentFactory;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly IGuaranteeDetailRepository _guaranteeDetailRepository;
        private readonly IMonitorRepository _monitorRepository;
        private readonly IRPTFTRepository _rptftRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IVariableConventor _variableConventor;
        public DailyDelinquentUpdateService(IRPTFTRepository rptftRepository,
            ICustomerDelinquentRepository customerDelinquentRepository,
            ICustomerDelinquentFactory customerDelinquentFactory, IDateTimeConvertor dateTimeConvertor,
            ICustomerAccountService customerAccountService, IStateRepository stateRepository,
            IAbisDetailRepository abisDetailRepository, IBankIranDetailRepository bankIranDetailRepository,
            IBranchRepository branchRepository, IVariableConventor variableConventor,
            IBranchClaimRepository branchClaimRepository, IMonitorRepository monitorRepository,
            ILogBaseRepository baseRepository, IChangeStatusLogFactory changeStatusLogFactory,
            IGuaranteeDetailRepository guaranteeDetailRepository, ICustomerInfoRepository customerInfoRepository){
            _rptftRepository = rptftRepository;
            _customerDelinquentRepository = customerDelinquentRepository;
            _customerDelinquentFactory = customerDelinquentFactory;
            _dateTimeConvertor = dateTimeConvertor;
            _customerAccountService = customerAccountService;
            _stateRepository = stateRepository;
            _abisDetailRepository = abisDetailRepository;
            _bankIranDetailRepository = bankIranDetailRepository;
            _branchRepository = branchRepository;
            _variableConventor = variableConventor;
            _branchClaimRepository = branchClaimRepository;
            _monitorRepository = monitorRepository;
            _baseRepository = baseRepository;
            _changeStatusLogFactory = changeStatusLogFactory;
            _guaranteeDetailRepository = guaranteeDetailRepository;
            _customerInfoRepository = customerInfoRepository;
        }
        public async Task<bool> UpdateWorkingCopyOfDatabaseAsync(){
            //var branches2 = await _branchRepository.GetAllAsNoTracking();
            //foreach (var branch in branches2)
            //{

            //        var customerDelinquents = await _customerDelinquentRepository.GetContractsByBranchAsync(branch.Code);
            //        var tasksQuery =
            //        from cust in customerDelinquents select UpdateBranchId(cust, branch.Id);
            //        var tasks = tasksQuery.ToArray();
            //        await Task.WhenAll(tasks);

            //}
         //   await AddFullName();

           await UpdateWorkingCopyOfBankIranDatabaseAsync(DateTime.Now.Date.AddDays(-3));
           await UpdateWorkingCopyOfAbisDatabaseAsync(DateTime.Now.Date.AddDays(-2));
           await UpdateWorkingCopyOfGuaranteeDatabaseAsync(DateTime.Now.Date.AddDays(-2));
           await UpdateBranchClaim();
            return true;
        }
        public async Task<bool> UpdateBranchClaim(){
            var dateWithSlash = _dateTimeConvertor.GetPersianDate(DateTime.Now.AddDays(-1));
            var dateWithSlashAbis = _dateTimeConvertor.GetPersianDate(DateTime.Now.AddDays(-1));
            if (!await _monitorRepository.IsFinishAbisAndBankIranTask(dateWithSlash, dateWithSlashAbis)) return false;
            var branches = await _branchRepository.GetAllAsNoTracking();
            await Task.WhenAll(branches.Select(ProcessCustomerDelinquent));
            return true;
        }
        public async Task<bool> UpdateWorkingCopyOfAbisDatabaseAsync(DateTime date){
            try{
                var dateWithSlash = _dateTimeConvertor.GetPersianDate(date);
                if (!await _monitorRepository.IsFinishAbisTask(dateWithSlash)) return false;
                var cdMaxDate = await _customerDelinquentRepository.GetMaxHisDate(BankType.Abis);
                var bankMaxDate = await _rptftRepository.GetAbisMaxHisDate();
                if (cdMaxDate == bankMaxDate) return false;
                var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
                var newItems =
                    (await _rptftRepository.GetAbisTodayCustomerDelinquentUpdatedItemsAsync(desireDate)).ToArray();
                if (newItems.Length == 0) return false;
                var allCd = await _customerDelinquentRepository.GetAllAbisAsync();
                var branches = await _branchRepository.GetAllAsNoTracking();
                IEnumerable<Branch> branchEnumerable = branches as Branch[] ?? branches.ToArray();
                var ids = new List<int>();

                #region Loop    

                for (var i = 0; i < newItems.Length; i++){
                    try{
                        var newItem = newItems[i];
                        //Get My Db
                        var existsItem = allCd.FirstOrDefault(o => o.ContractCode == newItem.Rptft.ContractCode);
                        var maturityDate = await
                            _dateTimeConvertor.GetGregorianFromPersianAsync(newItem.Rptft.MaturityDate);
                        CustomerDelinquent customerDelinquent;
                        var contractDetail = newItem.AbisDetail;
                        var flag = false;
                        if (existsItem == null){
                            var branch = branchEnumerable.FirstOrDefault(x => x.Code == newItem.Rptft.BranchCode);
                            var customerInformation =
                                await _customerInfoRepository.GetByCustomerNumberAsync(newItem.Rptft.CustomerNumber);
                            customerDelinquent =
                                _customerDelinquentFactory.Create(newItem.Rptft.BranchCode, newItem.Rptft.BranchName,
                                    maturityDate,
                                    await _dateTimeConvertor.GetGregorianFromPersianAsync(newItem.Rptft.StartDate),
                                    newItem.Rptft.CustomerNumber, "0", newItem.Rptft.ContractCode,
                                    newItem.Rptft.HistoryDate,
                                    await
                                        _customerAccountService.GetCustomerMobileNumberAsync(
                                            newItem.Rptft.CustomerNumber), false,
                                    newItem.Rptft.ApprovedAmount, newItem.Rptft.InterestRate,
                                    newItem.Rptft.RemainingPenalty, newItem.Rptft.ResourceId,
                                    newItem.Rptft.ContrantDescription, GetContractType(newItem.Rptft.ContractCode),
                                    customerInformation == null ? string.Empty : customerInformation.FullNameManaged);
                            var registerStateHandler = new RegisterStateHandler(maturityDate.AddDays(-30),
                                _stateRepository,
                                _customerDelinquentRepository);
                            customerDelinquent.SetState(registerStateHandler.Id);
                            customerDelinquent.SetBranchId(branch.Id);
                        }
                        else{
                            if (!existsItem.IsArchived) {
                                flag = true;
                            }
                            else{
                                existsItem.ApprovedAmount = newItem.Rptft.ApprovedAmount;
                                existsItem.InterestRate = newItem.Rptft.InterestRate;
                                existsItem.RemainingPenalty = newItem.Rptft.RemainingPenalty;
                                existsItem.IsArchived = false;
                            }
                            customerDelinquent = existsItem;
                        }

                        //Get Status from rptft031

                        // await _abisDetailRepository.OneAsync(x => x.ContractCode == customerDelinquent.ContractCode && x.HisDate == customerDelinquent.HistoryDate);
                        var status = "0";
                        if (customerDelinquent.MaturityDate.Date >= DateTime.Now.Date) {
                            status = "0";
                        }
                        else{
                            if (contractDetail.OverDue1 == 0 && contractDetail.OverDue2 == 0 &&
                                contractDetail.OverDue3 == 0) status = "6";
                            else if (contractDetail.OverDue3 > 0) {
                                status = "2";
                            }
                            else if (contractDetail.OverDue2 > 0) {
                                status = "5";
                            }
                            else if (contractDetail.OverDue1 > 0) { status = "1"; }
                        }
                        var remaining =
                            (decimal) (_variableConventor.ConvertDoubleDecimal(contractDetail.RemainingAmount) +
                                       _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue1) +
                                       _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue2) +
                                       _variableConventor.ConvertDoubleDecimal(contractDetail.OverDue3));
                        var remainingProfit = (decimal) (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                                         contractDetail.SoudeOverDue3 +
                                                         contractDetail.SoudeTashilateJari);
                        var debitBalance = remaining + remainingProfit + customerDelinquent.RemainingPenalty;
                        var statusFlag = customerDelinquent.Status != status;
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
                            mandehGheirJari = contractDetail.OverDue1 + contractDetail.OverDue2 +
                                              contractDetail.OverDue3 +
                                              (decimal)
                                                  (contractDetail.SoudeOverDue1 + contractDetail.SoudeOverDue2 +
                                                   contractDetail.SoudeOverDue3);
                        }
                        if (flag && customerDelinquent.DebitBalance == debitBalance && !statusFlag) {}
                        else{
                            customerDelinquent.HistoryDate = newItem.Rptft.HistoryDate;
                            customerDelinquent.Status = status;
                            customerDelinquent.Remaining = remaining;
                            customerDelinquent.DebitBalance = debitBalance;
                            customerDelinquent.RemainingProfit = remainingProfit;
                            customerDelinquent.MandehJari = mandehJari;
                            customerDelinquent.MandehGheireJary = mandehGheirJari;
                            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
                            if (statusFlag){
                                var changeStatus = _changeStatusLogFactory.Create(customerDelinquent.Id, status);
                                await _baseRepository.SaveAsync(changeStatus);
                            }
                        }
                        ids.Add(customerDelinquent.Id);
                    }
                    catch (Exception ex) {}
                }

                #endregion

                var archivedItems =
                    await _customerDelinquentRepository.GetItemsNotInDateNoTrackingAsync(desireDate, BankType.Abis);
                var itemsShouldBeUpdate = archivedItems.Where(x => !ids.Contains(x.Id));
                await Task.WhenAll(itemsShouldBeUpdate.Select(UpdateItem));
                return true;
            }
            catch (Exception ex) {
                throw;
            }
        }
        public async Task<bool> UpdateWorkingCopyOfBankIranDatabaseAsync(DateTime date){
            var dateWithSlash = _dateTimeConvertor.GetPersianDate(date);
            if (!await _monitorRepository.IsFinishBankIranTask(dateWithSlash)) return false;
            var cdMaxDate = await _customerDelinquentRepository.GetMaxHisDate(BankType.BankIran);
            var bankMaxDate = await _rptftRepository.GetBankIranMaxHisDate();
            if (cdMaxDate == bankMaxDate) return false;
            var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
            // shamel zemanat name ham mishavad
            var newItems =
                (await _rptftRepository.GetBankIranTodayCustomerDelinquentUpdatedItemsAsync(desireDate)).ToArray();
            if (newItems.Length == 0) return false;
            var allCd = await _customerDelinquentRepository.GetAllBankIranAsync();
            var branches = await _branchRepository.GetAllAsNoTracking();
            //guarantee
            // var guarantees =await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(desireDate);
            var ids = new List<int>();

            #region Loop

            for (var i = 0; i < newItems.Length; i++){
                try{
                    var newItem = newItems[i];
                    var existsItem = allCd.FirstOrDefault(o => o.ContractCode == newItem.Rptft.ContractCode);
                    var maturityDate = await
                        _dateTimeConvertor.GetGregorianFromPersianAsync(newItem.Rptft.MaturityDate);
                    var flag = false;
                    CustomerDelinquent customerDelinquent;
                    if (existsItem == null){
                        var branch = branches.FirstOrDefault(x => x.Code == newItem.Rptft.BranchCode);
                        var customerInformation =
                            await _customerInfoRepository.GetByCustomerNumberAsync(newItem.Rptft.CustomerNumber);
                        customerDelinquent =
                            _customerDelinquentFactory.Create(newItem.Rptft.BranchCode, newItem.Rptft.BranchName,
                                maturityDate,
                                await _dateTimeConvertor.GetGregorianFromPersianAsync(newItem.Rptft.StartDate),
                                newItem.Rptft.CustomerNumber, "0", newItem.Rptft.ContractCode, newItem.Rptft.HistoryDate,
                                await _customerAccountService.GetCustomerMobileNumberAsync(newItem.Rptft.CustomerNumber),
                                false,
                                newItem.Rptft.ContractCode.StartsWith("60")
                                    ? Convert.ToDecimal(newItem.Rptft.ZemanatAmount)
                                    : newItem.Rptft.ApprovedAmount,
                                newItem.Rptft.InterestRate, 0, newItem.Rptft.ResourceId,
                                newItem.Rptft.ContrantDescription, GetContractType(newItem.Rptft.ContractCode),
                                customerInformation == null ? string.Empty : customerInformation.FullNameManaged);
                        var registerStateHandler = new RegisterStateHandler(maturityDate.AddDays(-30), _stateRepository,
                            _customerDelinquentRepository);
                        customerDelinquent.SetState(registerStateHandler.Id);
                        customerDelinquent.SetBranchId(branch.Id);
                    }
                    else{
                        if (!existsItem.IsArchived) {
                            flag = true;
                        }
                        else{
                            existsItem.HistoryDate = newItem.Rptft.HistoryDate;
                            //baraye zemanatname
                            existsItem.ApprovedAmount = newItem.Rptft.ContractCode.StartsWith("60")
                                ? Convert.ToDecimal(newItem.Rptft.ZemanatAmount)
                                : newItem.Rptft.ApprovedAmount;
                            existsItem.InterestRate = newItem.Rptft.InterestRate;
                            existsItem.IsArchived = false;
                        }
                        customerDelinquent = existsItem;
                    }
                    //var guarantee =
                    //    guarantees.FirstOrDefault(x => x.ContractCode == customerDelinquent.ContractCode);
                    //if (guarantee != null){
                    //    if (customerDelinquent.GuaranteeStatus != guarantee.GuaranteeStatus ){
                    //        customerDelinquent.GuaranteeStatus = guarantee.GuaranteeStatus;
                    //        flag = false;
                    //    }  
                    //}

                    //Get Status from rptft031
                    var contractDetail = newItem.BankIranDetail;
                    // await _bankIranDetailRepository.OneAsync(x => x.ContractCode == customerDelinquent.ContractCode && x.HisDate == customerDelinquent.HistoryDate);
                    var status = "0";
                    if (customerDelinquent.MaturityDate.Date >= DateTime.Now.Date) {
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
                    var statusFlag = customerDelinquent.Status != status;
                    var remaining = contractDetail.MandeAghsatGherjari + contractDetail.MandeJari;
                    decimal remainingProfit = 0;
                    //baraye zemanatname ha hamishe mande az soud sefr ast
                    if (!contractDetail.ContractCode.StartsWith("60")) remainingProfit = contractDetail.MandesudeJari + contractDetail.Mandesudegherjari;
                    //check zemanatname
                    decimal remainingPenalty = 0;
                    if (!contractDetail.ContractCode.StartsWith("60")) remainingPenalty = Convert.ToDecimal(contractDetail.Mandevajheltezamdaryafti);
                    var debitBalance = remaining + remainingProfit + remainingPenalty;
                    if (flag && customerDelinquent.DebitBalance == debitBalance && customerDelinquent.Status == status) {}
                    else{
                        customerDelinquent.Status = status;
                        customerDelinquent.Remaining = remaining;
                        customerDelinquent.RemainingPenalty = remainingPenalty;
                        customerDelinquent.RemainingProfit = remainingProfit;
                        customerDelinquent.DebitBalance = debitBalance;
                        customerDelinquent.MandehJari = contractDetail.MandesudeJari;
                        customerDelinquent.MandehGheireJary = contractDetail.MandeAghsatGherjari;
                        await _customerDelinquentRepository.SaveAsync(customerDelinquent);
                        if (statusFlag){
                            var changeStatus = _changeStatusLogFactory.Create(customerDelinquent.Id, status);
                            await _baseRepository.SaveAsync(changeStatus);
                        }
                    }
                    ids.Add(customerDelinquent.Id);
                }
                catch (Exception ex) {}
            }

            #endregion

            var archivedItems =
                await _customerDelinquentRepository.GetItemsNotInDateNoTrackingAsync(desireDate, BankType.BankIran);
            var itemsShouldBeUpdate = archivedItems.Where(x => !ids.Contains(x.Id));
            await Task.WhenAll(itemsShouldBeUpdate.Select(UpdateItem));
            return true;
        }
        //in method bad az update bankiran bayad run shavad
        public async Task<bool> UpdateWorkingCopyOfGuaranteeDatabaseAsync(DateTime date){
            var dateWithSlash = _dateTimeConvertor.GetPersianDate(date);
            if (!await _monitorRepository.IsFinishBankIranTask(dateWithSlash)) return false;
            var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
            var details = await _guaranteeDetailRepository.GetByPersianDate(desireDate);
            var guarantees = await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(desireDate);
            var allCd = await _customerDelinquentRepository.GetAllBankIranAsync();
            for (var i = 0; i < guarantees.Length; i++){
                var flag = false;
                var guarantee = guarantees[i];
                var existsItem = allCd.FirstOrDefault(o => o.ContractCode == guarantee.ContractCode);
                var detail = details.FirstOrDefault(x => x.ContractCode == guarantee.ContractCode);
                if (existsItem == null) continue;
                if (existsItem.GuaranteeStatus != guarantee.GuaranteeStatus){
                    flag = true;
                    existsItem.GuaranteeStatus = guarantee.GuaranteeStatus;
                }
                if (existsItem.DebitorAmount != guarantee.Guarantee.DebitorAmount){
                    flag = true;
                    existsItem.DebitorAmount = guarantee.Guarantee.DebitorAmount;
                }
                if (detail != null){
                    if (detail.PaymentDate != existsItem.PaymentDate){
                        existsItem.PaymentDate = detail.PaymentDate;
                        flag = true;
                    }
                    if (detail.Jarimeh != existsItem.Jarimeh){
                        existsItem.Jarimeh = detail.Jarimeh;
                        flag = true;
                    }
                }
                if (flag) { await _customerDelinquentRepository.SaveAsync(existsItem); }
            }
            return true;
        }
        protected async Task UpdateItem(CustomerDelinquent customerDelinquent){
            customerDelinquent.IsArchived = true;
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        protected string GetStatusFromMaturityDate(DateTime maturityDate){
            if (DateTime.Now <= maturityDate) { return "0"; }
            var days = (DateTime.Now - maturityDate).TotalDays;
            if (0 <= days && days < 60) { return "6"; }
            if (60 <= days && days < 180) { return "1"; }
            if (180 <= days && days < 360) { return "5"; }
            return "2";
        }
        protected async Task ProcessCustomerDelinquent(Branch branch){
            var customerDelinquents = await _customerDelinquentRepository.GetContractsByBranchAsync(branch.Id);
            var customerDelinquentDebts =
                await _customerDelinquentRepository.GetAllDebtContractsByBranchAsync(branch.Id);
            var branchClaim = new BranchClaim();
            var delinquents = customerDelinquents as CustomerDelinquent[] ?? customerDelinquents.ToArray();
            foreach (var customerDelinquent in delinquents){
                branchClaim.TotalRemainingAmount +=
                    _variableConventor.ConvertDoubleDecimal(customerDelinquent.RemainingPenalty);
                branchClaim.TotalApprovedAmount +=
                    _variableConventor.ConvertDoubleDecimal(customerDelinquent.ApprovedAmount);
            }
            branchClaim.TotalCount = delinquents.Count();
            var delinquentDebts = customerDelinquentDebts as CustomerDelinquent[] ?? customerDelinquentDebts.ToArray();
            foreach (var customerDelinquentDebt in delinquentDebts){
                branchClaim.DebtRemainingAmount +=
                    _variableConventor.ConvertDoubleDecimal(customerDelinquentDebt.RemainingPenalty);
                branchClaim.DebtApprovedAmount +=
                    _variableConventor.ConvertDoubleDecimal(customerDelinquentDebt.ApprovedAmount);
            }
            branchClaim.DebtCount = delinquentDebts.Count();
            branchClaim.SetBranchId(branch.Id);
            branchClaim.Created = DateTime.Now.Date;
            await _branchClaimRepository.SaveAsync(branchClaim);
        }
        protected ContractType GetContractType(string contractCode){
            var strCode = contractCode.Substring(0, 2);
            return (ContractType) int.Parse(strCode);
        }
        protected async Task AddFullName(){
            var allCd = await _customerDelinquentRepository.GetAllBankIranAsync();
            foreach (var customerDelinquent in allCd) {
                var customerInformation =
                               await _customerInfoRepository.GetByCustomerNumberAsync(customerDelinquent.CustomerNumber);
                customerDelinquent.FullName = customerInformation == null
                    ? string.Empty
                    : customerInformation.FullNameManaged;
             await   _customerDelinquentRepository.SaveAsync(customerDelinquent);
            }

        }

        //public async Task<bool> UpdateWorkingCopyOfAbisDatabaseAsync1(DateTime date)
        //{
            
        //        var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
        //        var newItems =
        //            (await _rptftRepository.GetAbisTodayCustomerDelinquentUpdatedItemsAsync(desireDate)).ToArray();
        //        if (newItems.Length == 0) return false;
        //        var allCd = await _customerDelinquentRepository.GetAllAbisAsync();
        //        var ids = new List<CustomerDelinquent>();

        //        for (var i = 0; i < newItems.Length; i++)
        //        {
        //            try
        //            {
        //                var newItem = newItems[i];
        //                //Get My Db
        //                var existsItem = allCd.FirstOrDefault(o => o.ContractCode == newItem.Rptft.ContractCode);
        //                CustomerDelinquent customerDelinquent;

        //                var flag = false;
        //                if (existsItem == null)
        //                {
        //                    continue;
        //                }
        //                customerDelinquent = existsItem;
        //                if (customerDelinquent.IsArchived)
        //                    ids.Add(customerDelinquent);
        //            }
        //            catch (Exception ex) { }
        //        }
        //        await Task.WhenAll(ids.Select(UpdateItemToFalse));
        //        return true;
            
        //}
        //public async Task<bool> UpdateWorkingCopyOfBankIranDatabaseAsync1(DateTime date)
        //{
        //    var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
        //    // shamel zemanat name ham mishavad
        //    var newItems =
        //        (await _rptftRepository.GetBankIranTodayCustomerDelinquentUpdatedItemsAsync(desireDate)).ToArray();
        //    if (newItems.Length == 0) return false;
        //    var allCd = await _customerDelinquentRepository.GetAllBankIranAsync();
        //    var ids = new List<CustomerDelinquent>();

        //    for (var i = 0; i < newItems.Length; i++)
        //    {
        //        try
        //        {
        //            var newItem = newItems[i];
        //            //Get My Db
        //            var existsItem = allCd.FirstOrDefault(o => o.ContractCode == newItem.Rptft.ContractCode);
        //            CustomerDelinquent customerDelinquent;

        //            var flag = false;
        //            if (existsItem == null)
        //            {
        //                continue;
        //            }
        //            customerDelinquent = existsItem;
        //            if(customerDelinquent.IsArchived)
        //            ids.Add(customerDelinquent);
        //        }
        //        catch (Exception ex) { }
        //    }
        //    await Task.WhenAll(ids.Select(UpdateItemToFalse));
        //    return true;
        //}
        //protected async Task UpdateItemToFalse(CustomerDelinquent customerDelinquent)
        //{
        //    customerDelinquent.IsArchived = false;
        //    await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        //}
        ////in method bad az update bankiran bayad run shavad
        //public async Task<bool> UpdateWorkingCopyOfGuaranteeDatabaseAsync1(DateTime date)
        //{
        //    var dateWithSlash = _dateTimeConvertor.GetPersianDate(date);
        //    if (!await _monitorRepository.IsFinishBankIranTask(dateWithSlash)) return false;
        //    var desireDate = await _dateTimeConvertor.GetHistoryDateFormatAsync(date);
        //    var details = await _guaranteeDetailRepository.GetByPersianDate(desireDate);
        //    var guarantees = await _rptftRepository.GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(desireDate);
        //    var allCd = await _customerDelinquentRepository.GetAllBankIranAsync();
        //    for (var i = 0; i < guarantees.Length; i++)
        //    {
        //        var flag = false;
        //        var guarantee = guarantees[i];
        //        var existsItem = allCd.FirstOrDefault(o => o.ContractCode == guarantee.ContractCode);
        //        var detail = details.FirstOrDefault(x => x.ContractCode == guarantee.ContractCode);
        //        if (existsItem == null) continue;
        //        if (existsItem.GuaranteeStatus != guarantee.GuaranteeStatus)
        //        {
        //            flag = true;
        //            existsItem.GuaranteeStatus = guarantee.GuaranteeStatus;
        //        }
        //        if (existsItem.DebitorAmount != guarantee.Guarantee.DebitorAmount)
        //        {
        //            flag = true;
        //            existsItem.DebitorAmount = guarantee.Guarantee.DebitorAmount;
        //        }
        //        if (detail != null)
        //        {
        //            if (detail.PaymentDate != existsItem.PaymentDate)
        //            {
        //                existsItem.PaymentDate = detail.PaymentDate;
        //                flag = true;
        //            }
        //            if (detail.Jarimeh != existsItem.Jarimeh)
        //            {
        //                existsItem.Jarimeh = detail.Jarimeh;
        //                flag = true;
        //            }
        //        }
        //        if (flag) { await _customerDelinquentRepository.SaveAsync(existsItem); }
        //    }
        //    return true;
        //}
    }
}