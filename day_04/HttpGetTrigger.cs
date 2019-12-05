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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = @"potluckdishes/{id}")] HttpRequest req,
            [Kevsoft.Azure.WebJobs.MongoDb("potluck", "dishes", "{id}", ReadOnly = true, ConnectionStringSetting = "MongoDbUrl")] PotluckDish dish
            )
        {
            return new OkObjectResult(dish);
        }
    }
}
