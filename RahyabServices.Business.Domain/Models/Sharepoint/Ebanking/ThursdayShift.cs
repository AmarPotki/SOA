using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.Sharepoint.Ebanking{
    [SharepointListId("{e41e0b70-2bc4-49f0-b260-57732cf80fdc}")]
    public class ThursdayShift: IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        [SharepointFieldName("Title")]
        public string Name { get; set; }
        public string PersonnelCode { get; set; }
        public string Family { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        [SharepointFieldName("Year")]
        public LookupFieldMapper Year { get; set; }
        [SharepointFieldName("Month")]
        public LookupFieldMapper Month { get; set; }
        [SharepointFieldName("Day")]
        public MultiLookupFieldMapper Day { get; set; }
        [SharepointFieldName("Status")]
        public LookupFieldMapper Status { get; set; }

    }
}