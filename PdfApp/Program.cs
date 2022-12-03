using Core.Domain;
using Core.FluentValidations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Services.Interfaces;
using Services.Interfaces.Shared;
using Services.Services;
using Services.Shared.ApiKeyServices;
using Services.Shared.CachingService;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

#region ServiceInjection

builder.Services.AddDbContext<PdfApiContext>(opt => opt.UseInMemoryDatabase("PdfApi"));
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();

#region ApiKeyAuthentication

builder.Services.AddScoped<ApiKeyAuthenticationHandler>();
builder.Services.AddAuthentication().AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, null);

builder.Services.AddAuthentication(ApiKeyAuthenticationOptions.DefaultScheme);

#endregion ApiKeyAuthentication

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

builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition(ApiKeyAuthenticationOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = ApiKeyAuthenticationOptions.HeaderName,
        Type = SecuritySchemeType.ApiKey
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiKeyAuthenticationOptions.DefaultScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion AuthenticationGen

app.UseSwagger();
app.UseSwaggerUI();

#endregion Swagger

#region MinimalAPIs

#region GeneratePdfFromHtml

app.MapPost("/GeneratePdfFromHtml", async (PdfInputDto? pdfInput, CancellationToken cancellationToken, IPdfGeneratorService pdfGeneratorService)
    => Results.Ok(await pdfGeneratorService.ConvertHtmlToPdf(pdfInput, cancellationToken)));

#endregion GeneratePdfFromHtml

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
