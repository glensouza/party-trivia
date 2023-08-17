namespace simple_trivia_game.Models.Game
{
    internal class Question
    {
        private readonly string _question, _correctAnswer, _category;
        private readonly string[] _incorrectAnswers;
        private bool _wasCorrect;
        private readonly int _questionNumber; // from 1 to 10
        public Question(int QuestionNumber, string Question, string CorrectAnswer, string Category, string[] IncorrectAnswers)
        {
            _question = Question;
            _questionNumber = QuestionNumber;
            _correctAnswer = CorrectAnswer;
            _category = Category;
            _incorrectAnswers = IncorrectAnswers;
        }
        public int GetQuestionNumber()
        {
            return _questionNumber;
        }

        public string GetQuestion()
        {
            return _question;
        }

        public string GetCorrectAnswer()
        {
            return _correctAnswer;
        }

        public string[] GetIncorrectAnswers()
        {
            return _incorrectAnswers;
        }

        public string GetCategory()
        {
            return _category;
        }

        public void SetAsCorrect(bool Correct)
        {
            _wasCorrect = Correct;
        }
    }
}
