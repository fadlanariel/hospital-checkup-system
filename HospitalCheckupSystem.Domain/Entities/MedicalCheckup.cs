namespace HospitalCheckupSystem.Domain.Entities;

public class MedicalCheckup
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public string McuNumber { get; private set; }
    public DateTime CheckupDate { get; private set; }
    public string Status { get; private set; }

    private MedicalCheckup() { }

    public MedicalCheckup(Guid patientId, string mcuNumber, DateTime date)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        McuNumber = mcuNumber;
        CheckupDate = date;
        Status = "Draft";
    }
}
