using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.ClearingLog{
    public class EditRequestClearingLogDtoValidator : AbstractValidator<EditRequestClearingLogDto>{
        public EditRequestClearingLogDtoValidator(){
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.LegislationDate).NotEmpty().WithMessage("تاریخ مصوبه نباید خالی باشد");
        }
    }
}