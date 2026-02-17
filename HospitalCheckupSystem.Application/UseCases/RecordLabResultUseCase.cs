using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordLabResultUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public RecordLabResultUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(RecordLabResultCommand cmd)
    {
        var checkup = await _repo.GetByIdAsync(cmd.CheckupId)
            ?? throw new Exception("Checkuo not found");

        checkup.AddLabResult(
            cmd.TestName,
            cmd.Unit,
            cmd.Value,
            cmd.NormalMin,
            cmd.NormalMax);

        await _repo.UpdateAsync(checkup);
    }
}
