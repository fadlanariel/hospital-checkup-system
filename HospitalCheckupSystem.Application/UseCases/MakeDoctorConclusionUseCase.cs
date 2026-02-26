using FluentValidation;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class MakeDoctorConclusionUseCase
{
    private readonly IMedicalCheckupRepository _repo;
    private readonly IValidator<MakeDoctorConclusionCommand> _validator;

    public MakeDoctorConclusionUseCase(
        IMedicalCheckupRepository repo,
        IValidator<MakeDoctorConclusionCommand> validator)
    {
        _repo = repo;
        _validator = validator;
    }

    public async Task ExecuteAsync(MakeDoctorConclusionCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var checkup = await _repo.GetByIdAsync(command.CheckupId);

        if (checkup == null)
            throw new InvalidOperationException("Checkup not found");

        checkup.MakeConclusion(
            command.FitnessStatus,
            command.Diagnosis,
            command.Recommendation
        );

        await _repo.UpdateAsync(checkup);
    }
}
