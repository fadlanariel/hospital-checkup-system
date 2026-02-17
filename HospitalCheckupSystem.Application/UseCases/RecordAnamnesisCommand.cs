namespace HospitalCheckupSystem.Application.UseCases;

public class RecordAnamnesisCommand
{
    public Guid CheckupId { get; set; }
    public string Complaints { get; set; } = "";
    public string PastIllness { get; set; } = "";
    public string FamilyHistory { get; set; } = "";
    public string Allergies { get; set; } = "";
    public bool Smoking { get; set; }
    public bool Alcohol { get; set; }
    public string WorkHazards { get; set; } = "";
}
