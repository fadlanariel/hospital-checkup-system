using HospitalCheckupSystem.Domain.Enums;

namespace HospitalCheckupSystem.Application.UseCases;

public class MakeDoctorConclusionCommand
{
    public Guid CheckupId { get; set; }
    public FitnessStatus FitnessStatus { get; set; }
    public string Diagnosis { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
}
