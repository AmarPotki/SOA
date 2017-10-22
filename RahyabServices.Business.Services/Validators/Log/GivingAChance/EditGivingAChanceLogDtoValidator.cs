using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log{
    public class EditGivingAChanceLogDtoValidator : AbstractValidator<EditGivingAChanceLogDto>{
        public EditGivingAChanceLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository, IGivingAChanceService givingAChanceService)
        {
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("تعداد نباید خالی باشد");
            RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            RuleFor(x => x.ExpireDate).NotNull().WithMessage("تاریخ انقضا نباید خالی باشد");
            RuleFor(x => x)
             .MustAsync(async (o, x) => await givingAChanceService.CheckPrivilegeEditGivingAChanceLogAsync(o))
             .WithMessage("مقادیر وارد شده در سطح اختیارات شما برای ویرایش نمی باشد.");
        }
    }
}