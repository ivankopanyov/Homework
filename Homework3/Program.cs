using System;

namespace Homework3
{
    class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            OutputMainMenu();
        }

        /// <summary>
        /// Вывод главного меню.
        /// </summary>
        static void OutputMainMenu()
        {
            Console.WriteLine("================ Домашняя работа №3 ================");
            Console.WriteLine("1 - Вывод элементов двумерного массива по диагонали.");
            Console.WriteLine("2 - Вывод телефонного справочника.");
            Console.WriteLine("3 - Разворот строки.");
            Console.WriteLine("4 - Вывод игрового поля для морского боя.");
            Console.WriteLine();
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("====================================================");
            Console.WriteLine();

            int number;
            Console.Write("Укажите номер пункта меню: ");
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                number = -1;
            }
            Console.WriteLine();

            switch (number)
            {
                case 1:
                    Console.WriteLine("\n-- Вывод элементов двумерного массива по диагонали --\n");
                    Task1();
                    break;
                case 2:
                    Console.WriteLine("\n----------- Вывод телефонного справочника -----------\n");
                    Task2();
                    break;
                case 3:
                    Console.WriteLine("\n------------------ Разворот строки ------------------\n");
                    Task3();
                    break;
                case 4:
                    Console.WriteLine("\n-------- Вывод игрового поля для морского боя -------\n");
                    Task4();
                    break;
                case 0:
                    Console.WriteLine("\nЗавершение работы приложения...\nНажмите любую клавишу");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("\nНекорректный ввод.\nПовторите попытку.\n");
                    OutputMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Вывод подменю.
        /// </summary>
        /// <param name="taskNumber">Номер задачи.</param>
        static void OutputSubMenu(int taskNumber)
        {
            Console.WriteLine();
            Console.WriteLine("=============================");
            Console.WriteLine("1 - Повторить.");
            Console.WriteLine();
            Console.WriteLine("0 - Выход в главное меню.");
            Console.WriteLine("=============================");
            Console.WriteLine();

            int number;
            Console.Write("Укажите номер пункта меню: ");
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                number = -1;
            }
            Console.WriteLine();

            switch (number)
            {
                case 1:
                    RepeatTask(taskNumber);
                    break;
                case 0:
                    OutputMainMenu();
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.\nПовторите попытку.");
                    OutputSubMenu(taskNumber);
                    break;
            }
        }

        /// <summary>
        /// Повтор текущей задачи.
        /// </summary>
        /// <param name="taskNumber">Номер задачи.</param>
        static void RepeatTask(int taskNumber)
        {
            switch (taskNumber)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                default:
                    OutputMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Задача 1. Вывод элементов двумерного массива по диагонали.
        /// </summary>
        static void Task1()
        {
            int length;
            Console.Write("Укажите размерность массива (минимум 2): ");
            if (!int.TryParse(Console.ReadLine(), out length) || length < 2)
            {
                Console.WriteLine("Некорректный ввод.\nПовторите попытку.\n");
                Task1();
                return;
            }

            int[,] array = new int[length, length];
            Random random = new Random();

            Console.WriteLine();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(0, 10);
                    if (i == j || j == length - (i + 1))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{array[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            OutputSubMenu(1);
        }

        /// <summary>
        /// Задача 2. Вывод телефонного справочника.
        /// </summary>
        static void Task2()
        {
            string[,] directory = new string[5, 2] {
                { "Мама",    "+7 (900) 000-00-00 / mom@family.com" },
                { "Папа",    "+7 (900) 111-11-11 / dad@family.com" },
                { "Дедушка", "+7 (900) 222-22-22 / grandma@family.com" },
                { "Бабушка", "+7 (900) 333-33-33 / grandpa@family.com" },
                { "Брат",    "+7 (900) 444-44-44 / brother@family.com" }
            };

            int[] lengths = GetMaxLengths(directory);

            for (int i = 0; i < directory.GetLength(0); i++)
            {
                for (int j = 0; j < directory.GetLength(1); j++)
                {
                    string str = directory[i, j];
                    Console.Write($"{str} ");
                    if (j > 0)
                    {
                        continue;
                    }
                    for (int k = 0; k < lengths[j] - str.Length; k++)
                    {
                        Console.Write(".");
                    }
                    Console.Write("... ");
                }
                Console.WriteLine();
            }
            OutputSubMenu(2);
        }

        /// <summary>
        /// Поиск максимальной длины элемента в каждом измерении двумерного массива строк.
        /// </summary>
        /// <param name="array">Двумерный массив строк для вычисления максимальных длин элементов.</param>
        /// <returns>Массив максимальныч длин элементов в каждом измерении двумерного массива строк.</returns>
        static int[] GetMaxLengths(string[,] array)
        {
            int[] res = new int[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int length = array[i, j].Length;
                    if (length > res[j])
                    {
                        res[j] = length;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Задача 3. Разворот строки.
        /// </summary>
        static void Task3()
        {
            Console.Write("Введите строку для разворота: ");
            string str = Console.ReadLine();

            if (str.Trim() == "")
            {
                Console.WriteLine("Строка не может быть пустой!\nПовторите попытку\n");
                Task3();
                return;
            }

            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine($"Результат: {new string(charArray)}");
            OutputSubMenu(3);
        }

        /// <summary>
        /// Задача 4. Вывод игрового поля для морского боя.
        /// </summary>
        static void Task4()
        {
            SeaBattle.DrawField();
            OutputSubMenu(4);
        }
    }
}
