using Microsoft.AspNetCore.Mvc;
using TodoList.API.Dtos;
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
        var todoItems = await _todoItemsRepository.GetAllTodoItemsAsync();
        var result = todoItems.Select(todoItem => todoItem.ToDto()).ToList();

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
        var result = todoItem.ToDto();

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: TodoItems/Create
    [HttpPost("Create")]
    public async Task<IActionResult> Create([Bind("Title,Description")] TodoItemCreateDto todoItemCreateDto)
    {
        if (ModelState.IsValid)
        {
            var todoItem = todoItemCreateDto.ToEntity();
            await _todoItemsRepository.CreateNewTodoItemAsync(todoItem);
            return Ok(todoItemCreateDto);
        }
        else
        {
            return NotFound();
        }
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
            return Ok(todoItemModel);
        }
        else
        {
            return NotFound();
        }
    }

    // DELETE: TodoItems/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        else if (await _todoItemsRepository.GetTodoItemByIdAsync(id) == null)
        {
            return NotFound();
        }

        await _todoItemsRepository.DeleteTodoItemAsync(id);

        return Ok();
    }
}
