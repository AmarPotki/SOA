using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{BCA4B037-11E6-4AC7-9E82-8FC143D667A9}")]
    public class ImpunityLogList : IEntitySharepointMapper
    {
        public int? Id { get; set; }
        public string DocumentUrl { get; set; }
        public double InterestRate { get; set; }
        public string AuthorUserName { get; set; }
        public int CustomerDelinquentId { get; set; }
        public int RequestStateHandlerId { get; set; }
        public double ImpunityAmount { get; set; }
    }
}