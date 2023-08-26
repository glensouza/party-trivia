using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PartyTriviaShared.Models;
using PartyTriviaShared.Services;

namespace PartyTriviaAPI
{
    public class OpenTriviaDbTest
    {
        [FunctionName("OpenTriviaDbTest")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            OpenTriviaDbService service = new();

            TriviaCategory[] categories = await service.GetCategoriesAsync();

            Question[] questions1 = await service.GetQuestionsAsync(1, null, null, null);

            Question[] questions2 = await service.GetQuestionsAsync(5, null, OpenTriviaDbEnums.QuestionType.MultiChoice, OpenTriviaDbEnums.Difficulty.Easy);

            await service.CreateSessionTokenAsync();
            string originalToken = service.SessionToken;
            await service.CreateSessionTokenAsync();

            await service.ResetSessionTokenAsync();

            return new OkResult();
        }
    }
}
