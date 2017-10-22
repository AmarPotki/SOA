using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{02160fab-f26b-4f79-8cbe-82bab5f4a1d0}")]
    public class ClearingLogList:IEntitySharepointMapper{
        public int? Id { get; set; }
        public string DocumentUrl { get; set; }
        public int CustomerDelinquentId { get; set; }
        public int RequestStateHandlerId { get; set; }
        public string AuthorUserName { get; set; }
    }
}