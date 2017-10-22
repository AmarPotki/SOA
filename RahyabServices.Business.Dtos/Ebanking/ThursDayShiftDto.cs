using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Ebanking{
   public class ThursDayShiftDto : IDto{
       public string PersonnelCode { get; set; }
       public int Id { get; set; }
       public string Unit { get; set; }
       public string Day { get; set; }
       public string FullName { get; set; }
       public bool IsExist { get; set; }
    }
}