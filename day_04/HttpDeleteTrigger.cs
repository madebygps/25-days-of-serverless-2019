using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace day_04
{
    public static class HttpDeleteTrigger
    {
        [FunctionName("HttpDeleteTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = @"potluckdishes/delete/{id}")] HttpRequest req,
            [Kevsoft.Azure.WebJobs.MongoDb("potluck", "dishes", "{id}", ReadOnly = true, ConnectionStringSetting = "MongoDbUrl")] PotluckDish dish
            )
        {


            var Client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDbUrl"));
            var DB = Client.GetDatabase("potluck");

            var collection = DB.GetCollection<BsonDocument>("dishes");
            var Deleteone = await collection.DeleteOneAsync(
                            Builders<BsonDocument>.Filter.Eq("_id", dish.Id));

            return new OkObjectResult("Deleted dish");
        }
    }
}
