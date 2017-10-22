using RahyabServices.Business.Dtos.Delinquent.Contracts;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Contract{
    public class GetCustomerDelinquentHistoryDtoValidator : AbstractValidator<GetCustomerDelinquentHistoryDto>{
        public GetCustomerDelinquentHistoryDtoValidator(){
            RuleFor(x => x.PersianDate).Length(8).WithMessage("ساختار تاریخ باید بدون اسلش باشد");
        }
    }
}