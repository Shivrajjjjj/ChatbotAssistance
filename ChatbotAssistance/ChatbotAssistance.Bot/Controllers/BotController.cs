using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace ChatbotAssistance.Bot.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class BotController : ControllerBase
    {
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IBot _bot;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="adapter">Bot Framework HTTP Adapter</param>
        /// <param name="bot">Your Bot instance (inherited from ActivityHandler)</param>
        public BotController(IBotFrameworkHttpAdapter adapter, IBot bot)
        {
            _adapter = adapter;
            _bot = bot;
        }

        /// <summary>
        /// POST endpoint for receiving messages from Bot Framework or Emulator.
        /// </summary>
        /// <returns>Task</returns>
        [HttpPost]
        public async Task PostAsync()
        {
            await _adapter.ProcessAsync(Request, Response, _bot);
        }

        /// <summary>
        /// GET endpoint for testing if the bot API is alive.
        /// </summary>
        /// <returns>Status message</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("✅ Chatbot Assistance API is running. Use POST /api/messages to interact.");
        }
    }
}
