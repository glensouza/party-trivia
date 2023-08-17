using Newtonsoft.Json;

namespace simple_trivia_game.Models
{
    internal class JSONOutput
    {
        [JsonProperty("response_code")]
        public int? response_code { get; set; }
        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
}
