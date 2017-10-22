using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.AbsorbResourses{
    [SharepointListId("{cb790016-ac69-4f2c-8477-f217e2947647}")]
    public class Request:IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string CustomerNo { get; set; }
        public string FullName { get; set; }
    }
}