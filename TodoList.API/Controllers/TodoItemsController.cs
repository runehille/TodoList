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

        if (todoItem == null)
        {
            return NotFound();
        }

        var result = todoItem.ToDto();

        return Ok(result);
    }

    // POST: TodoItems/Create
    [HttpPost("Create")]
    public async Task<IActionResult> Create(TodoItemCreateDto todoItemCreateDto)
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
    public async Task<IActionResult> Edit(string id, TodoItemEditDto todoItemEditDto)
    {
        if (ModelState.IsValid)
        {
            await _todoItemsRepository.EditTodoItemAsync(todoItemEditDto.ToEntity());
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    // DELETE: TodoItems/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _todoItemsRepository.DeleteTodoItemAsync(id);

        return Ok();
    }
}
