using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class EditRequestSplitLogDtoValidator : AbstractValidator<EditRequestSplitLogDto>{
        public EditRequestSplitLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository, ISplitService splitService)
        {
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.InterestRate).GreaterThan(0).WithMessage("نرخ نباید خالی باشد");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("تعداد نباید خالی باشد");
            RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            RuleFor(x => x.StartDate).NotNull().WithMessage("تاریخ شروع نباید خالی باشد");
        }
    }
}