using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordEkgUseCase
{
    private readonly IMedicalCheckupRepository _repository;

    public RecordEkgUseCase(IMedicalCheckupRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RecordEkgCommand command)
    {
        var checkup = await _repository.GetByIdAsync(command.CheckupId)
            ?? throw new KeyNotFoundException("Checkup not found");

        checkup.RecordEkg(
            command.Rhythm,
            command.HeartRate,
            command.Axis,
            command.Impression,
            command.DoctorName,
            command.ExamDate
        );

        await _repository.UpdateAsync(checkup);
    }
}