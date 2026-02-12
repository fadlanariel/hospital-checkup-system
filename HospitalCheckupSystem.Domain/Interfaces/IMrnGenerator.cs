namespace HospitalCheckupSystem.Domain.Interfaces;

public interface IMrnGenerator
{
    string Generate(int sequence);
}