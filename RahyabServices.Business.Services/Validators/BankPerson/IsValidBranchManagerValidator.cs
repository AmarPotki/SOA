using FluentValidation;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.DataAccess.Repositories.BankPerson.Interfaces;
namespace RahyabServices.Business.Services.Validators.BankPerson{
    public class IsValidBranchManagerValidator : AbstractValidator<IsValidBranchManagerDtq>{
        public IsValidBranchManagerValidator(IPersonInfoRepository infoRepository){
            RuleFor(d => d. UserName)
         .MustAsync(async (o, userName, cancel) => await infoRepository.IsValidUserName(userName))
         .WithMessage(" نام کاربری نامعتبر است");
        }
    }
}