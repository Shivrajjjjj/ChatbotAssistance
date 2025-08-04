using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Logging;

public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
{
    public AdapterWithErrorHandler(ILogger<BotFrameworkHttpAdapter> logger)
        : base()
    {
        OnTurnError = async (turnContext, exception) =>
        {
            logger.LogError(exception, "Unhandled error");
            await turnContext.SendActivityAsync("Oops! Something went wrong.");
        };
    }
}
