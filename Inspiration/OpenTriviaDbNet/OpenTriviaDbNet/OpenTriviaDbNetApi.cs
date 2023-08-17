using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenTriviaDbNet.Models;

namespace OpenTriviaDbNet
{
    public class OpenTriviaDbNetApi
    {
        private const string QuestionBaseUrl = "https://opentdb.com/api.php";
        private const string CategoryBaseUrl = "https://opentdb.com/api_category.php";
        private const string TokenBaseUrl = "https://opentdb.com/api_token.php";

        public OpenTriviaDbNetApi()
        {
            
        }

        public string SessionToken { get; private set; }

        public async Task<TriviaCategory[]> GetCategoriesAsync()
        {
            var client = new HttpClient();
            var res = await client.GetAsync(CategoryBaseUrl);
            
            var content = await res.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<CategoryResponse>(content);
            return apiResponse.TriviaCategories;
        }

        public async Task<Question[]> GetQuestionsAsync(int numberOfQuestions, int? category = null, 
            Enums.QuestionType? questionType = null, Enums.Difficulty? difficulty = null)
        {
            if (string.IsNullOrEmpty(SessionToken))
            {
                await CreateSessionTokenAsync();
            }
            
            var client = new HttpClient();
            var query = GetQuestionQueryString(numberOfQuestions, category, questionType, difficulty);
            var url = $"{QuestionBaseUrl}?{query}";
            var res = await client.GetAsync(url);

            var content = await res.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<QuestionResponse>(content);
            return apiResponse.Results;
        }

        public async Task ResetSessionTokenAsync()
        {
            if (string.IsNullOrEmpty(SessionToken))
            {
                await CreateSessionTokenAsync();
                return;
            }
            
            var client = new HttpClient();
            var url = $"{TokenBaseUrl}?command=reset&token={SessionToken}";
            var res = await client.GetAsync(url);
        }

        public async Task CreateSessionTokenAsync()
        {
            var client = new HttpClient();
            var url = $"{TokenBaseUrl}?command=request";
            var res = await client.GetAsync(url);

            var content = await res.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

            SessionToken = apiResponse.Token;
        }

        private string GetQuestionQueryString(int numberOfQuestions, int? category, Enums.QuestionType? questionType, 
            Enums.Difficulty? difficulty)
        {
            var query = new List<string>();
            query.Add($"amount={numberOfQuestions}");

            if (category.HasValue)
            {
                query.Add($"category={category}");
            }

            switch (questionType)
            {
                case Enums.QuestionType.MultiChoice:
                    query.Add("type=multiple");
                    break;
                case Enums.QuestionType.TrueOrFalse:
                    query.Add("type=boolean");
                    break;
            }

            switch (difficulty)
            {
                case Enums.Difficulty.Easy:
                    query.Add("difficulty=easy");
                    break;
                case Enums.Difficulty.Medium:
                    query.Add("difficulty=medium");
                    break;
                case Enums.Difficulty.Hard:
                    query.Add("difficulty=hard");
                    break;
            }
            
            if (!string.IsNullOrEmpty(SessionToken))
            {
                query.Add($"token={SessionToken}");
            }
            
            var queryString = string.Join("&", query);
            return queryString;
        }
    }
}