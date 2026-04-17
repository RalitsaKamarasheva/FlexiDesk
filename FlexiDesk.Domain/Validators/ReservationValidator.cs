using FlexiDesk.Domain.Entities;
using FluentValidation;

namespace FlexiDesk.Domain.Validators
{
    public class ReservationValidator:AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            // Потребителят не може да е празен
            RuleFor(r => r.UserID)
                .NotEmpty().WithMessage("Потребителското име е задължително.");

            // Ресурсът трябва да е валиден GUID
            RuleFor(r => r.ResourceId)
                .NotEmpty().WithMessage("Трябва да изберете бюро/ресурс.");

            // Началото трябва да е в бъдещето
            RuleFor(r => r.StartTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Резервацията не може да е в миналото.");

            // Краят трябва да е след началото
            RuleFor(r => r.EndTime)
                .GreaterThan(r => r.StartTime).WithMessage("Крайният час трябва да е след началния.");
        }
    }
}
