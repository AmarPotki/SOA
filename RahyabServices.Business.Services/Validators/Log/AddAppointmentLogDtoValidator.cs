using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Appointment;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log
{
    public class AddAppointmentLogDtoValidator : AbstractValidator<AddAppointmentLogDto>
    {
        public AddAppointmentLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository)
        {
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.Result).NotEmpty().WithMessage("نتیجه نباید خالی باشد");
            RuleFor(x => x.AgentFullName).NotEmpty().WithMessage("نام مامور نباید خالی باشد");
            RuleFor(x => x.DateAction).NotEmpty().WithMessage("تاریخ ملاقات حضوری نباید خالی باشد");
        }
    }
}