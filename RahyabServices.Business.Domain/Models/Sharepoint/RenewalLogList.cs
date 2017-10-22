using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{D3A9B5B8-67D2-4044-8CCE-264B0DD46A53}")]
    public class RenewalLogList : IEntitySharepointMapper
    {
        public int? Id { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime LegislationDate { get; set; }
        public double InterestRate { get; set; }
        public string AuthorUserName { get; set; }
        public int CustomerDelinquentId { get; set; }
        public int RequestStateHandlerId { get; set; }
        public string UserNameEncrypted { get; set; }
        public string FacilityNumber { get; set; }
    }
}