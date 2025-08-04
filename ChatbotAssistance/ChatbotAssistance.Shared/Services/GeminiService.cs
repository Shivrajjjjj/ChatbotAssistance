using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ChatbotAssistance.Shared.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly JsonSerializerOptions _jsonOptions;

        public GeminiService(IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"]
                     ?? throw new InvalidOperationException("Gemini API key is missing in configuration.");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        /// <summary>
        /// Public method used by bots or UI to ask a question.
        /// </summary>
        public async Task<string> AskQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return "⚠️ Question cannot be empty.";

            // Optional: Add system prompt or context
            var prompt = $"You are a helpful assistant. Answer the following:\n{question.Trim()}";

            return await GenerateContentAsync(prompt);
        }

        /// <summary>
        /// Sends a prompt to Gemini and returns generated content.
        /// </summary>
        public async Task<string> GenerateContentAsync(string prompt)
        {
            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(payload, _jsonOptions);

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent")
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("X-goog-api-key", _apiKey);

            try
            {
                var response = await _httpClient.SendAsync(request);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"❌ Gemini API Error {response.StatusCode}: {responseJson}";

                using var doc = JsonDocument.Parse(responseJson);
                var reply = doc.RootElement
                               .GetProperty("candidates")[0]
                               .GetProperty("content")
                               .GetProperty("parts")[0]
                               .GetProperty("text")
                               .GetString();

                return reply ?? "No response from Gemini.";
            }
            catch (Exception ex)
            {
                return $"❌ Exception while calling Gemini: {ex.Message}";
            }
        }

        /// <summary>
        /// Simulates a response for testing.
        /// </summary>
        public async Task<string> GetSimulatedResponseAsync(string input)
        {
            await Task.Delay(300);
            return $"🤖 Simulated Gemini response to: \"{input}\"";
        }

        /// <summary>
        /// Simulates document generation.
        /// </summary>
        public async Task<string> GenerateDocumentAsync(string type)
        {
            await Task.Delay(300);
            return $"📄 Document generated for type: \"{type}\"";
        }
    }
}