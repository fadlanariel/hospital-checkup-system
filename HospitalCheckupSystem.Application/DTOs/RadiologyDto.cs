namespace HospitalCheckupSystem.Application.DTOs;

public class RadiologyDto
{
    public string Examination { get; set; } = default!;
    public string Findings { get; set; } = default!;
    public string Impression { get; set; } = default!;
    public string RadiologistName { get; set; } = default!;
    public DateTime ExamDate { get; set; }
}