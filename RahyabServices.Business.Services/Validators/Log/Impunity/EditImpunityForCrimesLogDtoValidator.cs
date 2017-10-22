using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Impunity
{
    public class EditImpunityForCrimesLogDtoValidator:AbstractValidator<EditImpunityForCrimesLogDto> {
        public EditImpunityForCrimesLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository, IImpunityService impunityService)
        {
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");       
            RuleFor(x => x.InterestRate).GreaterThan(0).WithMessage("نرخ نباید خالی باشد");            
            RuleFor(x => x)
             .MustAsync(async (o, x) => await impunityService.CheckPrivilegeEditImpunityLogAsync(o))
             .WithMessage("مقادیر وارد شده در سطح اختیارات شما برای ویرایش نمی باشد.");
        }
    }
}
