using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.OperationDepartment{
    [SharepointListId("{cf6edd10-b5dc-4cd6-97f3-e0e6429a3fe9}")]
    public class AtmOutputLib : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }

    }
}