using HospitalCheckupSystem.Domain.Enums;

namespace HospitalCheckupSystem.Domain.Entities;

public class MedicalConclusion
{
    public FitnessStatus FitnessStatus { get; private set; }
    public string Diagnosis { get; private set; }
    public string Recommendation { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MedicalConclusion() { }

    public MedicalConclusion(FitnessStatus fitnessStatus, string diagnosis, string recommendation)
    {
        FitnessStatus = fitnessStatus;
        Diagnosis = diagnosis;
        Recommendation = recommendation;
        CreatedAt = DateTime.UtcNow;
    }
}
