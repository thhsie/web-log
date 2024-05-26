using Weblog.Api.Application.Interfaces;

namespace Weblog.Api.Domain.Entities;
public static class BlogEndpoints
{
    public static void MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", GetAllBlogs);
        app.MapGet("/blogs/{id}", GetBlogById);
        app.MapPost("/blogs", CreateBlog);
        app.MapPut("/blogs/{id}", UpdateBlog);
        app.MapDelete("/blogs/{id}", DeleteBlog);
    }
    /// <summary>
    /// Retrieves all blogs.
    /// </summary>
    /// <returns>A list of blogs.</returns>
    internal static async Task<IResult> GetAllBlogs(IBlogService blogService)
    {

        var blogs = await blogService.GetAllAsync();
        return Results.Ok(blogs);
    }
    /// <summary>
    /// Retrieves a blog by its ID.
    /// </summary>
    /// <param name="id">The ID of the blog to retrieve.</param>
    /// <returns>The blog with the specified ID, or a 404 NotFound if the blog is not found.</returns>
    internal static async Task<IResult> GetBlogById(int id, IBlogService blogService)
    {

        var blog = await blogService.GetByIdAsync(id);
        if (blog == null)
            return Results.NotFound();
        return Results.Ok(blog);
    }
    /// <summary>
    /// Creates a new blog.
    /// </summary>
    /// <param name="blog">The blog to create.</param>
    /// <returns>The created blog, with its ID.</returns>
    internal static async Task<IResult> CreateBlog(Blog blog, IBlogService blogService)
    {

        var createdBlog = await blogService.CreateAsync(blog);
        return Results.Created($"/blogs/{createdBlog.Id}", createdBlog);
    }
    /// <summary>
    /// Updates an existing blog.
    /// </summary>
    /// <param name="id">The ID of the blog to update.</param>
    /// <param name="blog">The updated blog data.</param>
    /// <returns>The updated blog, or a 404 NotFound if the blog is not found.</returns>
    internal static async Task<IResult> UpdateBlog(int id, Blog blog, IBlogService blogService)
    {

        var updatedBlog = await blogService.UpdateAsync(id, blog);
        if (updatedBlog == null)
            return Results.NotFound();
        return Results.Ok(updatedBlog);
    }
    /// <summary>
    /// Deletes a blog.
    /// </summary>
    /// <param name="id">The ID of the blog to delete.</param>
    /// <returns>A 204 NoContent response if the blog was deleted, or a 404 NotFound if the blog is not found.</returns>
    internal static async Task<IResult> DeleteBlog(int id, IBlogService blogService)
    {

        var isDeleted = await blogService.DeleteAsync(id);
        if (!isDeleted)
            return Results.NotFound();
        return Results.NoContent();
    }
}
