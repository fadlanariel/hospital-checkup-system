namespace HospitalCheckupSystem.Application.DTOs;

public class RecordPhysicalExamRequest
{
    public string GeneralAppearance { get; set; } = default!;
    public string Eyes { get; set; } = default!;
    public string ENT { get; set; } = default!;
    public string Heart { get; set; } = default!;
    public string Lungs { get; set; } = default!;
    public string Abdomen { get; set; } = default!;
    public string Neurology { get; set; } = default!;
}