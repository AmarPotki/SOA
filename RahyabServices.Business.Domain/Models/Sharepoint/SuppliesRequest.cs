using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("ecf76273-ad1e-47f2-b1ea-8dce0e375c1b")]
    public class SuppliesRequest : IEntitySharepointMapper{
        public string ApplicantBranchName { get; set; }
        public string ApplicantBranchCode { get; set; }
        public string AccountOpennerBranch { get; set; }
       public string DeliveryBranchCode { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string Sheba { get; set; }
        public double SheetCount { get; set; }
        public double State { get; set; }
        public string SayadStatusCode { get; set; }
        public string SayadReasonCode { get; set; }
        public string StateDescription { get; set; }
        public string SayadStatusDescription { get; set; }
        public string SerialNoFrom { get; set; }
        public string SerialNoTo { get; set; }
        public string CentralBranchCode { get; set; }
        public string NationalId { get; set; }
        public string SerialNumbers { get; set; }
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string IranNaraFirstSerial { get; set; }
        public string IranNaraLastSerial { get; set; }
        public string Approver { get; set; }

        public string ApprovedDate { get; set; }
    }
}