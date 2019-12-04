using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace day_04
{
    public static class HttpGetTrigger
    {
        [FunctionName("HttpGetTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = @"companies/{id}")] HttpRequest req,
            [Kevsoft.Azure.WebJobs.MongoDb("test", "companies", "{id}", ReadOnly = true, ConnectionStringSetting = "MongoDbUrl")] Company company
            )
        {
            return new OkObjectResult(company);
        }
    }
}
