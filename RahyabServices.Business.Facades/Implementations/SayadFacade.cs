using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Facades.SayadWithSSL;
namespace RahyabServices.Business.Facades.Implementations{
    public class SayadFacade : ISayadFacade{
        private readonly AuthHeader _auth;
        private readonly SayadServicesSoapClient _services;
        public SayadFacade(){
            _services = new SayadServicesSoapClient();
            _auth = new AuthHeader {UserName = "", Password = ""};
            _services.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root,
                X509FindType.FindByIssuerName, "");
            ((BasicHttpBinding) _services.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding) _services.Endpoint.Binding).Security.Transport.ClientCredentialType =
                HttpClientCredentialType.Certificate;
        }
        public RequestResponse CallInsertChequeBookRequest(AccountInformationDto accountInformationDto){
            var accountOwnersDtOs = new List<AccountOwnersDTO>();
            foreach (var owner in accountInformationDto.AccountOwners){
                var acc = new AccountOwnersDTO();
                if (owner.IdentifierType == IdentifierType.NationalId){
                    acc.FirstName = owner.FirstName;
                    acc.LastName = owner.LastName;
                    acc.PartyType = (int) owner.PartyType;
                    acc.Identifier = owner.Identifier;
                    acc.Name = owner.Name;
                    acc.BirthDate = owner.BirthDate;
                    acc.IdentifierType = (IdentifierTypeList) (int) owner.IdentifierType;
                    acc.IdNum = owner.IdNum.Trim();
                    acc.CityCode = owner.CityCode;
                }
                else if (owner.IdentifierType == IdentifierType.ForeignId){
                    acc.FirstName = owner.FirstName;
                    acc.LastName = owner.LastName;
                    acc.Identifier = owner.Identifier;
                    acc.BirthDate = owner.BirthDate;
                    acc.IdNum = owner.IdNum.Trim();
                    acc.IdentifierType = (IdentifierTypeList) (int) owner.IdentifierType;
                }
                else{
                    acc.FirstName = owner.FirstName;
                    acc.LastName = owner.LastName;
                    acc.PartyType = (int) owner.PartyType;
                    acc.Identifier = owner.Identifier;
                    acc.BirthDate = owner.BirthDate;
                    acc.IdNum = owner.IdNum.Trim();
                    acc.IdentifierType = (IdentifierTypeList) (int) owner.IdentifierType;
                }
                accountOwnersDtOs.Add(acc);
            }
            var signatureOwnersDtOs = new List<SignatureOwnersDTO>();
            foreach (var owner in accountInformationDto.AccountSignerDtos){
                var sig = new SignatureOwnersDTO();
                if (owner.IdentifierType == IdentifierType.NationalId){
                    sig.Name = owner.CustomerName;
                    sig.CityCode = owner.CityCode;
                    sig.PartyType = 1;
                    sig.Identifier = owner.Identifier;
                    sig.BirthDate = owner.BirthDate;
                    sig.IdNum = owner.IdNum.Trim();
                    sig.IdentifierType = (IdentifierTypeList) (int) owner.IdentifierType;
                } //baraye ashkhas haghighi che irani bashe che khareji bashe 
                else{
                    sig.FirstName = owner.CustomerName;
                    sig.LastName = owner.CustomerSurname;
                    sig.PartyType = 0;
                    sig.Identifier = owner.Identifier;
                    sig.BirthDate = owner.BirthDate;
                    sig.IdentifierType = (IdentifierTypeList) (int) owner.IdentifierType;
                    sig.IdNum = owner.IdNum.Trim();
                }
                signatureOwnersDtOs.Add(sig);
            }
            var cbr = new ChequeBookRequestDTO
            {
                BankCode = "62",
                BranchCode = accountInformationDto.ApplicantBranchCode.Trim(),
                BranchName = accountInformationDto.ApplicantBranchName,
                IBAN = accountInformationDto.Sheba,
                OwnerBranchCode = accountInformationDto.ApplicantBranchCode.Trim(),
                OwnerBranchName = accountInformationDto.ApplicantBranchName,
                PostCode = "0",
                RequestCode = accountInformationDto.ItemId,
                SheetCount = accountInformationDto.SheetCount,
                SharedAccount = (SharedAccountTypes) accountInformationDto.SharedAccount,
                PrinteryStatus = 0, //0 baraye zamanist ke khode bank cheque ra shakhsi sazi va chap mikonad
                AccountOwnersDTO = accountOwnersDtOs.ToArray(),
                SignatureOwnersDTO = signatureOwnersDtOs.ToArray()
            };
            return _services.InsertChequeBookRequest(_auth, cbr);
        }
        public async Task<GetChequeBookStatusByInquiryCodeResponse> Check(string code){
            return await _services.GetChequeBookStatusByInquiryCodeAsync(_auth, code);
        }
        public async Task<UpdateAcceptInquiryResponse> Accept(string code, string inquiryTypeCode){
            return await _services.UpdateAcceptInquiryAsync(_auth, code, inquiryTypeCode);
        }
        public async Task<UpdateRejectInquiryResponse> Reject(string code){
            return await _services.UpdateRejectInquiryAsync(_auth, code);
        }
        public async Task<DeactivateBaseIBANResponse> DeActivateBaseAccount(string sheba){
            return await _services.DeactivateBaseIBANAsync(_auth, sheba);
        }
    }
}