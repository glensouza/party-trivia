using simple_trivia_game.Models.Game;
using simple_trivia_game.Util;
using System;
using System.Threading;

namespace simple_trivia_game
{
    // Main class for the game to be used for the client
    internal class Game
    {
        private readonly int _numOfQuestions;
        private int _right;
        private readonly string _url = "https://opentdb.com/api.php?amount={0}"; // hardcoded to 10 for now
        private readonly Question[] _questionSet;
        public Game(int Questions)
        {
            _url = string.Format(_url, Questions);
            _numOfQuestions = Questions;
            _right = 0;
            // generate question set
            _questionSet = new Question[_numOfQuestions];
            Models.JSONOutput returnedJSON = NetworkManager.GetJSONOutput(_url);
            int q = 0;
            foreach (Models.Result question in returnedJSON.Results)
            {
                _questionSet[q] =
                    new Question(q + 1, question.Question, question.CorrectAnswer, question.Category, question.IncorrectAnswers);
                q++;
            }
        }

        public int GetNumOfQuestions()
            => _numOfQuestions;

        public void StartGame()
        {
            if (_numOfQuestions > 50 && _numOfQuestions < 0)
            {
                Console.WriteLine("Can't start the game with a question set ranging more than 50 or less than 0... defaulting to 10.");
            }
            // count down from 3 to start
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Starting in {i}...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            for (int i = 0; i < _numOfQuestions; i++)
            {
                if (Display.ShowQuestion(_questionSet[i], _numOfQuestions))
                    _right++;
            }
            Display.ShowResults(_right, _numOfQuestions);
            Console.ReadLine();
        }
    }
}
