using System;

namespace OpenTriviaSharp.Exceptions
{
	/// <summary>
	/// Open Trivia exception.
	/// </summary>
	public class OpenTriviaException : Exception
	{
		public OpenTriviaException(string msg) : base (msg)
		{

		}

		public OpenTriviaException(string msg, Exception ex) : base(msg, ex)
		{

		}
	}
}
