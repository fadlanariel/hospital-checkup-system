namespace HospitalCheckupSystem.Application.DTOs;

public class StartMedicalCheckupRequest
{
    public Guid PatientId { get; set; }
    public DateTime CheckupDate { get; set; }
}

