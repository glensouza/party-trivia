using Newtonsoft.Json;

namespace OpenTriviaDbNet.Models
{
    internal class QuestionResponse
    {
        [JsonProperty("response_code")]
        public long ResponseCode { get; set; }

        [JsonProperty("results")]
        public Question[] Results { get; set; }
    }
}
