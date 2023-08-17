using simple_trivia_game.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simple_trivia_game.Util
{
    internal static class Display
    {
        private static readonly Random _rand = new Random();
        public static bool ShowQuestion(Question question, int TotalQuestions)
        {
            string[] choices = new string[question.GetIncorrectAnswers().Length + 1]; // there are typically 4 answers per the JSON output, True or False is 2
            int correctAnswerIndex = _rand.Next(0, choices.Length - 1);
            Queue<string> incorrectAnswers = new Queue<string>();
            Array.ForEach(question.GetIncorrectAnswers(), q => incorrectAnswers.Enqueue(q));
            // populate choices
            for (int i = 0; i < choices.Length; i++)
            {
                if (i == correctAnswerIndex)
                {
                    choices[i] = question.GetCorrectAnswer();
                }
                else
                {
                    choices[i] = incorrectAnswers.Dequeue();
                }
            }
            PrintQuestion(question, choices, TotalQuestions);
            char answer = Console.ReadKey()
                    .KeyChar;
            while (!IsValidChar(answer, question.GetIncorrectAnswers().Count() == 1))
            {
                PrintQuestion(question, choices, TotalQuestions);
                answer = Console.ReadKey()
                    .KeyChar;
            }
            bool correct = ConvertAnswerToIndex(answer) == correctAnswerIndex;
            question.SetAsCorrect(correct);
            return correct;
        }
        private static void PrintQuestion(Question question, string[] choices, int questionCount)
        {
            Console.Clear();
            Console.WriteLine("=======================================/");
            Console.WriteLine("Question {0}/{1} ({2}):", question.GetQuestionNumber(), questionCount, question.GetCategory());
            Console.WriteLine(question.GetQuestion());
            if (question.GetIncorrectAnswers().Length > 1)
            {
                Console.WriteLine("Was it...");
                Console.WriteLine("A.) {0}\nB.) {1}\nC.) {2}\nD.) {3}", choices);
            }
            else
            {
                Console.WriteLine("A.) True");
                Console.WriteLine("B.) False");
            }
            Console.WriteLine("=======================================/");
        }
        private static bool IsValidChar(char ch, bool trueFalse)
        {
            char[] validChar = new char[]
            {
                'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd'
            };
            char[] validCharTrueFalse = new char[]
            {
                'A', 'a', 'B', 'b'
            };
            if (trueFalse)
            {
                return validCharTrueFalse.Where(c => c == ch).Count() == 1;
            }
            else
            {
                return validChar.Where(c => c == ch).Count() == 1;
            }
        }
        private static int ConvertAnswerToIndex(char answer)
        {
            answer = answer
                .ToString()
                .ToUpper()[0];
            int index = -1; // -1 default if no switch case is met
            switch (answer)
            {
                case 'A': index = 0; break;
                case 'B': index = 1; break;
                case 'C': index = 2; break;
                case 'D': index = 3; break;
            }
            return index;
        }
        public static void ShowResults(int CorrectQuestions, int TotalQuestions)
        {
            Console.Clear();
            int percentage = (int)Math.Ceiling(CorrectQuestions / (double)TotalQuestions * 100.0);
            string questionPlural = TotalQuestions == 1 ? "question" : "questions";
            Console.WriteLine("=======================================/");
            Console.WriteLine("Take a deep breath, it's over.");
            Console.WriteLine("You got {0} out of {1} {2} correct ({3}%).", CorrectQuestions, TotalQuestions, questionPlural, percentage);
            Console.WriteLine("=======================================/");
            Console.Write("Play again? (Y/N): ");
            if (Console.ReadLine().ToLower() == "n")
            {
                Environment.Exit(0);
            }
            else
            {
                Program.game = new Game(10);
                Program.game.StartGame();
            }
        }
    }
}
