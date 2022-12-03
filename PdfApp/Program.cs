using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Services.Interfaces;
using Services.Services;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

#region ServiceInjection

builder.Services.AddDbContext<PdfApiContext>(opt => opt.UseInMemoryDatabase("PdfApi"));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();

#endregion ServiceInjection

#region ILogger

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

#endregion ILogger

#region MinimalAPIs

app.MapGet("/", () => "Hello World!");

#region GeneratePdfFromHtml

app.MapPost("/GeneratePdfFromHtml", async (PdfInputDto? pdfInput, CancellationToken cancellationToken, IPdfGeneratorService pdfGeneratorService)
    => Results.Ok(await pdfGeneratorService.ConvertHtmlToPdf(pdfInput, cancellationToken)));

#endregion GeneratePdfFromHtml

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
