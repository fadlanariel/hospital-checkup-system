namespace HospitalCheckupSystem.Application.DTOs;

public class MedicalCheckupSummaryDto
{
    public Guid Id { get; set; }
    public string McuNumber { get; set; } = default!;
    public DateTime CheckupDate { get; set; }
    public string Status { get; set; } = default!;
    public bool IsFinished { get; set; }
}
