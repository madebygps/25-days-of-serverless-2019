using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Bson;
using Kevsoft.Azure.WebJobs;

// https://kevsoft.net/2019/02/24/using-mongodb-and-azure-functions.html

namespace GwynethPena.Function
{
    public static class day_04_25daysofserverless2019
    {
        [FunctionName("day_04_25daysofserverless2019")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "companies")] Company company, HttpRequest req,
            [Kevsoft.Azure.WebJobs.MongoDb("test", "companies", ConnectionStringSetting = "MongoDbUrl")] IAsyncCollector<Company> companies,
            ILogger log)
        {
            await companies.AddAsync(company);

            return new OkObjectResult($"Created company '{company.Name}' with an id of '{company.Id}'");
        }
    }

    public class Company
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string CompanyNumber { get; set; }
    }






}
