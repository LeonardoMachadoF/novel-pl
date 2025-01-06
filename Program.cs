using backend.Data;
using backend.Data.Repository;
using backend.Routes;
using backend.Services.ChapterDomain;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using backend.Services.ValidationService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddNovelUseCases();
builder.Services.AddChapterUseCases();
builder.Services.AddScoped<INovelRepository, NovelRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<INovelValidationService, NovelValidationService>();
builder.Services.AddScoped<IChapterValidationService, ChapterValidationService>();
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
app.ChapterRoutes();

app.Run();