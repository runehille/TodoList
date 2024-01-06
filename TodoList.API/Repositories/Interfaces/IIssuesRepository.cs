using TodoList.API.Models;

namespace TodoList.API.Repositories.Interfaces
{
    public interface IIssuesRepository
    {
        Task<List<Issue>> GetAllIssuesAsync();
        Task<Issue> GetIssueByIdAsync(string? id);
        Task<Issue> CreateNewIssueAsync(Issue item);
        Task<Issue> EditIssueAsync(Issue item);
        Task DeleteIssueAsync(string? id);
    }
}