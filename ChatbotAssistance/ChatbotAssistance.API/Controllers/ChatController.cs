using Microsoft.AspNetCore.Mvc;
using ChatbotAssistance.Shared.Models;
using ChatbotAssistance.Shared.Services;

namespace ChatbotAssistance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly GeminiService _gemini;

        public ChatController(GeminiService gemini)
        {
            _gemini = gemini;
        }

        /// <summary>
        /// Accepts a user question and returns an AI-generated response using Gemini.
        /// </summary>
        [HttpPost("ask")]
        public async Task<ActionResult<ChatResponse>> Ask([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Question))
                return BadRequest(new { Error = "Question cannot be empty." });

            try
            {
                var answer = await _gemini.GenerateContentAsync(request.Question);
                return Ok(new ChatResponse { Answer = answer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Failed to generate response.",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Generates a document based on the provided type.
        /// </summary>
        [HttpPost("generate-document")]
        public async Task<ActionResult<DocumentResponse>> GenerateDocument([FromBody] DocumentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Type))
                return BadRequest(new { Error = "Document type is required." });

            try
            {
                var document = await _gemini.GenerateDocumentAsync(request.Type);
                return Ok(new DocumentResponse { Document = document });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Failed to generate document.",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Routes a query to the appropriate department.
        /// </summary>
        [HttpPost("route")]
        public ActionResult<RouteResponse> RouteQuery([FromBody] RouteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Department))
                return BadRequest(new { Error = "Department name is required." });

            var routedTo = request.Department.Trim().ToLower() switch
            {
                "hr" => "hr@company.com",
                "it" => "it-support@company.com",
                "sales" => "sales@company.com",
                _ => "general@company.com"
            };

            return Ok(new RouteResponse { RoutedTo = routedTo });
        }
    }
}