namespace HospitalCheckupSystem.Application.DTOs;

public class RecordAnamnesisRequest
{
    public string Complaints { get; set; } = default!;
    public string PastIllness { get; set; } = default!;
    public string FamilyHistory { get; set; } = default!;
    public string Allergies { get; set; } = default!;
    public bool Smoking { get; set; }
    public bool Alcohol { get; set; }
    public string WorkHazards { get; set; } = default!;
}

