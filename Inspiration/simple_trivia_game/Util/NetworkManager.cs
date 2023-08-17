using Newtonsoft.Json;
using simple_trivia_game.Models;
using System;
using System.Net;

namespace simple_trivia_game.Util
{
    /*
     * Class used to make requests to fetch JSON and deserialize
     */
    internal static class NetworkManager
    {
        public static JSONOutput GetJSONOutput(string URL)
        {
            JSONOutput deserializedJSON;
            try
            {
                using WebClient _wc = new WebClient();
                string jsonOutput = _wc.DownloadString(URL);
                deserializedJSON = JsonConvert.DeserializeObject<JSONOutput>(jsonOutput);
            }
            catch (WebException wEx)
            {
                Console.WriteLine("An error occurred reaching {0}... Reason: {1}", URL, wEx.Message);
                return null; // return null if we don't get a proper JSON output for whatever reason
            }
            return deserializedJSON;
        }
    }
}
