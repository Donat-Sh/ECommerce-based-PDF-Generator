using Core.Domain;
using Core.FluentValidations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Services.Interfaces;
using Services.Services;
using Services.Shared;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

#region ServiceInjection

builder.Services.AddDbContext<PdfApiContext>(opt => opt.UseInMemoryDatabase("PdfApi"));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();

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

#region FluentValidation

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IValidator<PdfInputDto>, PdfInputDtoValidator>();
builder.Services.AddScoped<IValidator<PageMarginsDto>, PageMarginsDtoValidator>();
builder.Services.AddScoped<IValidator<PdfOptionsDto>, PdfOptionsDtoValidator>();
builder.Services.AddScoped<IValidator<PdfOutputDto>, PdfOutputDtoValidator>();

#endregion FluentValidation

#region MinimalAPIs

#region GeneratePdfFromHtml

app.MapPost("/GeneratePdfFromHtml", async (PdfInputDto? pdfInput, CancellationToken cancellationToken, IPdfGeneratorService pdfGeneratorService)
    => Results.Ok(await pdfGeneratorService.ConvertHtmlToPdf(pdfInput, cancellationToken)));

#endregion GeneratePdfFromHtml

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
