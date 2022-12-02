using Core.Domain;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

#region ServiceInjection

builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();

#endregion ServiceInjection

#region MinimalAPIs

app.MapGet("/", () => "Hello World!");

#region GeneratePdfFromHtml

app.MapPost("/GeneratePdfFromHtml", async (PdfInput? pdfInput, CancellationToken cancellationToken, IPdfGeneratorService pdfGeneratorService)
    => Results.Ok(await pdfGeneratorService.ConvertHtmlToPdf(pdfInput, cancellationToken)));

#endregion GeneratePdfFromHtml

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
