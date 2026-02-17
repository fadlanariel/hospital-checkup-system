using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Domain.Services;

public class McuNumberGenerator : IMcuNumberGenerator
{
    public string Generate(int year, int sequence)
    {
        return $"MCU-{year}-{sequence:D6}";
    }
}
