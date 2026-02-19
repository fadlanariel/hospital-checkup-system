using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalCheckupSystem.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly HospitalDbContext _db;

    public PatientRepository(HospitalDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Patient patient)
    {
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync();
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _db.Patients
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetNextSequenceAsync()
    {
        var count = await _db.Patients.CountAsync();
        return count + 1;
    }
}
