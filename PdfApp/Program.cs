using Core.Domain;
using Core.FluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PdfApp.Attributes;
using PdfApp.Middlewares;
using Persistence.Context;
using Services.Interfaces;
using Services.Interfaces.Shared;
using Services.Services;
using Services.Shared.ApiKeyServices;
using Services.Shared.CachingService;
using Services.Shared.EnumServices;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

#region ServiceInjection

builder.Services.AddDbContext<PdfApiContext>(opt => opt.UseInMemoryDatabase("PdfApi"));
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IEnumService, EnumService>();

#endregion ServiceInjection

#region AutoMapper

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews();

#endregion AutoMapper

#region ILogger

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

#endregion ILogger

#region FluentValidations

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IValidator<PdfInputDto>, PdfInputDtoValidator>();
builder.Services.AddScoped<IValidator<PageMarginsDto>, PageMarginsDtoValidator>();
builder.Services.AddScoped<IValidator<PdfOptionsDto>, PdfOptionsDtoValidator>();
builder.Services.AddScoped<IValidator<PdfOutputDto>, PdfOutputDtoValidator>();

#endregion FluentValidations

#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AuthenticationGen

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pdf-Api Api-Key Authentication", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "XApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { key, new List<string>() }
    };
    
    c.AddSecurityRequirement(requirement);
});

#endregion AuthenticationGen

#endregion Swagger

// Add services to the container.
var app = builder.Build();

#region SwaggerUI

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pdf Generation Api v.1.2)");
    c.RoutePrefix = "";
});

#endregion SwaggerUI

#region Middleware

app.UseMiddleware<ApiKeyMiddleware>();

#endregion Middleware

#region MinimalAPIs

#region GeneratePdfFromHtml

app.MapPost("/GeneratePdfFromHtml", async (PdfInputDto? pdfInput, CancellationToken cancellationToken, IPdfGeneratorService pdfGeneratorService)
    => Results.Ok(await pdfGeneratorService.ConvertHtmlToPdf(pdfInput, cancellationToken)));

#endregion GeneratePdfFromHtml

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
