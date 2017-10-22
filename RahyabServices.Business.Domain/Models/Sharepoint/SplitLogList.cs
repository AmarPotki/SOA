using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint{
    [SharepointListId("{783f716a-7693-40c0-a722-f7b6b24f4c7b}")]
    public class SplitLogList:IEntitySharepointMapper{
        public int? Id { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime LegislationDate { get; set; }
        public double InterestRate { get; set; }
        public string AuthorUserName { get; set; }
        public int Count { get; set; }
        public DateTime StartDate { get; set; }
        public int CustomerDelinquentId { get; set; }
        public int RequestStateHandlerId { get; set; }
        public string UserNameEncrypted { get; set; }
    }
}