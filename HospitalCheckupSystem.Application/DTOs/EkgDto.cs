namespace HospitalCheckupSystem.Application.DTOs;

public class EkgDto
{
    public string Rhythm { get; set; } = default!;
    public int HeartRate { get; set; }
    public string Axis { get; set; } = default!;
    public string Impression { get; set; } = default!;
    public string DoctorName { get; set; } = default!;
    public DateTime ExamDate { get; set; }
}