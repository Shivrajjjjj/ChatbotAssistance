using Microsoft.AspNetCore.Components.Server; // ✅ Add this
using ChatbotAssistance.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Register named HttpClient
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5007/");
});

// Add Razor Components with interactive server support
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Enable detailed circuit errors
builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Map Razor components
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();