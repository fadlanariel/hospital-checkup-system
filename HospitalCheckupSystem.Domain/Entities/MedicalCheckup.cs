namespace HospitalCheckupSystem.Domain.Entities;

public class MedicalCheckup
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public string McuNumber { get; private set; }
    public DateTime CheckupDate { get; private set; }
    public string Status { get; private set; }
    public VitalSigns? Vitals { get; private set; }

    private MedicalCheckup() { }

    public MedicalCheckup(Guid patientId, string mcuNumber, DateTime date)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        McuNumber = mcuNumber;
        CheckupDate = date;
        Status = "Draft";
    }

    public void RecordVitals(decimal height, decimal weight, int systolic, int diastolic, int pulse)
    {
        Vitals = new VitalSigns(height, weight, systolic, diastolic, pulse);
    }

}
