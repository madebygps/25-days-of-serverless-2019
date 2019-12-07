using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;

namespace GwynethPena.Function
{
    public static class day_07_imagesearch
    {
        [FunctionName("day_07_imagesearch")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string subscriptionKey = System.Environment.GetEnvironmentVariable("bingsubkey");
            //stores the image results returned by Bing
            Images imageResults = null;

            var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(subscriptionKey));

            string searchTerm = req.Query["searchTerm"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            searchTerm = searchTerm ?? data?.searchTerm;

            // make the search request to the Bing Image API, and get the results"
            imageResults = client.Images.SearchAsync(query: searchTerm).Result; //search query
            ImageResult imageResult = new ImageResult();

            if (imageResults != null)
            {
                
                var firstImageResult = imageResults.Value.First();
                imageResult.url = firstImageResult.ContentUrl;
            }

            string json = JsonConvert.SerializeObject(imageResult, Formatting.Indented);

            return json != null
                ? (ActionResult)new OkObjectResult($"{json}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
