using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport{
    [SharepointListId("{4f5760c0-5ac7-4463-94a7-343a81af93e3}")]
    public class ReportFacilityDetail : IEntitySharepointMapper
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }



    }
}