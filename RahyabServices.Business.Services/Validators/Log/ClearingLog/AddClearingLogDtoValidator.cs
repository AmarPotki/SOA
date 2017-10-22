using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.ClearingLog{
    public class AddClearingLogDtoValidator : AbstractValidator<AddClearingLogDto>{
        public AddClearingLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository,ILogService logService){
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
           
            RuleFor(x => x.DocumentUrl).NotEmpty().WithMessage("آدرس فایل نباید خالی باشد");
            RuleFor(x => x.CustomerDelinquentId)
             .MustAsync(async (o, customerDelinquentId, cancel) => await logService.CheckHasARequestNotRespond(customerDelinquentId))
             .WithMessage("شناسه نامعتبر است");
            //RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            //RuleFor(x => x.ExpireDate).NotNull().WithMessage("تاریخ انقضا نباید خالی باشد");
        }
    }
}