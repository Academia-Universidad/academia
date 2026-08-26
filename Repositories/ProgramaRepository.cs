using ProgramacionV.Data;
using ProgramacionV.Models;
using Microsoft.EntityFrameworkCore;

namespace PorgramacionV.Repositories;

public class ProgramaRepository
{
    private readonly AppDbContexto _context;

    public ProgramaRepository(AppDbContexto context)
    {
        _context = context;
    }

    public async Task<List<ProgramaAcademico>> GetAllProgramasAsync()
    {
        return await _context.ProgramasAcademicos.ToListAsync();
    }

    public async Task<ProgramaAcademico?> GetProgramaByIdAsync(int id)
    {
        return await _context.ProgramasAcademicos.FindAsync(id);
    }

    public async Task AddProgramaAsync(ProgramaAcademico programa)
    {
        _context.ProgramasAcademicos.Add(programa);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProgramaAsync(ProgramaAcademico programa)
    {
        _context.ProgramasAcademicos.Update(programa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProgramaAsync(int id)
    {
        var programa = await _context.ProgramasAcademicos.FindAsync(id);
        if (programa != null)
        {
            _context.ProgramasAcademicos.Remove(programa);
            await _context.SaveChangesAsync();
        }
    }
}