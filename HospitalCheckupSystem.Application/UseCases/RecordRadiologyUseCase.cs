using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class RecordRadiologyUseCase
{
    private readonly IMedicalCheckupRepository _repository;

    public RecordRadiologyUseCase(IMedicalCheckupRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RecordRadiologyCommand command)
    {
        var checkup = await _repository.GetByIdAsync(command.CheckupId)
            ?? throw new KeyNotFoundException("Checkup not found");

        checkup.RecordRadiology(
            command.Examination,
            command.Findings,
            command.Impression,
            command.RadiologistName,
            command.ExamDate
        );

        await _repository.UpdateAsync(checkup);
    }
}