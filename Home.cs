using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsCookies
{
    public static class Home
    {
        [FunctionName("Home")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = @"<!doctype html>
<html>
<head>
  <meta charset='utf-8'>
  <title>Static Web App Cookies</title>
</head>
<body>
  <div>API output:</div>
  <div id=output></div>
  <script type='text/javascript'>
let count = 0;
function callApi() {
  let url = '/api/Test';
  if (++count % 3 == 0) {
    url += '?setcookie=' + count;
  }
  fetch(url)
  .then(response => response.text())
  .then(text => {
    var p = document.createElement('p');
    p.innerText = text;
    document.getElementById('output').appendChild(p);
  });
}

window.setInterval(callApi, 3000);
  </script>
</body>
</html>",
            };
        }
    }
}
