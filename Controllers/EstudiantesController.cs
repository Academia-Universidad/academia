using Microsoft.AspNetCore.Mvc;
using ProgramacionV.Models;
using ProgramacionV.Repositories;

namespace PorgramacionV.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly EstudiantesRepository _estudiantesRepository;

    public EstudiantesController(EstudiantesRepository estudiantesRepository)
    {
        _estudiantesRepository = estudiantesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Estudiante>>> GetAllEstudiantes()
    {
        var estudiantes = await _estudiantesRepository.GetAllEstudiantesAsync();
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Estudiante>> GetEstudianteById(int id)
    {
        var estudiante = await _estudiantesRepository.GetEstudianteByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound();
        }
        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<ActionResult> AddEstudiante(Estudiante estudiante)
    {
        await _estudiantesRepository.AddEstudianteAsync(estudiante);
        return CreatedAtAction(nameof(GetEstudianteById), new { id = estudiante.Id }, estudiante);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.Id)
        {
            return BadRequest();
        }

        await _estudiantesRepository.UpdateEstudianteAsync(estudiante);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEstudiante(int id)
    {
        await _estudiantesRepository.DeleteEstudianteAsync(id);
        return NoContent();
    }
}