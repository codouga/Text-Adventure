using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Create();
            
            bool quit = false;
            string? input;

            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                if (input is not null)
                {
                    quit = Parser.Parse(input);
                }                
            } while (!quit);    
        }

    }
}