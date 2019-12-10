using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;
using Newtonsoft.Json;

namespace GwynethPena.Function
{
    public static class githubThankYou
    {
        [FunctionName("githubThankYou")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string action = req.Query["action"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GithubIssue githubIssue = JsonConvert.DeserializeObject<GithubIssue>(requestBody);
            if (githubIssue.action.Equals("opened"))
            {
                var client = new GitHubClient(new ProductHeaderValue("25-days-of-serverless-gps"));
                string githubun = System.Environment.GetEnvironmentVariable("githubun");
                string githubpw = System.Environment.GetEnvironmentVariable("githubpw");
                var basicAuth = new Credentials(githubun, githubpw); // NOTE: not real credentials
                client.Credentials = basicAuth;


                var response = await client.Issue.Comment.Create(githubIssue.repository.id, githubIssue.issue.number, $@"Hello @{@githubIssue.issue.user.login} Thanks for submitting this issue, Happy Holidays!");
                if (response is null)
                {
                    return new OkObjectResult($"Error");
                }


            }

            return new OkObjectResult($"Success");
        }
    }
}
