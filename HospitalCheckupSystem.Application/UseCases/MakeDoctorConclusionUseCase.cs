using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class MakeDoctorConclusionUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public MakeDoctorConclusionUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(MakeDoctorConclusionCommand command)
    {
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
