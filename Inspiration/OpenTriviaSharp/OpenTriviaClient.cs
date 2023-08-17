using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using OpenTriviaSharp.Exceptions;
using OpenTriviaSharp.Models;

namespace OpenTriviaSharp
{
	/// <summary>
	/// Open Trivia Database client.
	/// </summary>
	public class OpenTriviaClient
	{
		#region Member

		private HttpClient _HttpClient;
		private readonly bool _Supplied;
		private readonly string _BaseApiUrl = "https://opentdb.com/api.php?";
		private readonly string _BaseTokenApiUrl = "https://opentdb.com/api_token.php?";
		private string _SessionToken;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// Create <see cref="OpenTriviaClient"/> instance.
		/// </summary>
		/// <param name="client">
		///		<see cref="HttpClient"/> object to send and receiving response.
		/// </param>
		/// <param name="sessionToken">
		///		Session token of Open Trivia for tracking requested question.
		/// </param>
		public OpenTriviaClient(HttpClient client = null, string sessionToken = null)
		{
			if (client == null)
			{
				this._HttpClient = new HttpClient();
				this._Supplied = false;
			}
			else
			{
				this._HttpClient = client;
				this._Supplied = true;
			}

			this.AddUserAgent();

			if (sessionToken == null || sessionToken.Trim() == "")
			{
				this._SessionToken = "";
			}
			else
			{
				this._SessionToken = sessionToken;
			}
		}

		/// <summary>
		/// Release all resource that this object handle.
		/// </summary>
		~OpenTriviaClient()
		{
			if (this._Supplied == false)
			{
				this._HttpClient.Dispose();
			}
		}

		#endregion Constructor & Destructor

		#region Properties

		/// <summary>
		/// Get current session token.
		/// </summary>
		public string Token
		{
			set
			{
				if (value == null || value.Trim() == "")
				{
					return;
				}

				this._SessionToken = value;
			}
			get
			{
				return this._SessionToken;
			}
		}

		#endregion Properties

		#region Protected Method

