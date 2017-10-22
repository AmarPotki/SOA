using FluentValidation;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Validators.Supplies{
    public class RejectSayadDtcValidator : AbstractValidator<RejectSayadDtc>
    {
        public RejectSayadDtcValidator(ISuppliesRequestRepository suppliesRequest)
        {
            RuleFor(x => x.ItemId).NotEmpty();
            RuleFor(x => x.Key).NotEmpty();
            RuleFor(x => x.ItemId).Must((o, itemId, cancel) => suppliesRequest.IsValid(itemId,"6"));
        }
    }
}