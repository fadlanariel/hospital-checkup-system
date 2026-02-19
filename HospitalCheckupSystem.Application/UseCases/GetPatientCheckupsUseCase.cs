using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Application.DTOs;

namespace HospitalCheckupSystem.Application.UseCases;

public class GetPatientCheckupsUseCase
{
    private readonly IMedicalCheckupRepository _repo;

    public GetPatientCheckupsUseCase(IMedicalCheckupRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MedicalCheckupSummaryDto>> ExecuteAsync(Guid patientId)
    {
        var checkups = await _repo.GetByPatientIdAsync(patientId);

        return checkups
            .OrderByDescending(c => c.CheckupDate)
            .Select(c => new MedicalCheckupSummaryDto
            {
                Id = c.Id,
                McuNumber = c.McuNumber,
                CheckupDate = c.CheckupDate,
                Status = c.Status,
                IsFinished = c.IsFinished
            })
            .ToList();
    }
}
