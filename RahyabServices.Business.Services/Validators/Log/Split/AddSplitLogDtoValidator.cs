using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class AddSplitLogDtoValidator : AbstractValidator<AddSplitLogDto>{
        public AddSplitLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository,
            ILogBaseRepository logBaseRepository){
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(
                    async (o, customerDelinquentId, cancel) =>
                        await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.InterestRate).GreaterThan(0).WithMessage("نرخ نباید خالی باشد");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("تعداد نباید خالی باشد");
            RuleFor(x => x.DocumentUrl).NotEmpty().WithMessage("آدرس فایل نباید خالی باشد");
            RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            RuleFor(x => x.StartDate).NotNull().WithMessage("تاریخ انقضا نباید خالی باشد");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(
                    async (o, customerDelinquentId, cancel) =>
                        !await logBaseRepository.IsExistRequestLogNotRespond(customerDelinquentId))
                .WithMessage("درحال حاضر برای این تسهیل فرم ثبت شده و منتظر تایید است");
        }
    }
}