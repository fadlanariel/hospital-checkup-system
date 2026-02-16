using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordVitalsUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public RecordVitalsUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(RecordVitalsCommand cmd)
    {
        var checkup = await _repo.GetByIdAsync(cmd.CheckupId)
            ?? throw new Exception("Checkup not found");

        checkup.RecordVitals(
            cmd.Height,
            cmd.Weight,
            cmd.Systolic,
            cmd.Diastolic,
            cmd.Pulse);

        await _repo.UpdateAsync(checkup);
    }
}
