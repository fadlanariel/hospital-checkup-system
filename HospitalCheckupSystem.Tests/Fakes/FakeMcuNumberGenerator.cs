using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Fakes;

public class FakeMcuNumberGenerator : IMcuNumberGenerator
{
    public string Generate(int year, int sequence)
        => $"MCU-{year}-{sequence:000000}";
}
