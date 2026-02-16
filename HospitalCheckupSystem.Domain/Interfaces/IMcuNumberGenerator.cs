namespace HospitalCheckupSystem.Domain.Interfaces;

public interface IMcuNumberGenerator
{
    string Generate(int year, int sequence);
}
