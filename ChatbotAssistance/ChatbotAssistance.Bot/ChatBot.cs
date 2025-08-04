using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using ChatbotAssistance.Shared.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ChatbotAssistance.Bot
{
    /// <summary>
    /// ChatBot class that handles incoming user messages and replies using GeminiService.
    /// </summary>
    public class ChatBot : ActivityHandler
    {
        private readonly GeminiService _gemini;

        /// <summary>
        /// Constructor with DI for GeminiService.
        /// </summary>
        public ChatBot(GeminiService gemini)
        {
            _gemini = gemini;
        }

        /// <summary>
        /// Handles message activity from the user.
        /// </summary>
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var userMessage = turnContext.Activity.Text;

            // Use GeminiService to get AI-generated response
            var reply = await _gemini.AskQuestion(userMessage);

            // Respond back to the user
            await turnContext.SendActivityAsync(MessageFactory.Text(reply), cancellationToken);
        }
    }
}
