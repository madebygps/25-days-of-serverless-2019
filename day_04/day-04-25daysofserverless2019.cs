using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;



namespace GwynethPena.Function
{
    public static class day_04_25daysofserverless2019
    {
        [FunctionName("day_04_25daysofserverless2019")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "potluckdishes")] PotluckDish dish, HttpRequest req,
            [Kevsoft.Azure.WebJobs.MongoDb("potluck", "dishes", ConnectionStringSetting = "MongoDbUrl")] IAsyncCollector<PotluckDish> dishes,
            ILogger log)
        {
            await dishes.AddAsync(dish);
        
            return new OkObjectResult($"Created dish '{dish.Name}' with an id of '{dish.Id}' will be brought by'{dish.FriendName}', please make note of ID so you can delete dish if needed later");
        }
    }
}