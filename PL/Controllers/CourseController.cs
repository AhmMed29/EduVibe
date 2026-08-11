using Microsoft.AspNetCore.Mvc;
using EduVibe.DTOs.Course;
using EduVibe.Models.Entities;
using EduVibe.Services;

namespace EduVibe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses([FromQuery] CourseFilterRequest request)
    {
        var result = await _courseService.GetAllAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var courseDto = await _courseService.GetByIdAsync(id);
        return Ok(courseDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _courseService.CreateAsync(course);
        return CreatedAtAction(nameof(GetCourse), new { id = created.Id }, created);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Course course)
    {
        if (id != course.Id) return BadRequest(new { message = "ID Mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _courseService.UpdateAsync(id, course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteAsync(id);
        return Ok();
    }
}
