using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomProvider.Action
{
    public class argSample
    {
        private readonly ILogger<argSample> _logger;

        public argSample(ILogger<argSample> logger)
        {
            _logger = logger;
        }

        [Function("argSample")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}/providers/Microsoft.CustomProviders/resourceproviders/{minirpname}/{action}")] HttpRequest req,
        string subscriptionId,
        string resourceGroupName,
        string minirpname,
        string action)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            _logger.LogInformation($"The Custom Provider Function 'HttpTrigger2' received a request '{req.Method}'.");
            _logger.LogInformation($"The miniRpName was: '{minirpname}'");
            _logger.LogInformation($"The action was: '{action}'");
            _logger.LogInformation($"The HTTP Method was: '{req.Method}'");
            

            if (action == "ping")
            {
                if (req.Method != HttpMethod.Post.Method)
                {
                    return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
                }
                else
                {
                    var host = req.Headers["Host"].FirstOrDefault() ?? "anonymous";
                     _logger.LogInformation($"The host was: '{host}'");
                    var content = $"{{ 'pingcontent' : {{ 'source' : '{host}' }} , 'message' : 'hello {host}'}}";
                    return new OkObjectResult(content);
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);

        }
    }
}
