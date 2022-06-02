using System;

namespace Homework7
{
    class Program
    {
        static int total = 20;
        static int min = 1;
        static int max = 5;
        static bool isPlayer = false;

        static Random random = new Random();

        static void Main(string[] args)
        {
            while (total > 0)
            {
                isPlayer = !isPlayer;
                Console.WriteLine("Current total: " + total);
                int number = 0;
                if (isPlayer)
                {
                    Console.Write("Input number from " + min + " to " + max + ": ");
                    while (number < min || number > max)
                    {
                        var input = Console.ReadKey(true);
                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                number = 1;
                                break;
                            case ConsoleKey.D2:
                                number = 2;
                                break;
                            case ConsoleKey.D3:
                                number = 3;
                                break;
                            case ConsoleKey.D4:
                                number = 4;
                                break;
                            case ConsoleKey.D5:
                                number = int.Parse(input.KeyChar.ToString());
                                break;
                        }
                    }
                    Console.WriteLine(number);
                }
                else
                {
                    number = total <= max ? total : random.Next(min, max + 1);
                    Console.WriteLine("Computer inputed number " + number);
                }

                total -= number;
            }

            Console.WriteLine(isPlayer ? "You win!" : "Computer win!");
            Console.ReadKey(true);
        }
    }
}
