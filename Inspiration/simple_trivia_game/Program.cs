using System;

namespace simple_trivia_game
{
    internal class Program
    {
        public static Game game;

        private static void Main(string[] args)
        {
            bool canContinue = false;
            while (!canContinue)
            {
                Console.Write("Welcome to simple_trivia_game! How many questions?: ");
                var input = Console.ReadLine();
                int questions;
                if (int.TryParse(input, out questions))
                {
                    if (questions <= 50 && questions > 0)
                    {
                        canContinue = true;
                        game = new Game(questions);
                        game.StartGame();
                    }
                    else
                        Console.WriteLine("Questions must be no more than 50, and at least 1 or greater.");
                }
            }            
        }
    }
}
