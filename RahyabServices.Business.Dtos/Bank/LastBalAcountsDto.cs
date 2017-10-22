using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Bank
{
   public class LastBalAcountsDto:IDto
    {
       public decimal Amount { get; set; }
        public  bool IsCorect { get; set; }
    }
}
