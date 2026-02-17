namespace HospitalCheckupSystem.Domain.Entities;

public class MedicalConclusion
{
    public bool Fit { get; private set; }
    public string Diagnosis { get; private set; }
    public string Recommendation { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MedicalConclusion() { }

    public MedicalConclusion(bool fit, string diagnosis, string recommendation)
    {
        Fit = fit;
        Diagnosis = diagnosis;
        Recommendation = recommendation;
        CreatedAt = DateTime.UtcNow;
    }
}
