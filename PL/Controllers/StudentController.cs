using Microsoft.AspNetCore.Mvc;
using EduVibe.DTOs.Student;
using EduVibe.Models.Entities;
using EduVibe.Services;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
namespace EduVibe.Controllers;

[Authorize(Roles ="Admin")]
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents([FromQuery] StudentFilterRequest request)
    {
        var result = await _studentService.GetAllAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var studentDto = await _studentService.GetByIdAsync(id);
        return Ok(studentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Student student)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _studentService.CreateAsync(student);
        return CreatedAtAction(nameof(GetStudent), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Student student)
    {
        if (id != student.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _studentService.UpdateAsync(id, student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
