using HospitalCheckupSystem.Domain.Enums;

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
    public List<LabResultItem> LabResults { get; private set; } = new();
    public MedicalConclusion? Conclusion { get; private set; }
    public bool IsFinished { get; private set; }

    private MedicalCheckup() { }

    private bool IsComplete()
    {
        return Vitals != null
            && Anamnesis != null
            && PhysicalExam != null
            && LabResults.Any();
    }

    private void EnsureNotFinished()
    {
        if (IsFinished)
            throw new InvalidOperationException("Checkup already finished");
    }

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
        int systolic, 
        int diastolic, 
        int pulse)
    {
        EnsureNotFinished();

        if (height <= 0)
            throw new InvalidOperationException("Invalid height");

        if (weight <= 0)
            throw new InvalidOperationException("Invalid weight");

        if (systolic < diastolic)
            throw new InvalidOperationException("Invalid blood pressure");

        if (pulse <= 0)
            throw new InvalidOperationException("Invalid pulse");

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
        EnsureNotFinished();

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
        EnsureNotFinished();

        PhysicalExam = new PhysicalExam(
            generalAppearance,
            eyes,
            ent,
            heart,
            lungs,
            abdomen,
            neurology);
    }

    public void AddLabResult(
        string testName,
        string unit,
        string value,
        decimal? normalMin,
        decimal? normalMax)
    {
        EnsureNotFinished();

        if (string.IsNullOrWhiteSpace(testName))
            throw new InvalidOperationException("Lab test name is required");

        if (normalMin.HasValue && normalMax.HasValue && normalMin > normalMax)
            throw new InvalidOperationException("Invalid lab normal range");

        LabResults.Add(new LabResultItem(
            testName,
            unit,
            value,
            normalMin,
            normalMax));
    }

    public void MakeConclusion(
        FitnessStatus fitnessStatus,
        string diagnosis,
        string recommendation)
    {
        if (IsFinished)
            throw new InvalidOperationException("Checkup already finished");

        if (!IsComplete())
            throw new InvalidOperationException("Medical checkup data incomplete");

        Conclusion = new MedicalConclusion(
            fitnessStatus,
            diagnosis,
            recommendation);

        IsFinished = true;
    }

}
