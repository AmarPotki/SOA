using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport{
    [SharepointListId("{a1913dd7-c51e-4927-8fd8-f6fd0302eec0}")]
    public class DailyliquidityReport : IEntitySharepointMapper{
        public string Title { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int? Id { get; set; }
    }
}