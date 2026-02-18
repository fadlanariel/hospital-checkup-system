namespace HospitalCheckupSystem.Application.DTOs;

public class MakeConclusionRequest
{
    public bool Fit { get; set; }
    public string Diagnosis { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
}