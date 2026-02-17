namespace HospitalCheckupSystem.Domain.Entities;

public class Anamnesis
{
    public string Complaints { get; private set; }
    public string PastIllness { get; private set; }
    public string FamilyHistory { get; private set; }
    public string Allergies { get; private set; }
    public bool Smoking { get; private set; }
    public bool Alcohol { get; private set; }
    public string WorkHazards { get; private set; }

    private Anamnesis() { }

    public Anamnesis(
        string complaints, 
        string pastIllness, 
        string familyHistory, 
        string allergies, 
        bool smoking, 
        bool alcohol, 
        string workHazards)
    {
        Complaints = complaints;
        PastIllness = pastIllness;
        FamilyHistory = familyHistory;
        Allergies = allergies;
        Smoking = smoking;
        Alcohol = alcohol;
        WorkHazards = workHazards;
    }
}
