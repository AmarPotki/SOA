using FluentValidation;
using RahyabServices.Business.Dtos.Cando.Adanic;
namespace RahyabServices.Business.Services.Validators.Cando{
    public class CallServiceDtqValidator : AbstractValidator<CallServiceDtq>{
        public CallServiceDtqValidator(){
            RuleFor(x => x.Variables).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}