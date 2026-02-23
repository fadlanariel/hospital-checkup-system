using HospitalCheckupSystem.Domain.Enums;

namespace HospitalCheckupSystem.Application.DTOs;

public class MakeConclusionRequest
{
    public FitnessStatus FitnessStatus { get; set; }
    public string Diagnosis { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
}