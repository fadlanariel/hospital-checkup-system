using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class StartMedicalCheckupUseCase
{
    private readonly IMedicalCheckupRepository _repo;
    private readonly IMcuNumberGenerator _generator;

    public StartMedicalCheckupUseCase(
        IMedicalCheckupRepository repo,
        IMcuNumberGenerator generator)
    {
        _repo = repo;
        _generator = generator;
    }

    public async Task<StartMedicalCheckupResult> ExecuteAsync(Guid patientId, DateTime date)
    {
        var year = date.Year;

        var sequence = await _repo.GetNextSequenceAsync(year);
        var number = _generator.Generate(year, sequence);

        var checkup = new MedicalCheckup(patientId, number, date);

        await _repo.AddAsync(checkup);

        return new StartMedicalCheckupResult
        {
            Id = checkup.Id,
            McuNumber = checkup.McuNumber
        };
    }
    
}
