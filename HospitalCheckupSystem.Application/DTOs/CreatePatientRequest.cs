namespace HospitalCheckupSystem.Application.DTOs;

public class CreatePatientRequest
{
    public string Name { get; set; } = default!;
    public DateTime Dob {  get; set; }
    public string Gender { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Insurance { get; set; }
}
