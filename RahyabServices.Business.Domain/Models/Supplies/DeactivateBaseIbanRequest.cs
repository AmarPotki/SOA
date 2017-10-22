using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Supplies{
    [SharepointListId("537aa729-c5a4-4e08-a0f1-a7ba30ce8991")]
    public class DeactivateBaseIbanRequest : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string SayadStatusCode { get; set; }
        public string SayadReasonCode { get; set; }
        public string SayadStatusDescription { get; set; }
    }
}