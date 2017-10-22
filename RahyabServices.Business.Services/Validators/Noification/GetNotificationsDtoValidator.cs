using RahyabServices.Business.Dtos.Delinquent.Notification;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Noification{
    public class GetNotificationsDtoValidator : AbstractValidator<GetNotificationsDto>{
        public GetNotificationsDtoValidator(ICryptographer cryptographer,IHrFacade hrFacade){
            RuleFor(d => d.UserName).NotEmpty().WithMessage("نام کاربری نامعتبر است");
            RuleFor(d => d.UserName)
                .Must( (o, customerNumber) =>  hrFacade.IsValidUserName( cryptographer.Decrypt(customerNumber)))
                .WithMessage("نام کاربری نامعتبر است");
        
        }
    }
}