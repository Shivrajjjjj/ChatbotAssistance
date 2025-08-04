using ChatbotAssistance.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ✅ Register GeminiService using HttpClient
builder.Services.AddHttpClient<GeminiService>();

// ✅ Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS policy to allow frontend requests (e.g., from Blazor at http://localhost:5133)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Enable CORS globally
app.UseCors("AllowAll");

// ✅ Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Use HTTPS redirection and authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Map controller endpoints
app.MapControllers();

// ✅ Run the API
app.Run();
