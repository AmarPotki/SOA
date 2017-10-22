using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log{
    public class AddImpunityForCrimesLogDtoValidator : AbstractValidator<AddImpunityForCrimesLogDto>{
        public AddImpunityForCrimesLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository){
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.InterestRate).GreaterThan(0).WithMessage("نرخ نباید خالی باشد");
            RuleFor(x => x.DocumentUrl).NotEmpty().WithMessage("آدرس فایل نباید خالی باشد");
        }
    }
}