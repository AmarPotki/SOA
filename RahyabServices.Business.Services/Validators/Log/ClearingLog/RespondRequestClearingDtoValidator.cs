using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.ClearingLog
{
    public class RespondRequestClearingDtoValidator : AbstractValidator<RespondRequestClearingDto>{
        public RespondRequestClearingDtoValidator(IStateRepository stateRepository){
            RuleFor(x => x.RespondUserName).NotEmpty().WithMessage("نام کاربری تایید کننده نباید خالی باشد");
            RuleFor(x => x.RequestStateHandlerId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.RequestStateHandlerId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await stateRepository.IsExist(customerDelinquentId,o.RequestStateHandlerId))
                .WithMessage("درخواست نامعتبر است");
        }
    }
}