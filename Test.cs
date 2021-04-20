using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsCookies
{
    public static class Test
    {
        [FunctionName("Test")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var cookie = req.Cookies["test"];
            var message = $"Request Cookie = {cookie}.";

            var setcookie = req.Query["setcookie"];
            if (setcookie.Count != 0)
            {
                req.HttpContext.Response.Cookies.Append("test", setcookie[0]);
                message += $" Setting cookie to {setcookie[0]}.";
            }

            req.HttpContext.Response.Headers["Cache-Control"] = "no-store, max-age=0";
            return new OkObjectResult(message);
        }
    }
}
