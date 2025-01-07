using backend.Data;
using backend.Data.Repository;
using backend.Entities;
using backend.Routes;
using backend.Services.ChapterDomain;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using backend.Services.ValidationService;
using backend.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddSingleton<TokenGenerator>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey("CHAVESEdauyjsvbdauysdgbasjkhdbgjashdbgasCRETA"u8.ToArray()),
            ValidIssuer = "https://localhost:5001/",
            ValidAudience = "https://localhost:5001/",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapPost("/register", async (string username, string password, string email, DataContext _context, TokenGenerator tokenGen) =>
{
    var user = new User
    {
        Username = username,
        Password = password,
        Email = email
    };
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    var token = tokenGen.GenerateToken(user.UserId, user.Email);
    
    return new { token };
});

app.MapGroup("/novel").NovelRoutes().WithTags("Novel");
app.MapGroup("/{slug}/chapter").ChapterRoutes().WithTags("Chapter");

app.Run();