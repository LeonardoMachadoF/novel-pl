using backend.Data;
using backend.Data.Repository;
using backend.Middlewares;
using backend.Routes;
using backend.Services.AuthDomain;
using backend.Services.ChapterDomain;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using backend.Utils;
using backendpl.Extensions;
using backendpl.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

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

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapApplicationRoutes();

app.Run();