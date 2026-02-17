namespace HospitalCheckupSystem.Application.UseCases;

public class RecordPhysicalExamCommand
{
    public Guid CheckupId { get; set; }

    public string GeneralAppearance { get; set; } = "";
    public string Eyes { get; set; } = "";
    public string ENT { get; set; } = "";
    public string Heart { get; set; } = "";
    public string Lungs { get; set; } = "";
    public string Abdomen { get; set; } = "";
    public string Neurology { get; set; } = "";
}
