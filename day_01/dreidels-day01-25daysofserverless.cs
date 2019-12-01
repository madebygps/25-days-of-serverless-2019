using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;
using System.Net.Http;


namespace GwynethPena.Function
{
    public static class dreidels_day01_25daysofserverless
    {
        [FunctionName("dreidels_day01_25daysofserverless")]
        public static String Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)

        {

            // Names of all sides of dreidel.
            string[] dreidel_side_names = new string[] { "ה hay","נ nun", "ג gimmel", "ש shin" };
            // let's generate a random number.
            Random rnd = new Random();
            // Generates a number between 0 and 3
            int dreidel_side = rnd.Next(0, 4); 

            string random_dreidel_side = dreidel_side_names[dreidel_side];

            // Format the answer into json.
            string json = string.Format("{{ \"side\": \"{0}\" }}", random_dreidel_side);

            return json;

        }
    }
}