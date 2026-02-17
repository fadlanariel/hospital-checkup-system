namespace HospitalCheckupSystem.Domain.Entities;

public class MedicalCheckup
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public string McuNumber { get; private set; }
    public DateTime CheckupDate { get; private set; }
    public string Status { get; private set; }
    public VitalSigns? Vitals { get; private set; }
    public Anamnesis? Anamnesis { get; private set; }
    public PhysicalExam? PhysicalExam { get; private set; }

    private MedicalCheckup() { }

    public MedicalCheckup(Guid patientId, string mcuNumber, DateTime date)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        McuNumber = mcuNumber;
        CheckupDate = date;
        Status = "Draft";
    }

    public void RecordVitals(
        decimal height, 
        decimal weight, 
        int systolic, int 
        diastolic, 
        int pulse)
    {
        Vitals = new VitalSigns(
            height, 
            weight, 
            systolic, 
            diastolic, 
            pulse);
    }

    public void RecordAnamnesis(
        string complaints,
        string pastIllness,
        string familyHistory,
        string allergies,
        bool smoking,
        bool alcohol,
        string workHazards)
    {
        Anamnesis = new Anamnesis(
            complaints,
            pastIllness,
            familyHistory,
            allergies,
            smoking,
            alcohol,
            workHazards);
    }

    public void RecordPhysicalExam(
        string generalAppearance,
        string eyes,
        string ent,
        string heart,
        string lungs,
        string abdomen,
        string neurology)
    {
        PhysicalExam = new PhysicalExam(
            generalAppearance,
            eyes,
            ent,
            heart,
            lungs,
            abdomen,
            neurology);
    }

}
