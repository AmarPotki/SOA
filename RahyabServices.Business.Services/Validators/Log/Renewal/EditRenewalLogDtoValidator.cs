using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Renewal{
    public class EditRenewalLogDtoValidator : AbstractValidator<EditRenewalLogDto>{
        public EditRenewalLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository, IRenewalService renewalService)
        {
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            RuleFor(x => x.FacilityNumber).NotNull().WithMessage("شماره تسهیلات نباید خالی باشد");
            RuleFor(x => x)
             .MustAsync(async (o, x) => await renewalService.CheckPrivilegeEditRenewalLogAsync(o))
             .WithMessage("مقادیر وارد شده در سطح اختیارات شما برای ویرایش نمی باشد.");
        }
    }
}