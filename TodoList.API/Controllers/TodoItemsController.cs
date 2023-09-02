using Microsoft.AspNetCore.Mvc;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemsController : Controller
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public TodoItemsController(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }

    // GET: TodoItems
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _todoItemsRepository.GetAllTodoItemsAsync();

        return Ok(result);
    }

    // GET: TodoItems/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var todoItem = await _todoItemsRepository.GetTodoItemByIdAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

    // POST: TodoItems/Create
    [HttpPost("Create")]
    public async Task<IActionResult> Create([Bind("Title,Description")] TodoItem todoItem)
    {
        if (ModelState.IsValid)
        {
            await _todoItemsRepository.CreateNewTodoItemAsync(todoItem);
            return Ok(todoItem);
        }
        else
        {
            return NotFound();
        }
    }

    // GET: TodoItems/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var todoItemModel = await _todoItemsRepository.GetTodoItemByIdAsync(id);

        if (todoItemModel == null)
        {
            return NotFound();
        }
        return Ok(todoItemModel);
    }

    // POST: TodoItems/Edit/5
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(string id, [Bind("Title,Description")] TodoItem todoItemModel)
    {
        if (id != todoItemModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            todoItemModel.LastModifiedTimestamp = DateTime.UtcNow;
            await _todoItemsRepository.EditTodoItemAsync(todoItemModel);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return NotFound();
        }
    }

    // GET: TodoItems/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var todoItemModel = await _todoItemsRepository.GetTodoItemByIdAsync(id);

        if (todoItemModel == null)
        {
            return NotFound();
        }

        return Ok(todoItemModel);
    }

    // POST: TodoItems/Delete/5
    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _todoItemsRepository.DeleteTodoItemAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
