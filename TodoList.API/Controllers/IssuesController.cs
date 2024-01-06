using Microsoft.AspNetCore.Mvc;
using TodoList.API.Dtos;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IssuesController : Controller
{
    private readonly IIssuesRepository _IssuesRepository;

    public IssuesController(IIssuesRepository IssuesRepository)
    {
        _IssuesRepository = IssuesRepository;
    }

    // GET: Issues
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var Issues = await _IssuesRepository.GetAllIssuesAsync();
        var result = Issues.Select(Issue => Issue.ToDto()).ToList();

        return Ok(result);
    }

    // GET: Issues/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Issue = await _IssuesRepository.GetIssueByIdAsync(id);

        if (Issue == null)
        {
            return NotFound();
        }

        var result = Issue.ToDto();

        return Ok(result);
    }

    // POST: Issues/Create
    [HttpPost("Create")]
    public async Task<IActionResult> Create(IssueCreateDto IssueCreateDto)
    {

        if (ModelState.IsValid)
        {
            await _IssuesRepository.CreateNewIssueAsync(IssueCreateDto.ToEntity());
            return Ok(IssueCreateDto);
        }
        else
        {
            return NotFound();
        }
    }

    // POST: Issues/Edit/5
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(string id, IssueEditDto IssueEditDto)
    {
        if (ModelState.IsValid)
        {
            await _IssuesRepository.EditIssueAsync(IssueEditDto.ToEntity());
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    // DELETE: Issues/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _IssuesRepository.DeleteIssueAsync(id);

        return Ok();
    }
}
