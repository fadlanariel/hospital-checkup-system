using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class GetMedicalCheckupUseCase
{
    private readonly IMedicalCheckupRepository _repository;

    public GetMedicalCheckupUseCase(IMedicalCheckupRepository repository)
    {
        _repository = repository;
    }

    public async Task<MedicalCheckup> Execute(Guid id)
    {
        var checkup = await _repository.GetByIdAsync(id);

        if (checkup == null)
            throw new InvalidOperationException("Checkup not found");

        return checkup;
    }
}
