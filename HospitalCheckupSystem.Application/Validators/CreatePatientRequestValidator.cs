using FluentValidation;
using HospitalCheckupSystem.Application.DTOs;

namespace HospitalCheckupSystem.Application.Validators;

public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
{
    public CreatePatientRequestValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Dob)
            .LessThan(DateTime.Today);

        RuleFor(x => x.Gender)
            .Must(g => g == "M" || g == "F")
            .WithMessage("Gender must be M or F");

        RuleFor(x => x.Phone)
            .NotEmpty();

        RuleFor(x => x.Address)
            .NotEmpty();
    }
}
