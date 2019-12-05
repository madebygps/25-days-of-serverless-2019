using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using System.Net.Http;
using System.Text;

namespace GwynethPena.Function
{

    public static class day_05_25daysofserverless2019
    {
        private const string text_key_var = "TEXT_ANALYTICS_SUBSCRIPTION_KEY";
        private static readonly string text_key = Environment.GetEnvironmentVariable(text_key_var);
        private const string text_endpoint_var = "TEXT_ANALYTICS_ENDPOINT";
        private static readonly string text_endpoint = Environment.GetEnvironmentVariable(text_endpoint_var);

        [FunctionName("day_05_25daysofserverless2019")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var text_client = authenticateClient();

            sentimentAnalysisExample(text_client);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<Letter> letters = JsonConvert.DeserializeObject<List<Letter>>(requestBody);
            foreach (var letter in letters)
            {


            }

            string result = "nice";
            return new OkObjectResult($"Hello, {result}");
        }

        static TextAnalyticsClient authenticateClient()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(text_key);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = text_endpoint
            };
            return client;
        }
        static double sentimentAnalysisExample(ITextAnalyticsClient client, string text)
        {
            var result = client.Sentiment("I had the best day of my life.", "en");
            double score = (double)result.Score;
            return score;
            //Console.WriteLine($"Sentiment Score: {result.Score:0.00}");
        }
    }
}
