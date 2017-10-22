using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using RahyabServices.Business.Dtos;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Services.Intefaces.Supplies;
namespace RahyabServices.Business.Services.Validators.Supplies{
    public class IsValidCustomerInformationDtqValidator : AbstractValidator<IsValidCustomerInformationDtq>{
        public IsValidCustomerInformationDtqValidator(ISuppliesService suppliesService){
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.AccountNumber).MustBeValidCustomerInformation(suppliesService);
           
        }
    }
    public class IsValidCustomerInformation : PropertyValidator
    {
        private readonly ISuppliesService _suppliesService;
        public IsValidCustomerInformation(ISuppliesService suppliesService) : base("{ValidationMessage}")
        {
            _suppliesService = suppliesService;
        }
        protected override bool IsValid(PropertyValidatorContext context){
            return Task.Run(() => IsValidAsync(context, new CancellationToken())).GetAwaiter().GetResult();
        }
        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation){
            var err = "";
            var dup = await IsvalidDuplicate(context);
            if (dup.IsError){
                 context.MessageFormatter.AppendArgument("ValidationMessage", dup.Description);
                return false;
            }
            if (!await IsValidSigner(context))
                err +="شرایط برداشت در بانک ایران وارد نشده است. \r\n";
            var bankInfo = await IsBankInformation(context);
            if (bankInfo.IsError) err += bankInfo.Description;
            context.MessageFormatter.AppendArgument("ValidationMessage",err);
            return string.IsNullOrEmpty(err);
        }


        private async Task<bool> IsValidSigner(PropertyValidatorContext context)
        {
            var account = context.PropertyValue as string;
            return await _suppliesService.IsValidKarizSinger(new IsValidCustomerInformationDtq
            { AccountNumber = account });
        }

        private async Task<ErrorInfoDto> IsBankInformation(PropertyValidatorContext context)
        {
            var account = context.PropertyValue as string;
            return await _suppliesService.CheckAccountInformation(account);
        }
        private async Task<ErrorInfoDto> IsvalidDuplicate(PropertyValidatorContext context){
            var account = context.PropertyValue as string;
            return await _suppliesService.CheckAccountDuplicate(account);
        } 
    }

    public static class CustomerInformationValidatExtensions
    {
        public static IRuleBuilderOptions<T, object> MustBeValidCustomerInformation<T>
            (this IRuleBuilder<T, object> ruleBuilder,ISuppliesService suppliesService)
        {
            return ruleBuilder.SetValidator(new IsValidCustomerInformation(suppliesService));
        }

    }
}