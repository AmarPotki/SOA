using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Contract{
    public class GetCustomerDelinquentHistoryByBranchCodeDtoValidator :
        AbstractValidator<GetCustomerDelinquentHistoryByBranchCodeDto>{
        public GetCustomerDelinquentHistoryByBranchCodeDtoValidator(){
            RuleFor(x => x.PersianDate).Length(8).WithMessage("ساختار تاریخ باید بدون اسلش باشد");
            RuleFor(x => x.BranchCode).NotEmpty().WithMessage("شعبه باید پرشود");
        }
    }
}