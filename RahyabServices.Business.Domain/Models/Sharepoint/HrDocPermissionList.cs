using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{EF6F8D71-D6A0-4A24-B9E1-78AAB2E85197}")]
    public class HrDocPermission : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public double State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public string PersonalNo { get; set; }

        [SharepointFieldName("Users")]
        public MultiLookupFieldMapper Users { get; set; }
    }
}