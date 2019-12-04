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
// using Microsoft.WindowsAzure.Storage;
using System.Net.Http;
using Microsoft.WindowsAzure.Storage;

using Microsoft.WindowsAzure.Storage.Table;

namespace GwynethPena.Function
{
    public static class day_03_25daysofserverless2019
    {

        [FunctionName("day_03_25daysofserverless2019")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // Read json from github webhook.
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // Deserialize json into custom GitHubCommit object
            GitHubCommit commit = JsonConvert.DeserializeObject<GitHubCommit>(requestBody);
            // Here will we save images that ARE png.
            HttpClient client = new HttpClient();
            String containedPng = "No PNGs in commit";

            // Go through each added file from commit and compare if it contains .png in file name. If so, added to list of saved imaged.
            foreach (string addedFileName in commit.head_commit.added)
            {
                if (addedFileName.Contains(".png"))
                {
                    containedPng = "PNGs in commit";
                    //PngEntity pngEntity = new PngEntity();
                    string formattedUrl = formatUrl(addedFileName);
                    PngEntity _pngentity = new PngEntity(addedFileName, addedFileName);

                    _pngentity.URL_VC = formattedUrl;
                    _pngentity.Name_VC = addedFileName;

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"));

                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                    CloudTable table = tableClient.GetTableReference("day032019");
                    await table.CreateIfNotExistsAsync();
                    TableOperation tableOperation = TableOperation.Insert(_pngentity);
                    await table.ExecuteAsync(tableOperation);

                }
            }

            return containedPng != null
                ? (ActionResult)new OkObjectResult($"{containedPng}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public static string formatUrl(String filename)
        {
            string githuburl = "https://raw.githubusercontent.com/madebygps/furryfriendspics/master/";
            return githuburl + filename;
        }
    }

    public class PngOfPet
    {
        public string url { get; set; }
    }
    public class HeadCommit
    {
        public List<string> added { get; set; }
    }
    public class GitHubCommit
    {
        public HeadCommit head_commit { get; set; }
    }
    public class PngEntity : TableEntity
    {
        public PngEntity(string key, string row)
        {
            this.PartitionKey = key;
            this.RowKey = row;
        }

        public PngEntity() { }

        public string URL_VC { get; set; }
        public string Name_VC { get; set; }

    }
}
