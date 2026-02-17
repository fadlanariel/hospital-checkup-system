using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordAnamnesisUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public RecordAnamnesisUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(RecordAnamnesisCommand cmd)
    {
        var checkup = await _repo.GetByIdAsync(cmd.CheckupId)
            ?? throw new Exception("Checkup not found");

        checkup.RecordAnamnesis(
            cmd.Complaints,
            cmd.PastIllness,
            cmd.FamilyHistory,
            cmd.Allergies,
            cmd.Smoking,
            cmd.Alcohol,
            cmd.WorkHazards);

        await _repo.UpdateAsync(checkup);
    }
}
