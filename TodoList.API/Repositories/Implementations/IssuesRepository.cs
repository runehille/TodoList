using Microsoft.EntityFrameworkCore;
using TodoList.API.DataAccess.Context;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Repositories.Implementations;

public class IssuesRepository : IIssuesRepository
{
    private readonly AppDbContext _dbContext;

    public IssuesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Issue>> GetAllIssuesAsync()
    {
        var result = await _dbContext.Issues.ToListAsync();

        return result;
    }

    public async Task<Issue> GetIssueByIdAsync(string? id)
    {
        var Issue = await _dbContext.Issues.FirstOrDefaultAsync(m => m.Id == id);

        return Issue!;
    }
    public async Task<Issue> CreateNewIssueAsync(Issue Issue)
    {

        _dbContext.Issues.Add(Issue);

        await _dbContext.SaveChangesAsync();

        return Issue;
    }
    public async Task<Issue> EditIssueAsync(Issue Issue)
    {

        _dbContext.Update(Issue);
        await _dbContext.SaveChangesAsync();

        return Issue;
    }

    public async Task DeleteIssueAsync(string? id)
    {
        var Issue = await _dbContext.Issues.FindAsync(id);
        if (Issue != null)
        {
            _dbContext.Issues.Remove(Issue);
        }

        await _dbContext.SaveChangesAsync();
    }
}
