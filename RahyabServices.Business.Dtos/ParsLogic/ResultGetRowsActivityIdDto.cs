using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.ParsLogic
{
  public  class ResultGetRowsActivityIdDto : IDto{
      public bool IsSuccess { get; set; }
      public string RequestType { get; set; }
      public string Status { get; set; }
      public string Branch { get; set; }
      public string Description { get; set; }
      public string ActionerFN { get; set; }
      public string ActionerLN { get; set; }
  }
}
