using ChatbotAssistance.Bot;
using ChatbotAssistance.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // Register Adapter with error handling
        services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

        // Register ChatBot logic
        services.AddTransient<IBot, ChatBot>();

        // Register OpenAI Service using HttpClient
        services.AddHttpClient<GeminiService>();

        // Add configuration to DI container
        services.AddSingleton<IConfiguration>(configuration);

        // Add Controller support
        services.AddControllers().AddNewtonsoftJson();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Enables /api/messages
                endpoints.MapGet("/", () => "Chatbot Assistance Bot is running...");
            });
        });
    });

await builder.Build().RunAsync();
