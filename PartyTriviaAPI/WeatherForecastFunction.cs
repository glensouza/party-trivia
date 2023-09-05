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
using PartyTriviaShared;

namespace PartyTriviaAPI;

public class WeatherForecastFunction
{
    [FunctionName("WeatherForecast")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        Random randomNumber = new Random();
        int temp = 0;

        WeatherForecast[] result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = temp = randomNumber.Next(-20, 55),
            Summary = this.GetSummary(temp)
        }).ToArray();

        return new OkObjectResult(result);
    }

    private string GetSummary(int temp)
    {
        string summary = "Mild";

        if (temp >= 32)
        {
            summary = "Hot";
        }
        else if (temp <= 16 && temp > 0)
        {
            summary = "Cold";
        }
        else if (temp <= 0)
        {
            summary = "Freezing";
        }

        return summary;
    }
}