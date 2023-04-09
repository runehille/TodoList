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

    // GET: TodoItems/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return Ok();
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
            _context.Add(todoItemModel);
            await _context.SaveChangesAsync();
        }
        return Ok();
    }

    // GET: TodoItems/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.TodoItems == null)
        {
            return NotFound();
        }

        var todoItemModel = await _context.TodoItems.FindAsync(id);
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
            try
            {
                _context.Update(todoItemModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemModelExists(todoItemModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return Ok(todoItemModel);
    }

    // GET: TodoItems/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.TodoItems == null)
        {
            return NotFound();
        }

        var todoItemModel = await _context.TodoItems
            .FirstOrDefaultAsync(m => m.Id == id);
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
        if (_context.TodoItems == null)
        {
            return Problem("Entity set 'SqlDbContext.TodoItems'  is null.");
        }
        var todoItemModel = await _context.TodoItems.FindAsync(id);
        if (todoItemModel != null)
        {
            _context.TodoItems.Remove(todoItemModel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TodoItemModelExists(Guid id)
    {
        return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
