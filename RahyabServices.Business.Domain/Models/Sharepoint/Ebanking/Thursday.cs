using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.Sharepoint.Ebanking{
    [SharepointListId("{35581e44-9c79-4301-8f69-85efcc50f35a}")]
    public class Thursday : IEntitySharepointMapper{
        public int? Id { get; set; }
        public string Title { get; set; }
        [SharepointFieldName("Year")]
        public LookupFieldMapper Year { get; set; }
        [SharepointFieldName("Month")]
        public LookupFieldMapper Month { get; set; }
    }
}