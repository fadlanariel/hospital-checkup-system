namespace HospitalCheckupSystem.Application.UseCases;

public class RecordVitalsCommand
{
    public Guid CheckupId { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
}
