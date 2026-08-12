using Microsoft.AspNetCore.Mvc;
using EduVibe.DTOs.Department;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using EduVibe.Services;

namespace EduVibe.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments([FromQuery] DepartmentFilterRequest request)
    {
        var result = await _departmentService.GetAllAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartment(int id)
    {
        var departmentDto = await _departmentService.GetByIdAsync(id);
        return Ok(departmentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Department department)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _departmentService.CreateAsync(department);
        return CreatedAtAction(nameof(GetDepartment), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Department department)
    {
        if (id != department.Id) return BadRequest(new { Message = $"Department {id} Not Found" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _departmentService.UpdateAsync(id, department);
        return Ok(department);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _departmentService.DeleteAsync(id);
        return Ok(new { message = $"Department [{id}] Deleted Successfully" });
    }
}
