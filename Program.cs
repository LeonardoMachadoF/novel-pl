using backend.Data;
using backend.Data.Repository;
using backend.Routes;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite("Data Source=noveldb.sqlite");
});

builder.Services.AddScoped<INovelService, NovelService>();
builder.Services.AddScoped<INovelRepository, NovelRepository>();
builder.Services.AddSingleton<IErrorService, ErrorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint(
            "/openapi/v1.json", 
            "Demo"
            );
    });
}

app.UseHttpsRedirection();

app.NovelRoutes();

app.Run();