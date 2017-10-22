using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{57D37797-94A6-4B25-9409-53D8D9EAB533}")]
    public class TatApplicant : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string NationalID { get; set; }
        public string FatherTitle { get; set; }
        public double FileNo { get; set; }
        public DateTime Birthday { get; set; }

    }
}