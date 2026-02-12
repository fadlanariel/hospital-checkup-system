using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Domain.Interfaces;

public interface IPatientRepository
{
    Task AddAsync(Patient patient);
    Task<int> GetNextSequenceAsync();
}
