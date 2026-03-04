namespace HospitalCheckupSystem.Application.DTOs;

public class MedicalCheckupReportDto
{
    public string PatientMrn { get; set; } = default;
    public string PatientName { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }

    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string McuNumber { get; set; } = default!;
    public DateTime CheckupDate { get; set; }

    public VitalSignsDto? Vitals { get; set; }
    public AnamnesisDto? Anamnesis { get; set; }
    public PhysicalExamDto? PhysicalExam { get; set; }
    public List<LabResultItemDto> LabResults { get; set; } = new();
    public RadiologyDto? Radiology { get; set; }
    public MedicalConclusionDto Conclusion { get; set; } = default!;
}
