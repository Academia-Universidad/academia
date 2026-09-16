using Microsoft.AspNetCore.Mvc;
using PorgramacionV.Repositories;
using ProgramacionV.Models;

namespace PorgramacionV.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramasController : ControllerBase
{
    private readonly ProgramaRepository _programaRepository;

    public ProgramasController(ProgramaRepository programaRepository)
    {
        _programaRepository = programaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProgramaAcademico>>> GetAllProgramas()
    {
        var programas = await _programaRepository.GetAllProgramasAsync();
        return Ok(programas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProgramaAcademico>> GetProgramaById(int id)
    {
        var programa = await _programaRepository.GetProgramaByIdAsync(id);
        if (programa == null)
        {
            return NotFound();
        }
        return Ok(programa);
    }

    [HttpPost]
    public async Task<ActionResult> AddPrograma(ProgramaAcademico programa)
    {
        await _programaRepository.AddProgramaAsync(programa);
        return CreatedAtAction(nameof(GetProgramaById), new { id = programa.Id }, programa);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrograma(int id, ProgramaAcademico programa)
    {
        if (id != programa.Id)
        {
            return BadRequest();
        }

        await _programaRepository.UpdateProgramaAsync(programa);
        return NoContent();
    }

}