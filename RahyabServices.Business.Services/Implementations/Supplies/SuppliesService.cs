using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Domain.Models.Supplies;
using RahyabServices.Business.Dtos;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
using RahyabServices.DataAccess.Repositories.Supplies.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Supplies{
    public class SuppliesService : ISuppliesService{
        private readonly IDeactivateBaseIbanRequestRepository _baseIbanRequestRepository;
        private readonly IBranchCodeSystemRepository _branchCodeSystemRepository;
        private readonly IIranNaraChequeRequestRepository _chequeRequestRepository;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        // faghat too server haye bank kar mikonad
        private readonly IKarizFacade _karizFacade;
        private readonly ILogger _logger;
        private readonly IRequestSerialIdRepository _requestSerial;
        private readonly ISayadFacade _sayadFacade;
        private readonly ISuppliesRepository _suppliesRepository;
        private readonly ISuppliesRequestRepository _suppliesRequestRepository;
        public SuppliesService(ISuppliesRepository suppliesRepository, IKarizFacade karizFacade,
            ISuppliesRequestRepository suppliesRequestRepository, ICustomerInfoRepository customerInfoRepository,
            ISayadFacade sayadFacade, ICustomerAccountService customerAccountService, ILogger logger,
            IDeactivateBaseIbanRequestRepository baseIbanRequestRepository,
            IIranNaraChequeRequestRepository chequeRequestRepository, IDateTimeConvertor dateTimeConvertor,
            IRequestSerialIdRepository requestSerial, IBranchCodeSystemRepository branchCodeSystemRepository){
            _suppliesRepository = suppliesRepository;
            _karizFacade = karizFacade;
            _suppliesRequestRepository = suppliesRequestRepository;
            _customerInfoRepository = customerInfoRepository;
            _sayadFacade = sayadFacade;
            _customerAccountService = customerAccountService;
            _logger = logger;
            _baseIbanRequestRepository = baseIbanRequestRepository;
            _chequeRequestRepository = chequeRequestRepository;
            _dateTimeConvertor = dateTimeConvertor;
            _requestSerial = requestSerial;
            _branchCodeSystemRepository = branchCodeSystemRepository;
        }
        public async Task<AccountInformationDto> GetAccountInformation(GetAccountInformationDtq dtq){
            var accInfo = await _suppliesRepository.GetAccountOwners(dtq.AccountNumber);
            var accountOwners = new List<RespondAccountOwnerDto>();
            var customerInfoDt = accInfo.Tables[0];
            var faragirList = new List<FaragirDto>();
            //sefr mamooli , 1 eshteraki
            var sharedAccount = 0;
            if (accInfo.Tables.Count > 1){
                sharedAccount = 1;
                var partInfoDt = accInfo.Tables[1];
                foreach (DataRow dataRow in partInfoDt.Rows){
                    var partType = dataRow["CUSTYPE"].ToString() == "01" ? PartyType.Actual : PartyType.Legal;
                    if (partType == PartyType.Legal)
                        accountOwners.Add(new RespondAccountOwnerDto
                        {
                            PartyType = partType,
                            BirthDate = int.Parse(dataRow["BIRTH_DATE"].ToString()),
                            CityCode = dataRow["ISSUED_TOWN_Code"].ToString().Trim(),
                            Name = dataRow["FULL_NAME"].ToString().Trim(),
                            IdNum = dataRow["COMPGNO"].ToString().Trim(),
                            Identifier = dataRow["IDNUMBER-CORP"].ToString().Trim(),
                            IdentifierType = IdentifierType.NationalId
                        });
                    else{
                        //! baraye motevalin 68 be bad  
                        var birthDate = int.Parse(dataRow["BIRTH_DATE"].ToString());
                        var idNum = dataRow["IDNUMBER"].ToString().Trim();

                        //Todo: baraye atbae khareji
                        string identifier;
                        if (dataRow["NATIONALITY"].ToString() == "F"){
                            identifier = dataRow["FARAGIRNO"].ToString().Trim();
                            faragirList.Add(new FaragirDto
                            {
                                IdNum = dataRow["IDNUMBER"].ToString().Trim(),
                                Number = identifier
                            });
                        }
                        else{
                            identifier = dataRow["ECONOMIC-CODE"].ToString().Trim();

                            //Todo: 
                            //! baraye motevalin 68 be bad  
                            if (birthDate >= 13680101 && string.IsNullOrEmpty(idNum)) { idNum = identifier; }
                        }
                        accountOwners.Add(new RespondAccountOwnerDto
                        {
                            PartyType = partType,
                            BirthDate = birthDate,
                            CityCode = dataRow["ISSUED_TOWN_Code"].ToString().Trim(),
                            IdNum = idNum,
                            IdentifierType =
                                dataRow["NATIONALITY"].ToString() == "F"
                                    ? IdentifierType.ForeignId
                                    : IdentifierType.NationalCode,
                            Identifier = identifier,
                            FirstName = dataRow["FName"].ToString(),
                            LastName = dataRow["LName"].ToString()
                        });
                    }
                }
            }
            else{
                var dataRow = customerInfoDt.Rows[0];
                var partType = dataRow["CUSTYPE"].ToString() == "01" ? PartyType.Actual : PartyType.Legal;
                if (partType == PartyType.Legal)
                    accountOwners.Add(new RespondAccountOwnerDto
                    {
                        PartyType = partType,
                        BirthDate = int.Parse(dataRow["BIRTH_DATE"].ToString()),
                        CityCode = dataRow["ISSUED_TOWN_Code"].ToString().Trim(),
                        Name = dataRow["FULL_NAME"].ToString().Trim(),
                        IdNum = dataRow["COMPGNO"].ToString().Trim(),
                        Identifier = dataRow["IDNUMBER-CORP"].ToString().Trim(),
                        IdentifierType = IdentifierType.NationalId
                    });
                else{
                    //! baraye motevalin 68 be bad  
                    var birthDate = int.Parse(dataRow["BIRTH_DATE"].ToString());
                    var idNum = dataRow["IDNUMBER"].ToString().Trim();

                    //Todo: baraye atbae khareji
                    string identifier;
                    if (dataRow["NATIONALITY"].ToString() == "F"){
                        identifier = dataRow["FARAGIRNO"].ToString().Trim();
                        faragirList.Add(new FaragirDto
                        {
                            IdNum = dataRow["IDNUMBER"].ToString().Trim(),
                            Number = identifier
                        });
                    }
                    else{
                        identifier = dataRow["ECONOMIC-CODE"].ToString().Trim();
                        //Todo: 
                        //! baraye motevalin 68 be bad  
                        if (birthDate >= 13680101 && string.IsNullOrEmpty(idNum)) { idNum = identifier; }
                    }
                    accountOwners.Add(new RespondAccountOwnerDto
                    {
                        PartyType = partType,
                        BirthDate = birthDate,
                        CityCode = dataRow["ISSUED_TOWN_Code"].ToString().Trim(),
                        IdNum = idNum,
                        IdentifierType =
                            dataRow["NATIONALITY"].ToString() == "F"
                                ? IdentifierType.ForeignId
                                : IdentifierType.NationalCode,
                        Identifier = identifier,
                        FirstName = dataRow["FName"].ToString(),
                        LastName = dataRow["LName"].ToString()
                    });
                }
            }
            var kariz = await GetKarizInformation(dtq.AccountNumber);
            var custNumbers = kariz.Item1;
            var customers = await _customerInfoRepository.GetCustomersAsync(custNumbers);
            var signers = new List<AccountSignerDto>();
            foreach (var cust in customers){
                var partType = cust.CustomerType == "01" ? PartyType.Actual : PartyType.Legal;
                if (partType == PartyType.Legal){
                    //felan esme sherkat ro az tooye signer ha hazf shavad hamhangi ba aghay godazgar
                    signers.Add(new AccountSignerDto
                    {
                        CustomerName = cust.FullNameManaged,
                        // CustomerSurname = cust.LastName,
                        BirthDate = int.Parse("13" + cust.BirthDate),
                        IdNum = cust.CompanyNumber.Trim(),
                        Identifier = cust.CompanyId.Trim(),
                        IdentifierType = IdentifierType.NationalId,
                        CityCode = cust.IssuedTown
                    });
                }
                else{
                    //Todo: 
                    //! baraye motevalin 68 be bad  
                    var birthDate = int.Parse("13" + cust.BirthDate);
                    var idNum = cust.NationalNumber.Trim();
                    if (birthDate >= 13680101 && string.IsNullOrEmpty(idNum) && cust.Nationality == "0001") {
                        idNum = cust.EconomicCode.Trim();
                    }
                    var faragir = "";
                    if (faragirList.Any(x => x.IdNum == cust.NationalNumber.Trim())) faragir = faragirList.FirstOrDefault(x => x.IdNum == cust.NationalNumber.Trim()).Number;
                    signers.Add(new AccountSignerDto
                    {
                        CustomerName = cust.FirstName,
                        CustomerSurname = cust.LastName,
                        BirthDate = int.Parse("13" + cust.BirthDate),
                        IdNum = idNum,

                        // check shavad
                        IdentifierType = cust.Nationality != "0001"
                            ? IdentifierType.ForeignId
                            : IdentifierType.NationalCode,
                        Identifier =
                            cust.Nationality != "0001"
                                ? faragir
                                : cust.EconomicCode.Trim()
                    });
                }
            }
            var customerInformation = Mapper.Map<KarizResponseDto, AccountInformationDto>(kariz.Item2);
            customerInformation.AccountOwners = accountOwners;
            customerInformation.AccountSignerDtos = signers;
            customerInformation.SharedAccount = sharedAccount;
            return customerInformation;
        }
        public async Task<BriefAccountInformationDto> BriefAccountInformation(GetAccountInformationDtq dtq){
            var accInfo = await _suppliesRepository.GetAccountOwners(dtq.AccountNumber);
            var customerInfoDt = accInfo.Tables[0];
            var brief = new BriefAccountInformationDto();
            //sefr mamooli , 1 eshteraki
            //var sharedAccount = 0;
            //if (accInfo.Tables.Count > 1)
            //    sharedAccount = 1;
            var dataRow = customerInfoDt.Rows[0];
            var partType = dataRow["CUSTYPE"].ToString() == "01" ? PartyType.Actual : PartyType.Legal;
            brief.NationalId = partType == PartyType.Legal
                ? dataRow["IDNUMBER-CORP"].ToString().Trim()
                : dataRow["ECONOMIC-CODE"].ToString().Trim();
            brief.FullName = dataRow["FName"].ToString().Trim() + " " + dataRow["LName"].ToString().Trim();
            brief.CustType = dataRow["CUSTYPE"].ToString();
            brief.OpennerBranch = dataRow["ACCOUNT_OPNBR"].ToString().Length == 3
                ? "0" + dataRow["ACCOUNT_OPNBR"]
                : dataRow["ACCOUNT_OPNBR"].ToString();
            brief.CustomerNumber = dataRow["CUSTNO"].ToString();
            brief.Sheba = _customerAccountService.GetSheba(dtq.AccountNumber);
            return brief;
        }
        public async Task<bool> Inquiry(InquiryRequestDtc inquiryRequestDtc){
            var request = _suppliesRequestRepository.GetItemById(inquiryRequestDtc.ItemId);
            AccountInformationDto accountInfo;
            try{
                var acc = request.AccountNumber;
                accountInfo =
                    await
                        GetAccountInformation(new GetAccountInformationDtq {AccountNumber = acc});
                accountInfo.AccountNumber = acc;
                accountInfo.ApplicantBranchCode = !string.IsNullOrEmpty(request.CentralBranchCode)
                    ? request.CentralBranchCode
                    : GetBranchCodeSystem(request.ApplicantBranchCode).SystemCode;
                accountInfo.ApplicantBranchName = request.ApplicantBranchName.RemoveDashAndParenthesis();
                accountInfo.ItemId = "AB" + request.Id.Value;
                accountInfo.SheetCount = int.Parse(request.SheetCount.ToString());
                accountInfo.Sheba = _customerAccountService.GetSheba(acc); // request.Sheba;
            }
            catch (Exception ex){
                request.State = 9;
                request.StateDescription =
                    "خطا ،اطلاعات مشتری در دیتابیس بانک صحیح نمی باشد";
                request.SayadStatusDescription = ex.Message;
                request.DeliveryBranchCode = inquiryRequestDtc.DeliveryBranch;
                _suppliesRequestRepository.Update(request);
                return false;
            }
            try{
                var result = _sayadFacade.CallInsertChequeBookRequest(accountInfo);
                if (result.Code == 400){
                    request.State = 3;
                    request.StateDescription = "اطلاعات در سامانه صیاد با موفقیت ثبت شده است ، منتظر نتیجه استعلام";
                    request.SayadStatusCode = "400";
                    request.SayadStatusDescription = result.Description;
                    request.DeliveryBranchCode = inquiryRequestDtc.DeliveryBranch;
                    _suppliesRequestRepository.Update(request);
                    return true;
                }
                //handle bug : RequestCode Is Duplicate
                if (result.Code == 474) return false;
                request.State = 4;
                request.StateDescription = "بازگشت خطا از سامانه صیاد";
                request.SayadStatusCode = result.Code.ToString();
                request.SayadStatusDescription = result.Description;
                request.DeliveryBranchCode = inquiryRequestDtc.DeliveryBranch;
                _suppliesRequestRepository.Update(request);
                return false;
            }
            catch (Exception ex){
                request.State = 5;
                request.StateDescription =
                    "خطا ، بنا به دلایلی اطلاعات در سامانه صیاد ثبت نشده ، سیستم بعد از چند دقیقه دوباره تلاش خواهد کرد";
                request.SayadStatusDescription = ex.Message;
                request.DeliveryBranchCode = inquiryRequestDtc.DeliveryBranch;
                _suppliesRequestRepository.Update(request);
                return false;
            }
        }
        public async Task<bool> CheckSayadState(){
            var requests = _suppliesRequestRepository.GetRequestByState(3).ToList();
            var waitRequests = _suppliesRequestRepository.GetRequestByState(7);
            requests.AddRange(waitRequests);
            foreach (var suppliesRequest in requests){
                try{
                    var state =
                        (await _sayadFacade.Check("AB" + suppliesRequest.Id.Value))
                            .GetChequeBookStatusByInquiryCodeResult;
                    if (state.StatusCode == "10"){
                        suppliesRequest.State = 6;
                        suppliesRequest.SayadReasonCode = state.StatusReasonCode;
                        suppliesRequest.StateDescription = "منتظر تایید مجدد رئیس شعبه";
                        suppliesRequest.SayadStatusDescription = state.StatusDescription;
                        suppliesRequest.SayadStatusCode = state.StatusCode;
                        _suppliesRequestRepository.Update(suppliesRequest);
                    }
                    else if (state.StatusCode == "4"){
                        suppliesRequest.State = 8;
                        suppliesRequest.SayadReasonCode = state.StatusReasonCode;
                        suppliesRequest.StateDescription = "دسته چک در سامانه صیاد تایید شد";
                        suppliesRequest.SerialNoFrom = state.LstChequeSerial.First().ChequeSerial;
                        suppliesRequest.SerialNoTo = state.LstChequeSerial.Last().ChequeSerial;
                        suppliesRequest.SerialNumbers = string.Join(",",
                            state.LstChequeSerial.Select(x => x.ChequeSerial).ToArray());
                        suppliesRequest.SayadStatusDescription = state.StatusDescription;
                        suppliesRequest.SayadStatusCode = state.StatusCode;
                        _suppliesRequestRepository.Update(suppliesRequest);
                    }
                    else if (state.StatusCode == "3"){
                        suppliesRequest.State = -1;
                        suppliesRequest.SayadReasonCode = state.StatusReasonCode;
                        suppliesRequest.StateDescription = "دسته چک در سامانه صیاد رد شد";
                        suppliesRequest.SayadStatusDescription = state.StatusDescription;
                        suppliesRequest.SayadStatusCode = state.StatusCode;
                        _suppliesRequestRepository.Update(suppliesRequest);
                    }
                }
                catch (Exception ex){
                    _logger.Error(new FaultDto {Location = "Supplies Service", Message = ex.Message});
                    return false;
                }
            }
            return true;
        }
        public async Task<CheckSayadStateResultDto> CheckSayadState(CheckSayadStateDtq dtq){
            try{
                var state =
                    (await _sayadFacade.Check("AB" + dtq.ItemId))
                        .GetChequeBookStatusByInquiryCodeResult;
                return new CheckSayadStateResultDto
                {
                    ReasonCode = state.StatusReasonCode,
                    StatusDescription = state.StatusDescription,
                    StatusCode = state.StatusCode,
                    ErrorCode = state.ErrorCode.ToString(),
                    ErrorDescription = state.StatusDescription
                };
            }
            catch (Exception ex){
                _logger.Error(new FaultDto {Location = "Supplies Service", Message = ex.Message});
                return new CheckSayadStateResultDto
                {
                    StatusDescription = ex.Message
                };
            }
        }
        public async Task<bool> RetryInquiry(){
            var request = _suppliesRequestRepository.GetRequestByState(5).ToList();
            var errorDb = _suppliesRequestRepository.GetRequestByState(9).ToList();
            request.AddRange(errorDb);
            foreach (var suppliesRequest in request){
                try {
                    await Inquiry(new InquiryRequestDtc {ItemId = suppliesRequest.Id.Value});
                }
                catch (Exception ex){
                    _logger.Error(new FaultDto {Location = "Supplies Service", Message = ex.Message});
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> Accept(AcceptSayadDtc acceptSayadDtc){
            try{
                var item = _suppliesRequestRepository.GetItemById(acceptSayadDtc.ItemId);
                var result = await _sayadFacade.Accept("AB" + acceptSayadDtc.ItemId, item.SayadReasonCode);
                //if (result.UpdateAcceptInquiryResult.Code != 400) return false;
                item.SayadStatusCode = result.UpdateAcceptInquiryResult.Code.ToString();
                item.SayadStatusDescription = result.UpdateAcceptInquiryResult.Description;
                item.StateDescription = "ارسال مجدد درخواست به سامانه صیاد ، منتظر نتیجه از صیاد";
                item.State = 7;
                _suppliesRequestRepository.Update(item);
                return true;
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("Supplies Service", ex.Message, ex.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
        public async Task<bool> Reject(RejectSayadDtc rejectSayadDtc){
            try{
                var item = _suppliesRequestRepository.GetItemById(rejectSayadDtc.ItemId);
                //! bayad farayan dara hale entezar bashad ke betavanad in tabe ra seda bezanad
                //todo :
                //? bayad be validation dto montaghel shavad
                //if (item.State != 6) return false;
                var result = await _sayadFacade.Reject("AB" + rejectSayadDtc.ItemId);
                if (result.UpdateRejectInquiryResult.Code != 400) return false;
                item.SayadStatusCode = result.UpdateRejectInquiryResult.Code.ToString();
                item.SayadStatusDescription = result.UpdateRejectInquiryResult.Description;
                item.StateDescription = "رد درخواست با موفقیت انجام شد";
                item.State = -1;
                _suppliesRequestRepository.Update(item);
                return true;
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("Supplies Service", ex.Message, ex.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
        public async Task<bool> DeactivateBaseIbanRequest(DeactivateBaseIBANDtc deactivateBaseIbanDtc){
            try{
                var item = _baseIbanRequestRepository.GetItemById(deactivateBaseIbanDtc.ItemId);
                var sheba = _customerAccountService.GetSheba(deactivateBaseIbanDtc.Account);
                var result = await _sayadFacade.DeActivateBaseAccount(sheba);
                if (result.DeactivateBaseIBANResult.Code != 400) return false;
                item.SayadStatusCode = result.DeactivateBaseIBANResult.Code.ToString();
                item.SayadStatusDescription = result.DeactivateBaseIBANResult.Description;
                _baseIbanRequestRepository.Update(item);
                return true;
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("Supplies Service", ex.Message, ex.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
        public async Task<bool> IranNaraChequeRequest(IranNaraChequeRequestDtc iranNaraChequeRequestDtc){
            try{
                var request = _suppliesRequestRepository.GetItemById(iranNaraChequeRequestDtc.ItemId);
                var iranNaraRequest = new IranNaraChequeRequest
                {
                    CustomerRequestNumber = "AB" + request.Id,
                    BranchCode =
                        request.ApplicantBranchCode.StartsWith("0")
                            ? request.ApplicantBranchCode.Remove(0, 1)
                            : request.ApplicantBranchCode, // شعبه افتتاح حساب
                    BranchRequestNumber = 113960230 + request.Id.Value, //شماره درخواست
                    CustomerName = request.CustomerName,
                    Iban = request.Sheba,
                    CheckbookCode = 3 + request.SheetCount.ToString(), //تعداد برگ دسته چک
                    ChequeCnt = 1, // تعداد دسته چک
                    CustomerAccountNumber = request.AccountNumber,
                    MelliCode = request.NationalId, //کد یوز
                    User = "ArcUser",
                    SendingDate = _dateTimeConvertor.GetPersianDateWithOutSlash(DateTime.Now),
                    SendingBranchCode =
                        request.ApplicantBranchCode.StartsWith("0")
                            ? request.ApplicantBranchCode.Remove(0, 1)
                            : request.ApplicantBranchCode // شعبه درخواست کننده
                };
                var query =
                    "INSERT INTO[dbo].[tbl_ABChequeRequest]([CustomerRequestNumber],[BranchRequestNumber],[SendingDate],[BranchCode]," +
                    "[CustomerAccountNumber],[CustomerName],[CheckbookCode],[SendingBranchCode],[Iban],[User],[ChequeCnt],[MelliCode]," +
                    "[IsPrint]) VALUES";
                query +=
                    $" ('{iranNaraRequest.CustomerRequestNumber}','{iranNaraRequest.BranchRequestNumber}','{iranNaraRequest.SendingDate}','{iranNaraRequest.BranchCode}'," +
                    $"'{iranNaraRequest.CustomerAccountNumber}','{iranNaraRequest.CustomerName}','{iranNaraRequest.CheckbookCode}','{iranNaraRequest.SendingBranchCode}'," +
                    $"'{iranNaraRequest.Iban}','{iranNaraRequest.User}',{iranNaraRequest.ChequeCnt},'{iranNaraRequest.MelliCode}','{iranNaraRequest.IsPrint}')";
                var seials = request.SerialNumbers.Split(',');
                var taks = seials.Select(x => AddRequestSerialId(new RequestSerialId
                {
                    CustomerRequestNumber = iranNaraRequest.CustomerRequestNumber,
                    SerialId = x,
                    IranNaraChequeRequestId = iranNaraRequest.Id
                }));
                //bana be darkhast sherkat iran nara ebteda detail bad master vard mishavad
                await Task.WhenAll(taks);
                // be dalil inke trigger ro table hast majboor boodim injroori save konim , ef nemitoonest insert kone
                await _chequeRequestRepository.NonQExecuteueryAsync(query);
                var newIranNaraRequest =
                    await
                        _chequeRequestRepository.QueryAsync(
                            async f =>
                                await
                                    f.FirstOrDefaultAsync(
                                        x => x.CustomerRequestNumber == iranNaraRequest.CustomerRequestNumber));
                var requestSerialIds =
                    await _requestSerial.QueryAsync(async
                        f =>
                        await
                            f.Where(x => x.CustomerRequestNumber == iranNaraRequest.CustomerRequestNumber).ToListAsync());
                await Task.WhenAll(requestSerialIds.Select(x => UpdateRequestSerialId(x, newIranNaraRequest.Id)));
                request.State = 1200;
                request.Approver = iranNaraChequeRequestDtc.ApproverName;
                request.ApprovedDate = _dateTimeConvertor.GetPersianDate(DateTime.Now);
                request.StateDescription = "درخواست به واحد چاپ دسته چک ارسال شد";
                _suppliesRequestRepository.Update(request);
                return true;
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("Supplies Service", ex.Message, ex.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
        public async Task<bool> CheckIranNaraState(){
            var request = _suppliesRequestRepository.GetRequestByState(1200);
            var requestArray = request.Select(x => "AB" + x.Id).ToArray();

            //var isPrintes =await 
            //    _chequeRequestRepository.QueryAsync(
            //        async f => await f.Where(x => x.IsPrint && requestArray.Contains(x.CustomerRequestNumber)).ToListAsync());
            var isPrintes = await _requestSerial.QueryAsync(
                async f =>
                    await
                        f.Include("IranNaraChequeRequest")
                            .Where(
                                x => x.IranNaraChequeRequest.IsPrint && requestArray.Contains(x.CustomerRequestNumber))
                            .ToListAsync());
            var iranNaraChequeRequests = isPrintes.Select(x => x.IranNaraChequeRequest).Distinct();
            foreach (var isPrint in iranNaraChequeRequests){
                var item =
                    request.FirstOrDefault(x => x.Id == int.Parse(isPrint.CustomerRequestNumber.Replace("AB", "")));
                item.State = 1201;
                var fSerial = isPrintes.Where(f => f.IranNaraChequeRequestId == isPrint.Id).Min(x => x.Serial);
                var lSerial = isPrintes.Where(f => f.IranNaraChequeRequestId == isPrint.Id).Max(x => x.Serial);
                item.IranNaraFirstSerial = fSerial;
                item.IranNaraLastSerial = lSerial;
                item.StateDescription = "دسته چک چاپ شد";
                _suppliesRequestRepository.Update(item);
            }
            return true;
        }
        public async Task<bool> IsValidKarizSinger(IsValidCustomerInformationDtq isValidCustomerInformationDtq){
            var kriz = await GetKarizInformation(isValidCustomerInformationDtq.AccountNumber);
            return kriz.Item1.Count > 0;
        }
        /// <summary>
        ///     check kardan moshakhasat fard dar database bank
        ///     error ha yeki eyki ezafe mishe:
        ///     1- shomare shenasname moshkel dar
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<ErrorInfoDto> CheckAccountInformation(string accountNumber){
            var isError = false;
            var error = "";
            var accInfo = await _suppliesRepository.GetAccountOwners(accountNumber);
            var accountOwners = new List<RespondAccountOwnerDto>();
            var customerInfoDt = accInfo.Tables[0];
            var faragirList = new List<FaragirDto>();
            //sefr mamooli , 1 eshteraki
            var sharedAccount = 0;
            if (accInfo.Tables.Count > 1){
                sharedAccount = 1;
                var partInfoDt = accInfo.Tables[1];
                foreach (DataRow dataRow in partInfoDt.Rows){
                    var partType = dataRow["CUSTYPE"].ToString() == "01" ? PartyType.Actual : PartyType.Legal;
                    if (partType == PartyType.Legal){
                        if (string.IsNullOrEmpty(dataRow["COMPGNO"].ToString().Trim())){
                            isError = true;
                            error += "شماره ثبت شرکت نامعتبر است \r\n";
                        }
                        if (string.IsNullOrEmpty(dataRow["IDNUMBER-CORP"].ToString().Trim())){
                            isError = true;
                            error += "شماره ملی شرکت نامعتبر است \r\n";
                        }
                    }
                    else{
                        //! baraye motevalin 68 be bad  
                        var birthDate = int.Parse(dataRow["BIRTH_DATE"].ToString());
                        var idNum = dataRow["IDNUMBER"].ToString().Trim();

                        //Todo: baraye atbae khareji
                        string identifier;
                        if (dataRow["NATIONALITY"].ToString() == "F"){
                            identifier = dataRow["FARAGIRNO"].ToString().Trim();
                            if (string.IsNullOrEmpty(identifier) || string.IsNullOrEmpty(idNum)){
                                isError = true;
                                error += "شماره فراگیر اتباع خارجی نامعتبر است \r\n";
                            }
                        }
                        else{
                            var fullName = dataRow["FName"] + " " + dataRow["LName"];
                            identifier = dataRow["ECONOMIC-CODE"].ToString().Trim();

                            //Todo: 
                            //! baraye motevalin 68 be bad  
                            if (birthDate >= 13680101 && string.IsNullOrEmpty(idNum)) { idNum = identifier; }
                            if (string.IsNullOrEmpty(identifier)){
                                isError = true;
                                error += $"شماره ملی {fullName} نامعتبر است \r\n";
                            }
                            if (string.IsNullOrEmpty(idNum)){
                                isError = true;
                                error += $"شماره شناسنامه {fullName} نامعتبر است \r\n";
                            }
                        }
                    }
                }
            }
            else{
                var dataRow = customerInfoDt.Rows[0];
                var partType = dataRow["CUSTYPE"].ToString() == "01" ? PartyType.Actual : PartyType.Legal;
                if (partType == PartyType.Legal){
                    if (string.IsNullOrEmpty(dataRow["COMPGNO"].ToString().Trim())){
                        isError = true;
                        error += "شماره ثبت شرکت نامعتبر است \r\n";
                    }
                    if (string.IsNullOrEmpty(dataRow["IDNUMBER-CORP"].ToString().Trim())){
                        isError = true;
                        error += "شماره ملی شرکت نامعتبر است \r\n";
                    }
                }
                else{
                    //! baraye motevalin 68 be bad  
                    var birthDate = int.Parse(dataRow["BIRTH_DATE"].ToString());
                    var idNum = dataRow["IDNUMBER"].ToString().Trim();

                    //Todo: baraye atbae khareji
                    string identifier;
                    if (dataRow["NATIONALITY"].ToString() == "F"){
                        identifier = dataRow["FARAGIRNO"].ToString().Trim();
                        if (string.IsNullOrEmpty(identifier) || string.IsNullOrEmpty(idNum)){
                            isError = true;
                            error += "شماره فراگیر اتباع خارجی نامعتبر است \r\n";
                        }
                    }
                    else{
                        identifier = dataRow["ECONOMIC-CODE"].ToString().Trim();

                        //Todo: 
                        //! baraye motevalin 68 be bad  
                        if (birthDate >= 13680101 && string.IsNullOrEmpty(idNum)) { idNum = identifier; }
                        if (string.IsNullOrEmpty(identifier)){
                            isError = true;
                            error += "شماره ملی نامعتبر است \r\n";
                        }
                        if (string.IsNullOrEmpty(idNum)){
                            isError = true;
                            error += "شماره شناسنامه نامعتبر است \r\n";
                        }
                    }
                }
            }
            return new ErrorInfoDto {Description = error, IsError = isError};
        }

        public async Task<ErrorInfoDto> CheckAccountDuplicate(string accountNumber)
        {
            var error = "";
            var query = @"<View><Query>
   <Where>
      <And>
         <And>
            <Gt>
               <FieldRef Name='State' />
               <Value Type='Number'>-1</Value>
            </Gt>
            <Lt>
               <FieldRef Name='State' />
               <Value Type='Number'>1300</Value>
            </Lt>
         </And>
         <Eq>
            <FieldRef Name='AccountNumber' />
            <Value Type='Text'>" + accountNumber + @"</Value>
         </Eq>
      </And>
   </Where>
</Query></View>";
            var items= _suppliesRequestRepository.GetItems(query);
            if (!items.Any()) return new ErrorInfoDto {Description = error, IsError = false};
            error += "برای این شماره حساب قبلا درخواست ثبت شده است \r\n";
            return new ErrorInfoDto { Description = error, IsError = true };

        }
        public async Task<bool> DoNothing(IsValidCustomerInformationDtq isValidKarizSingerDtq){
            return true;
        }

        public async Task<bool> RejectByAdmin(RejectSayadDtc rejectSayadDtc){
            try{
                var item = _suppliesRequestRepository.GetItemById(rejectSayadDtc.ItemId);
                //! bayad farayan dara hale entezar bashad ke betavanad in tabe ra seda bezanad
                var result = await _sayadFacade.Reject("AB" + rejectSayadDtc.ItemId);
                if (result.UpdateRejectInquiryResult.Code != 400) return false;
                item.SayadStatusCode = result.UpdateRejectInquiryResult.Code.ToString();
                item.SayadStatusDescription = result.UpdateRejectInquiryResult.Description;
                item.StateDescription = "پایان فرآیند بدلیل بروز اشکال";
                item.State = -1;
                _suppliesRequestRepository.Update(item);
                _logger.Warn(new FaultDto("Supplies Service", $"پایان فرآیند توسط  {rejectSayadDtc.UserName}",
                    FaultSource.Web));
                return true;
            }
            catch (Exception ex){
                _logger.Error(new FaultDto("Supplies Service", ex.Message, ex.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
        private async Task<Tuple<List<string>, KarizResponseDto>> GetKarizInformation(string accountNumber){
            var accInfoKariz = await _karizFacade.GetInfomationFromChannel(accountNumber);
            var persianCurrentDate =
                int.Parse((await _dateTimeConvertor.GetPersianDateAsync(DateTime.Now)).Replace("/", ""));
            var custNumbers = new List<string>();
            foreach (var condition in accInfoKariz.Conditions){
                if (persianCurrentDate > int.Parse(condition.ConditionExpireDate)) continue;
                foreach (var sign in condition.Signers) {
                    if (!custNumbers.Contains(sign.Cif)) custNumbers.Add(sign.Cif);
                }
            }
            return Tuple.Create(custNumbers, accInfoKariz);
        }
        private async Task AddRequestSerialId(RequestSerialId requestSerialId){
            await _requestSerial.SaveAsync(requestSerialId);
        }
        private async Task UpdateRequestSerialId(RequestSerialId requestSerialId, long id){
            requestSerialId.SetChequeRequestId(id);
            await _requestSerial.SaveAsync(requestSerialId);
        }
        private BranchCodeSystem  GetBranchCodeSystem(string branchCode){
            var query = @"<View><Query>
   <Where>
      <Eq>
         <FieldRef Name='BranchCode' />
         <Value Type='Text'>" + branchCode + @"</Value>
      </Eq>
   </Where>
</Query></View>";
            return _branchCodeSystemRepository.GetItem(query);
        }
    }
}