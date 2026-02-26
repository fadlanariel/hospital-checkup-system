using FluentValidation;
using HospitalCheckupSystem.Application.UseCases;

namespace HospitalCheckupSystem.Application.Validators;

public class MakeDoctorConclusionCommandValidator
    : AbstractValidator<MakeDoctorConclusionCommand>
{
    public MakeDoctorConclusionCommandValidator()
    {
        RuleFor(x => x.CheckupId)
            .NotEmpty();

        RuleFor(x => x.Diagnosis)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Recommendation)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.FitnessStatus)
            .IsInEnum();
    }
}