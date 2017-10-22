using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.OperationDepartment{
    [SharepointListId("{499b0e72-f392-49a5-a9f5-7a6584d98ffb}")]
    public class QueryList : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string Title { get; set; }
    }
}