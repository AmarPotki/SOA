using RahyabServices.Business.Dtos.Delinquent.Notification;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Noification{
    public class RemoveNotificationDtoValidator : AbstractValidator<RemoveNotificationDto>{
        public RemoveNotificationDtoValidator(){
            RuleFor(x => x.UserName).NotEmpty().WithMessage("نام کاربری نامعتبر است");
            RuleFor(x => x.NotificationId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
        }
    }
}