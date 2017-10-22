using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class RespondRequestSplitDtoValidator : AbstractValidator<RespondRequestSplitDto>{
        public RespondRequestSplitDtoValidator(IStateRepository stateRepository){
            RuleFor(x => x.RespondUserName).NotEmpty().WithMessage("نام کاربری تایید کننده نباید خالی باشد");
            RuleFor(x => x.RequestStateHandlerId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.RequestStateHandlerId)
                .MustAsync(async (o, requestStateHandlerId, cancel) => await stateRepository.IsExist(o.CustomerDelinquentId, requestStateHandlerId))
                .WithMessage("درخواست نامعتبر است");
        }
    }
}