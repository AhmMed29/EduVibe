using Microsoft.AspNetCore.Mvc;
using EduVibe.DTOs.Instructor;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using EduVibe.Services;

namespace EduVibe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorController : ControllerBase
{
    private readonly IInstructorService _instructorService;

    public InstructorController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInstructors([FromQuery] InstructorFilterRequest request)
    {
        var result = await _instructorService.GetAllAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstructor(int id)
    {
        var instructorDto = await _instructorService.GetByIdAsync(id);
        return Ok(instructorDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Instructor instructor)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _instructorService.CreateAsync(instructor);
        return CreatedAtAction(nameof(GetInstructor), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Instructor instructor)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _instructorService.UpdateAsync(id, instructor);
        return Ok(instructor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _instructorService.DeleteAsync(id);
        return NoContent();
    }
}
