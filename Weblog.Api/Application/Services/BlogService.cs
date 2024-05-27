using Weblog.Api.Application.Interfaces;
using Weblog.Api.Domain.DTOs;
using Weblog.Api.Domain.Entities;

namespace Weblog.Api.Application.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IEnumerable<BlogPreview>> GetAllAsync()
    {
        return await _blogRepository.GetAllAsync();
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await _blogRepository.GetByIdAsync(id);
    }

    public async Task<Blog> CreateAsync(Blog blog)
    {
        blog = new Blog(blog.Id, blog.Title, blog.Content, blog.Content.Substring(0, Math.Min(100, blog.Content.Length)) + "...");
        return await _blogRepository.CreateAsync(blog);
    }

    public async Task<Blog?> UpdateAsync(int id, Blog blog)
    {
        blog = new Blog(blog.Id, blog.Title, blog.Content, blog.Content.Substring(0, Math.Min(100, blog.Content.Length)) + "...");
        return await _blogRepository.UpdateAsync(id, blog);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _blogRepository.DeleteAsync(id);
    }
}
