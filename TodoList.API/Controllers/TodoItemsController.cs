using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.API.DataAccess;
using TodoList.API.DataModels;

namespace TodoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemsController : Controller
{
    private readonly SqlDbContext _context;
    private readonly ITodoItemsRepository _todoItemsRepository;

    public TodoItemsController(SqlDbContext context, ITodoItemsRepository todoItemsRepository)
    {
        _context = context;
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
    public async Task<IActionResult> Details(Guid? id)
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Create")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedTimestamp,LastModifiedTimestamp,LastModifiedBy")] TodoItemModel todoItemModel)
    {
        if (ModelState.IsValid)
        {
            todoItemModel.Id = Guid.NewGuid();
            await _todoItemsRepository.CreateNewTodoItemAsync(todoItemModel);
        }
        return Ok(todoItemModel);
    }

    // GET: TodoItems/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid? id)
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Edit/{id}")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,CreatedTimestamp,LastModifiedTimestamp,LastModifiedBy")] TodoItemModel todoItemModel)
    {
        if (id != todoItemModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
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
    public async Task<IActionResult> Delete(Guid? id)
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
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _todoItemsRepository.DeleteTodoItemAsync(id);

        return RedirectToAction(nameof(Index));
    }


}
