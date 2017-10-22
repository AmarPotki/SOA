using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{E8BFF34C-3C9E-4B4A-8FCA-D135A2836EE1}")]
    public class GivingAChanceLogList : IEntitySharepointMapper
    {
        public int? Id { get; set; }
        public int Count { get; set; }
        public string DocumentUrl { get; set; }
        public int CustomerDelinquentId { get; set; }
        public int RequestStateHandlerId { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime LegislationDate { get; set; }
        public DateTime ExpireDate { get; set; }
      
        public decimal DepositAmount { get; set; }
    }
}