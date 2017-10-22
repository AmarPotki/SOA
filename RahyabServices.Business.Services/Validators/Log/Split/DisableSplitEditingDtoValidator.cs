using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class DisableSplitEditingDtoValidator : AbstractValidator<DisableSplitEditingDto>{
        public DisableSplitEditingDtoValidator(ILogBaseRepository logBaseRepository)
        {
            RuleFor(x => x.CustomerDelinquentId)
             .MustAsync(async (o, customerDelinquentId, cancel) => await logBaseRepository.IsExistRequestSplitLog(customerDelinquentId))
             .WithMessage("درخواست نامعتبر است");
        }
    }
}