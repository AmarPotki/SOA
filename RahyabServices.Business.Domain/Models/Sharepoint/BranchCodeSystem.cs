using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.Sharepoint
{
    //لیست کدهای سیستم شعبه های بانک در سامانه صیاد
    [SharepointListId("422A8D9C-50D2-4DF3-AAE8-8BD12D1686A2")]
  public class BranchCodeSystem : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        //نام شعبه
        public string Title { get; set; }
        //کد سامانه
        public string SystemCode { get; set; }
        //کد شعبه
        public string BranchCode { get; set; }


    }
}
