using Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

#region MinimalAPIs

app.MapPost("/", (HttpRequest request, PdfInput? input) => {
    return Results.Ok(new PdfOutput(errorMessage: "To be implemented"));
});

var test = Results.Ok(new PdfOutput(errorMessage: "To be implemented"));

app.MapGet("/", () => "Hello World!");

app.MapPost("/GeneratePdfFromHtml", );

#endregion MinimalAPIs

app.Run();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
