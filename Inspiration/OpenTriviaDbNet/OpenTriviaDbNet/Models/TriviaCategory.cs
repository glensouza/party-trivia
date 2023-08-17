using Newtonsoft.Json;

namespace OpenTriviaDbNet.Models
{
    public class TriviaCategory
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}