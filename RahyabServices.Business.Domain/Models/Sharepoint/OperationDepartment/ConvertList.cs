using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.OperationDepartment{
    [SharepointListId("{29bde3c9-a1f4-4e54-9c20-956064e761f1}")]
    public class ConvertList : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string Convert { get; set; }
    }
}