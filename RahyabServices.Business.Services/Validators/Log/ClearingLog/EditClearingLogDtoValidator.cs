using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Services.Implementations.Delinquent;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.ClearingLog{
    public class EditClearingLogDtoValidator : AbstractValidator<EditClearingLogDto>{
        public EditClearingLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository, IClearingService clearingService)
        {
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
           
            RuleFor(x => x)
              .MustAsync(async (o, x) => await clearingService.CheckPrivilegeEditClearingLogAsync(o))
              .WithMessage("مقادیر وارد شده در سطح اختیارات شما برای ویرایش نمی باشد.");
        }
    }
}