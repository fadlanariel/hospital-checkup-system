using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Domain.Interfaces;

public interface IMedicalCheckupRepository
{
    Task<int> GetNextSequenceAsync(int year);
    Task AddAsync(MedicalCheckup checkup);
    Task<MedicalCheckup?> GetByIdAsync(Guid id);
    Task UpdateAsync(MedicalCheckup checkup);
    Task<List<MedicalCheckup>> GetByPatientIdAsync(Guid patientId);
}
