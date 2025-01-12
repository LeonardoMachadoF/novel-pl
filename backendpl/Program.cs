using backend.Data;
using backend.Data.Repository;
using backend.Routes;
using backend.Services.AuthDomain;
using backend.Services.ChapterDomain;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using backend.Utils;
using backendpl.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthUseCases();
builder.Services.AddNovelUseCases();
builder.Services.AddChapterUseCases();
builder.Services.AddValidators();

builder.Services.AddScoped<INovelRepository, NovelRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IErrorService, ErrorService>();
builder.Services.AddSingleton<ITokenGenerator,TokenGenerator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey("CHAVESEdauyjsvbdauysdgbasjkhdbgjashdbgasCRETA"u8.ToArray()),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
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

app.MapGroup("/auth").AuthRoutes().WithTags("Auth");
app.MapGroup("/novel").NovelRoutes().WithTags("Novel");
app.MapGroup("/{slug}/chapter").ChapterRoutes().WithTags("Chapter");

app.Run();