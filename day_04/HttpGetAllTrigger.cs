using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;


namespace day_04
{
    public static class HttpGetAllTrigger
    {
        [FunctionName("HttpGetAllTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = @"potluckdishes/listall")] HttpRequest req
           )
        {

            var Client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDbUrl"));
            var DB = Client.GetDatabase("potluck");

            var collection = DB.GetCollection<Dish>("dishes");

            var documents = await collection.Find(_ => true).ToListAsync();

            string json = JsonConvert.SerializeObject(documents, Formatting.Indented);

            return new OkObjectResult($"{json}");
        }
    }


    public class Dish
    {

        [BsonId]
        [BsonRepresentation(BsonType.String)] public MongoDB.Bson.ObjectId Id { get; set; }

        public string Name { get; set; }

        public string FriendName { get; set; }
    }
}
