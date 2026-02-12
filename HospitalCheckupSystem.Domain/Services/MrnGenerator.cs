using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Domain.Services;

public class MrnGenerator : IMrnGenerator
{
    public string Generate(int sequence)
    {
        var year = DateTime.UtcNow.Year;
        return $"MRN-{year}-{sequence:D6}";
    }
}
