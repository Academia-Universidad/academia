using ProgramacionV.Data;
using ProgramacionV.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgramacionV.Repositories;

public class EstudiantesRepository(AppDbContexto context)
{
    private readonly AppDbContexto _context = context;

    public async Task<List<Estudiante>> GetAllEstudiantesAsync()
    {
        return await _context.Estudiantes.ToListAsync();
    }

    public async Task<Estudiante?> GetEstudianteByIdAsync(int id)
    {
        return await _context.Estudiantes.FindAsync(id);
    }

    public async Task AddEstudianteAsync(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEstudianteAsync(Estudiante estudiante)
    {
        _context.Estudiantes.Update(estudiante);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEstudianteAsync(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante != null)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }
    }
}