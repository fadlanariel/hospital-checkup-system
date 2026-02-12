namespace HospitalCheckupSystem.Application.DTOs;

public class CreatePatientResponse
{
    public Guid Id { get; set; }
    public string Mrn { get; set; } = default!;
    public string Name { get; set; } = default!;
}
