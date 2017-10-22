using System.Collections.Generic;
using System.Security.AccessControl;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class AccountInformationDto : IDto {
        public string ItemId { get; set; }
        public int SharedAccount { get; set; }
        public string ApplicantBranchName { get; set; }
        
        public string ApplicantBranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string Sheba { get; set; }
        public int SheetCount { get; set; }
        public KarizResultDto Result { get; set; }
        public string CicsTransId { get; set; }
        public string SeqNo { get; set; }
        public string DataType { get; set; }
        public string Acknowledge { get; set; }
        public string Msgnbr { get; set; }
        public List<AccountConditionDto> Conditions { get; set; }
        public List<RespondAccountOwnerDto> AccountOwners { get; set; }
        public  List<AccountSignerDto> AccountSignerDtos { get; set; } 
        public string UserName { get; set; }
    }
}