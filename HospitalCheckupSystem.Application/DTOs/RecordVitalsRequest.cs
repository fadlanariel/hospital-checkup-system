namespace HospitalCheckupSystem.Application.DTOs;

public class RecordVitalsRequest
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
}
