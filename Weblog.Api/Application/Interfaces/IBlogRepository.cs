using Weblog.Api.Domain.Entities;

namespace Weblog.Api.Application.Interfaces;

public interface IBlogRepository
{
    Task<IEnumerable<Blog>> GetAllAsync();
    Task<Blog?> GetByIdAsync(int id);
    Task<Blog> CreateAsync(Blog blog);
    Task<Blog?> UpdateAsync(int id, Blog blog);
    Task<bool> DeleteAsync(int id);
}
