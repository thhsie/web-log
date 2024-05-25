using Microsoft.EntityFrameworkCore;
using Weblog.Api.Application.Interfaces;
using Weblog.Api.Application.Services;
using Weblog.Api.Infrastructure.Data;
using Weblog.Api.Infrastructure.Repositories;
using Weblog.Api.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Weblog.Api";
    config.Title = "Weblog.Api v1";
    config.Version = "v1";
});

var app = builder.Build();

// Apply any pending migrations on application startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    dbContext.Database.EnsureCreated();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}


app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "Weblog.Api";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
});


app.MapGet("/blogs", GetAllBlogs);
app.MapGet("/blogs/{id}", GetBlogById);
app.MapPost("/blogs", CreateBlog);
app.MapPut("/blogs/{id}", UpdateBlog);
app.MapDelete("/blogs/{id}", DeleteBlog);

app.Run();

async Task<IResult> GetAllBlogs(IBlogService blogService)
{
    var blogs = await blogService.GetAllAsync();
    return Results.Ok(blogs);
}

async Task<IResult> GetBlogById(int id, IBlogService blogService)
{
    var blog = await blogService.GetByIdAsync(id);
    if (blog == null)
        return Results.NotFound();
    return Results.Ok(blog);
}

async Task<IResult> CreateBlog(Blog blog, IBlogService blogService)
{
    var createdBlog = await blogService.CreateAsync(blog);
    return Results.Created($"/blogs/{createdBlog.Id}", createdBlog);
}

async Task<IResult> UpdateBlog(int id, Blog blog, IBlogService blogService)
{
    var updatedBlog = await blogService.UpdateAsync(id, blog);
    if (updatedBlog == null)
        return Results.NotFound();
    return Results.Ok(updatedBlog);
}

async Task<IResult> DeleteBlog(int id, IBlogService blogService)
{
    var isDeleted = await blogService.DeleteAsync(id);
    if (!isDeleted)
        return Results.NotFound();
    return Results.NoContent();
}
