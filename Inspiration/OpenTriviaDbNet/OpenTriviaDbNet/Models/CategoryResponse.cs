using Newtonsoft.Json;

namespace OpenTriviaDbNet.Models
{
    internal class CategoryResponse
    {
        [JsonProperty("trivia_categories")]
        public TriviaCategory[] TriviaCategories { get; set; }
    }
}