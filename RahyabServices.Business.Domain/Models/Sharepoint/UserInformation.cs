using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;

namespace RahyabServices.Business.Domain.Models.Sharepoint
{
    [SharepointListName("لیست اطلاعات مربوط به کاربران")]
    public class UserInformation : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        [SharepointFieldName("Title")]
        public string DisplayName { get; set; }
        [SharepointFieldName("Name")]
        public string UserName { get; set; }
    }
}
