using FluentValidation;
using RahyabServices.Business.Dtos.BankPerson;
namespace RahyabServices.Business.Services.Validators.BankPerson{
    public class GetUserInfoByPersonnelNoDtqValidator : AbstractValidator<GetUserInfoByPersonnelNoDtq>{
        public GetUserInfoByPersonnelNoDtqValidator(){
            RuleFor(x => x.PersonnalNumber).Empty();
            RuleFor(x => x.PersonnalNumber).Matches(@"^\d$").WithMessage("شماره پرسنلی نامعتبر است");

        }
    }
}