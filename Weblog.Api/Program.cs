using Microsoft.EntityFrameworkCore;
using Weblog.Api.Application.Interfaces;
using Weblog.Api.Application.Services;
using Weblog.Api.Infrastructure.Data;
using Weblog.Api.Infrastructure.Repositories;
using Weblog.Api.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    dbContext.Database.EnsureCreated();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}

app.UseCors();

app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "Weblog.Api";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
});


app.MapBlogEndpoints();

app.Run();

