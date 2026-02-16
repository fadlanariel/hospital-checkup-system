using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Domain.Interfaces;

public interface IMedicalCheckupRepository
{
    Task<int> GetNextSequenceAsync(int year);
    Task AddAsync(MedicalCheckup checkup);
}