		/// <summary>
		///		Get JSON response from url.
		/// </summary>
		/// <typeparam name="T">
		///		The type of the object to deserialize.
		///	</typeparam>
		/// <param name="url">
		///		API URL. 
		/// </param>
		/// <returns>
		///		The instance of <typeparamref name="T"/> being deserialized.
		///	</returns>
		/// <exception cref="OpenTriviaException">
		///		Unexpected error occured.
		/// </exception>
		/// <exception cref="HttpRequestException">
		///		The request failed due to an underlying issue such as network connectivity, DNS
		///     failure, server certificate validation or timeout.
		/// </exception>
		/// <exception cref="JsonException">
		///		The JSON is invalid.
		/// </exception>
		protected async Task<T> GetJsonResponseAsync<T>(string url)
		{
			try
			{
				using (var request = new HttpRequestMessage(HttpMethod.Get, url))
				using (var response = await this._HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
				using (var stream = await response.Content.ReadAsStreamAsync())
				{
					if (response.IsSuccessStatusCode)
					{
						try
						{
							return await JsonSerializer.DeserializeAsync<T>(stream);
						}
						catch (JsonException)
						{
							throw;
						}
					}

					throw new OpenTriviaException(
						$"Unexpected error occured.\n Status code = { (int)response.StatusCode }\n Reason = { response.ReasonPhrase }.");
				}
			}
			catch (HttpRequestException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read JSON data with encoding <see cref="ResponseEncoding.Default"/>.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>
		///		A <see cref="TriviaQuestion"/>.
		/// </returns>
		protected TriviaQuestion ReadTriviaQuestion(JsonElement item)
		{
			var incorrect = new List<string>();

			foreach (var ans in item.GetProperty("incorrect_answers").EnumerateArray())
			{
				incorrect.Add(
					HttpUtility.HtmlDecode(
						ans.GetString()));
			}

			return new TriviaQuestion(
				this.DetermineCategory(item.GetProperty("category").GetString()), 
				this.DetermineType(item.GetProperty("type").GetString()), 
				this.DetermineDifficulty(item.GetProperty("difficulty").GetString()),
				HttpUtility.HtmlDecode(item.GetProperty("question").GetString()),
				HttpUtility.HtmlDecode(item.GetProperty("correct_answer").GetString()),
				incorrect.ToArray()
				);
		}

		/// <summary>
		/// Read JSON data with encoding <see cref="ResponseEncoding.Url3986"/>.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>
		///		A <see cref="TriviaQuestion"/>.
		/// </returns>
		protected TriviaQuestion ReadTriviaQuestionURL(JsonElement item)
		{
			var incorrect = new List<string>();

			foreach (var ans in item.GetProperty("incorrect_answers").EnumerateArray())
			{
				incorrect.Add(
					HttpUtility.UrlDecode(
						ans.GetString()));
			}

			return new TriviaQuestion(
				this.DetermineCategory(HttpUtility.UrlDecode(item.GetProperty("category").GetString())),
				this.DetermineType(item.GetProperty("type").GetString()),
				this.DetermineDifficulty(item.GetProperty("difficulty").GetString()),
				HttpUtility.UrlDecode(item.GetProperty("question").GetString()),
				HttpUtility.UrlDecode(item.GetProperty("correct_answer").GetString()),
				incorrect.ToArray()
				);
		}

		/// <summary>
		/// Read JSON data with encoding <see cref="ResponseEncoding.Base64"/>.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>
		///		A <see cref="TriviaQuestion"/>.
		/// </returns>
		protected TriviaQuestion ReadTriviaQuestionBase64(JsonElement item)
		{
			var incorrect = new List<string>();

			foreach (var ans in item.GetProperty("incorrect_answers").EnumerateArray())
			{
				incorrect.Add(
					this.FromBase64(
						ans.GetString()));
			}

			return new TriviaQuestion(
				this.DetermineCategory(this.FromBase64(item.GetProperty("category").GetString())),
				this.DetermineType(this.FromBase64(item.GetProperty("type").GetString())),
				this.DetermineDifficulty(this.FromBase64(item.GetProperty("difficulty").GetString())),
				this.FromBase64(item.GetProperty("question").GetString()),
				this.FromBase64(item.GetProperty("correct_answer").GetString()),
				incorrect.ToArray()
				);
		}

		/// <summary>
		/// Add browser user agent to <see cref="HttpClient"/>.
		/// </summary>
		protected void AddUserAgent()
		{
			if (this._HttpClient == null)
			{
				return;
			}

			if (this._HttpClient.DefaultRequestHeaders.UserAgent.Count == 0)
			{
				this._HttpClient.DefaultRequestHeaders.Add(
					"User-Agent",
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36");
			}
		}

		#region Helper Method

		/// <summary>
		///		Convert base64 string to UTF-8 string.
		/// </summary>
		/// <param name="base64String">
		///		Base64 string to convert.
		///		</param>
		/// <returns>
		///		UTF-8 string.
		/// </returns>
		protected string FromBase64(string base64String)
		{
			return Encoding.UTF8.GetString(
				Convert.FromBase64String(base64String));
		}

		/// <summary>
		///		Convert JSON reponse category property to <see cref="Category"/>.
		/// </summary>
		/// <param name="category">
		///		Category string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="Category"/> based on paramater <paramref name="category"/>.
		/// </returns>
		protected Category DetermineCategory(string category)
		{
			if (category.Contains("General"))
			{
				return Category.General;
			}
			else if (category.Contains("Books"))
			{
				return Category.Books;
			}
			else if (category.Contains("Film"))
			{
				return Category.Film;
			}
			else if (category.Contains("Music"))
			{
				return Category.Music;
			}
			else if (category.Contains("Theatres"))
			{
				return Category.Theatres;
			}
			else if (category.Contains("Television"))
			{
				return Category.Television;
			}
			else if (category.Contains("Video Games"))
			{
				return Category.VideoGames;
			}
			else if (category.Contains("Board Games"))
			{
				return Category.BoardGames;
			}
			else if (category.Contains("Science"))
			{
				return Category.Science;
			}
			else if (category.Contains("Computers"))
			{
				return Category.Computers;
			}
			else if (category.Contains("Mathematics"))
			{
				return Category.Mathematics;
			}
			else if (category.Contains("Mythology"))
			{
				return Category.Mythology;
			}
			else if (category.Contains("Sports"))
			{
				return Category.Sports;
			}
			else if (category.Contains("Geography"))
			{
				return Category.Geography;
			}
			else if (category.Contains("History"))
			{
				return Category.History;
			}
			else if (category.Contains("Politics"))
			{
				return Category.Politics;
			}
			else if (category.Contains("Art"))
			{
				return Category.Art;
			}
			else if (category.Contains("Celebrities"))
			{
				return Category.Celebrities;
			}
			else if (category.Contains("Animals"))
			{
				return Category.Animals;
			}
			else if (category.Contains("Vehicles"))
			{
				return Category.Vehicles;
			}
			else if (category.Contains("Comics"))
			{
				return Category.Comics;
			}
			else if (category.Contains("Gadgets"))
			{
				return Category.Gadgets;
			}
			else if (category.Contains("Anime") || category.Contains("Manga"))
			{
				return Category.AnimeManga;
			}
			else if (category.Contains("Cartoon"))
			{
				return Category.Cartoon;
			}
			else
			{
				return Category.Any;
			}
		}

		/// <summary>
		///		Convert JSON reponse difficulty property to <see cref="Difficulty"/>.
		/// </summary>
		/// <param name="difficulty">
		///		Difficulty string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="Difficulty"/> based on parameter <paramref name="difficulty"/>.
		/// </returns>
		protected Difficulty DetermineDifficulty(string difficulty)
		{
			switch (difficulty)
			{
				case "hard":
					return Difficulty.Hard;
				case "medium":
					return Difficulty.Medium;
				case "easy":
					return Difficulty.Easy;
				default:
					return Difficulty.Any;
			}
		}

		/// <summary>
		///		Convert JSON reponse type property to <see cref="TriviaType"/>.
		/// </summary>
		/// <param name="type">
		///		Type string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="TriviaType"/> based on parameter <paramref name="type"/>.
		/// </returns>
		protected TriviaType DetermineType(string type)
		{
			switch (type)
			{
				case "multiple":
					return TriviaType.Multiple;
				case "boolean":
					return TriviaType.Boolean;
				default:
					return TriviaType.Any;
			}
		}

		/// <summary>
		///		Get response message based on response code.
		/// </summary>
		/// <param name="responseCode">
		///		Response code in the JSON response.
		/// </param>
		/// <returns>
		///		Message based on response code.
		/// </returns>
		protected string ResponseError(byte responseCode)
		{
			switch (responseCode)
			{
				case 0:
					return "Returned results successfully.";
				case 1:
					return "The API doesn't have enough questions for your query.";
				case 2:
					return "Invalid parameter(s). Arguments passed aren't valid.";
				case 3:
					return "Invalid session token.";
				case 4:
					return "Session token has retrieved all possible questions for the specified query. Reset the token.";
				default:
					return "An error has occured in the API";
			}
		}

		#endregion Helper Method

		#endregion Protected Method

		#region Public Method

		/// <summary>
		///		Retrieves random <see cref="TriviaQuestion"/>.
		/// </summary>
		/// <param name="amount">
		///		 Amount of questions to be retrieved.
		/// </param>
		/// <param name="category">
		///		Category of the questions to be retrieved.
		/// </param>
		/// <param name="type">
		///		Type of the questions to be retrieved.
		/// </param>
		/// <param name="difficulty">
		///		 Difficulty of the questions to be retrieved.
		/// </param>
		/// <param name="encoding">
		///		 Encoding of the API response to be used.
		/// </param>
		/// <param name="sessionToken">
		///		Session token to be used.
		/// </param>
		/// <returns>
		///		Array of <see cref="TriviaQuestion"/> based on specified parameters.
		/// </returns>
		/// <exception cref="OpenTriviaException">
		///		Unexpected error occured.
		/// </exception>
		/// <exception cref="HttpRequestException">
		///		The request failed due to an underlying issue such as network connectivity, DNS
		///     failure, server certificate validation or timeout.
		/// </exception>
		/// <exception cref="JsonException">
		///		The JSON is invalid.
		/// </exception>
		public async Task<TriviaQuestion[]> GetQuestionAsync(byte amount = 10, Category category = Category.Any, TriviaType type = TriviaType.Any, Difficulty difficulty = Difficulty.Any, ResponseEncoding encoding = ResponseEncoding.Default, string sessionToken = "")
		{
			if (amount <= 0)
			{
				amount = 1;
			}
			else if (amount > 50) // hard limit
			{
				amount = 50;
			}

			var url = new StringBuilder(this._BaseApiUrl + $"amount={ amount }");
			
			if (category != Category.Any)
			{
				url.Append($"&category={ (byte)category }");
			}

			if (difficulty != Difficulty.Any)
			{
				url.Append($"&difficulty={ difficulty.ToString().ToLower() }");
			}

			if (type != TriviaType.Any)
			{
				url.Append($"&type={ type.ToString().ToLower() }");
			}

			if (encoding != ResponseEncoding.Default)
			{
				url.Append($"&encode={ encoding.ToString().ToLower() }");
			}

			if (sessionToken != "")
			{
				url.Append($"&token={ sessionToken }");
			}
			else
			{
				if (this._SessionToken != "")
				{
					url.Append($"&token={ this._SessionToken }");
				}
			}

			using (var doc = await this.GetJsonResponseAsync<JsonDocument>(url.ToString()))
			{
				var responseCode = doc.RootElement.GetProperty("response_code").GetByte();

				if (responseCode != 0)
				{
					throw new OpenTriviaException(this.ResponseError(responseCode));
				}

				var jsonArray = doc.RootElement.GetProperty("results");

				var questions = new List<TriviaQuestion>();

				if (encoding == ResponseEncoding.Default)
				{
					foreach (var item in jsonArray.EnumerateArray())
					{
						questions.Add(this.ReadTriviaQuestion(item));
					}
				}
				else if (encoding == ResponseEncoding.Url3986)
				{
					foreach (var item in jsonArray.EnumerateArray())
					{
						questions.Add(this.ReadTriviaQuestionURL(item));
					}
				}
				else
				{
					foreach (var item in jsonArray.EnumerateArray())
					{
						questions.Add(this.ReadTriviaQuestionBase64(item));
					}
				}

				return questions.ToArray();
			}
		}

		/// <summary>
		///		Retrieves a new session token.
		/// </summary>
		/// <returns>
		///		New session token.
		/// </returns>
		public async Task<string> RequestTokenAsync()
		{
			var url = $"{ this._BaseTokenApiUrl }command=request";
			
			using (var doc = await this.GetJsonResponseAsync<JsonDocument>(url))
			{
				var responseCode = doc.RootElement.GetProperty("response_code").GetByte();

				if (responseCode != 0)
				{
					throw new OpenTriviaException(this.ResponseError(responseCode));
				}

				return doc.RootElement.GetProperty("token").GetString();
			}
		}

		/// <summary>
		///		Resets a session token.
		/// </summary>
		/// <param name="sessionToken">
		///		Session token to be reset.
		/// </param>
		/// <returns>
		///		<see langword="true"/> if session token reseted; <see langword="false"/> otherwise.
		/// </returns>
		/// <exception cref="JsonException">
		///		The JSON is invalid.
		/// </exception>
		public async Task<bool> ResetTokenAsync(string sessionToken)
		{
			var url = $"{ this._BaseTokenApiUrl }command=reset&token={ sessionToken }";

			try
			{
				using (var doc = await this.GetJsonResponseAsync<JsonDocument>(url))
				{
					var responseCode = doc.RootElement.GetProperty("response_code").GetByte();

					if (responseCode == 0)
					{
						return true;
					}

					return false;
				}
			}
			catch (JsonException)
			{
				throw new OpenTriviaException(this.ResponseError(3));
			}
		}

		/// <summary>
		///		Retrieves number of question based on <see cref="Category"/>.
		/// </summary>
		/// <param name="category">
		///		Category of question.
		/// </param>
		/// <param name="difficulty">
		///		Difficulty of question.
		/// </param>
		/// <returns>
		///		Number of question.
		/// </returns>
		public async Task<uint> CountQuestionAsync(Category category = Category.General, Difficulty difficulty = Difficulty.Any)
		{
			var url = $"https://opentdb.com/api_count.php?category={ (byte)category }";

			try
			{
				using (var doc = await this.GetJsonResponseAsync<JsonDocument>(url))
				{
					var categoryId = doc.RootElement.GetProperty("category_id").GetByte();

					if ((byte)category != categoryId)
					{
						throw new OpenTriviaException(this.ResponseError(2));
					}

					var result = doc.RootElement.GetProperty("category_question_count");

					switch (difficulty)
					{
						case Difficulty.Easy:
							return result.GetProperty("total_easy_question_count").GetUInt32();
						case Difficulty.Medium:
							return result.GetProperty("total_medium_question_count").GetUInt32();
						case Difficulty.Hard:
							return result.GetProperty("total_hard_question_count").GetUInt32();
						case Difficulty.Any:
						default:
							return result.GetProperty("total_question_count").GetUInt32();
					}
				}
			}
			catch (JsonException)
			{
				throw new OpenTriviaException(this.ResponseError(2));
			}
		}

		/// <summary>
		///		Get category list.
		/// </summary>
		/// <returns>
		///		Array of tuple (id, name) of category.
		/// </returns>
		public async Task<(byte, string)[]> CategoryListAsync()
		{
			var url = "https://opentdb.com/api_category.php";
			
			using (var doc = await this.GetJsonResponseAsync<JsonDocument>(url))
			{
				var category = doc.RootElement.GetProperty("trivia_categories");

				var tuple = new List<(byte, string)>();
				
				foreach (var item in category.EnumerateArray())
				{
					tuple.Add(
						(item.GetProperty("id").GetByte(), 
						item.GetProperty("name").GetString()));
				}

				return tuple.ToArray();
			}
		}

		#endregion Public Method
	}
}
