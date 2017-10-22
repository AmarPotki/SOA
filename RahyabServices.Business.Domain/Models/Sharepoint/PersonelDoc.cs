using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{529E1EE4-A33C-43E5-9F9B-61282899B4F4}")]
    public class PersonelDoc : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string FolderName { get; set; }
    }
    
}