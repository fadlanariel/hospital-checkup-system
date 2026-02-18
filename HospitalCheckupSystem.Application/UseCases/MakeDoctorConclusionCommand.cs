namespace HospitalCheckupSystem.Application.UseCases;

public class MakeDoctorConclusionCommand
{
    public Guid CheckupId { get; set; }
    public bool Fit { get; set; }
    public string Diagnosis { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
}
