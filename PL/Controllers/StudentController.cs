using Microsoft.AspNetCore.Mvc;
using EduVibe.DTOs.Student;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using Microsoft.AspNetCore.Authorization;
namespace EduVibe.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager,Instructor")]
    public async Task<IActionResult> GetAllStudents([FromQuery] StudentFilterRequest request)
    {
        var result = await _studentService.GetAllAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager,Instructor,Student")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var studentDto = await _studentService.GetByIdAsync(id);
        return Ok(studentDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetStudent), new { id = created.Id }, created);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await _studentService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
