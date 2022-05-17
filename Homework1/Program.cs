using System;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Просим пользователя ввести имя и сохраняем результат в переменную
            Console.Write("Введите ваше имя: ");
            string userName = Console.ReadLine();

            //Выводим в консоль сообщение с именем пользователя и текущей датой
            Console.WriteLine($"Привет, {userName}, сегодня {DateTime.Now}");

            //Выходим из приложения по нажатию клавиши
            Console.ReadKey();
        }
    }
}
