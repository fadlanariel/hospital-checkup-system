using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordPhysicalExamUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public RecordPhysicalExamUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(RecordPhysicalExamCommand cmd)
    {
        var checkup = await _repo.GetByIdAsync(cmd.CheckupId)
            ?? throw new Exception("Checkup not found");

        checkup.RecordPhysicalExam(
            cmd.GeneralAppearance,
            cmd.Eyes,
            cmd.ENT,
            cmd.Heart,
            cmd.Lungs,
            cmd.Abdomen,
            cmd.Neurology);

        await _repo.UpdateAsync(checkup);
    }
}
