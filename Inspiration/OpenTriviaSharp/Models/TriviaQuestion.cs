using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace OpenTriviaSharp.Models
{
	/// <summary>
	/// Trivia question.
	/// </summary>
	public readonly struct TriviaQuestion
	{
		#region Constructor & Destructor

		/// <summary>
		///		Create a <see cref="TriviaQuestion"/>.
		/// </summary>
		/// <param name="category">
		///		Question category.
		/// </param>
		/// <param name="type">
		///		Question type.
		/// </param>
		/// <param name="difficulty">
		///		Question difficulty.
		/// </param>
		/// <param name="question">
		///		Question.
		/// </param>
		/// <param name="correctAnswer">
		///		Question correct answer.
		/// </param>
		/// <param name="incorrectAnswers">
		///		Question incorrect answers.
		/// </param>
		public TriviaQuestion(
			Category category, 
			TriviaType type, 
			Difficulty difficulty, 
			string question, 
			string correctAnswer, 
			string[] incorrectAnswers)
		{
			this.Category = category;
			this.Type = type;
			this.Difficulty = difficulty;
			this.Question = question;
			this.CorrectAnswer = correctAnswer;
			this.IncorrectAnswers = new ReadOnlyCollection<string>(incorrectAnswers);
		}

		#endregion Constructor & Destructor

		#region Public Properties

		/// <summary>
		/// Get the question <see cref="Category"/>.
		/// </summary>
		public Category Category { get; }
		
		/// <summary>
		/// Get the question <see cref="TriviaType"/>.
		/// </summary>
		public TriviaType Type { get; }

		/// <summary>
		/// Get the question <see cref="Models.Difficulty"/>.
		/// </summary>
		public Difficulty Difficulty { get; }

		/// <summary>
		/// Get the question.
		/// </summary>
		[JsonPropertyName("question")]
		public string Question { get; }

		/// <summary>
		/// Get the question correct answer.
		/// </summary>
		[JsonPropertyName("correct_answer")]
		public string CorrectAnswer { get; }

		/// <summary>
		/// Get the question incorrect answers.
		/// </summary>
		public ReadOnlyCollection<string> IncorrectAnswers { get; }

		#endregion Public Properties
	}
}
