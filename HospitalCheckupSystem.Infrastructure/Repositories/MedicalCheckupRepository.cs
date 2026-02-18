using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalCheckupSystem.Infrastructure.Repositories;

public class MedicalCheckupRepository : IMedicalCheckupRepository
{
    private readonly HospitalDbContext _context;

    public MedicalCheckupRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MedicalCheckup checkup)
    {
        await _context.MedicalCheckups.AddAsync(checkup);
        await _context.SaveChangesAsync();
    }

    public async Task<MedicalCheckup?> GetByIdAsync(Guid id)
    {
        return await _context.MedicalCheckups
            .Include(x => x.LabResults)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetNextSequenceAsync(int year)
    {
        var count = await _context.MedicalCheckups
            .CountAsync(x => x.CheckupDate.Year == year);

        return count + 1;
    }

    public async Task UpdateAsync(MedicalCheckup checkup)
    {
        _context.MedicalCheckups.Update(checkup);
        await _context.SaveChangesAsync();
    }
}
