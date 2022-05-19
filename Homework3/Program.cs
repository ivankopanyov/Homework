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
            while (true)
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
                        Console.WriteLine("\nНекорректный ввод. Повторите попытку.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод подменю.
        /// </summary>
        /// <returns>Возвращает true, если пользователь решил повторить выполнение задачи. false - выход в главное меню.</returns>
        static bool OutputSubMenu()
        {
            while (true)
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
                if (int.TryParse(Console.ReadLine(), out number) && (number == 0 || number == 1))
                {
                    return number == 1;
                }
                Console.WriteLine("Некорректный ввод. Повторите попытку.\n");
            }
        }

        /// <summary>
        /// Задача 1. Вывод элементов двумерного массива по диагонали.
        /// </summary>
        static void Task1()
        {
            bool f = true;

            while (f)
            {
                Console.WriteLine();
                Console.WriteLine("============================================================");
                Console.WriteLine("1 - Вывод элементов двумерного массива, идущих по диагонали.");
                Console.WriteLine("2 - Вывод всех элементов двумерного массива по диагонали.");
                Console.WriteLine();
                Console.WriteLine("0 - Выход в главное меню.");
                Console.WriteLine("============================================================");
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
                        Task1_1(CreateArray());
                        f = OutputSubMenu();
                        break;
                    case 2:
                        Task1_2(CreateArray());
                        f = OutputSubMenu();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите попытку.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Инициализация двумерного массива.
        /// </summary>
        /// <returns>Двумерный массив с заданными пользователем измерениями.</returns>
        static int[,] CreateArray()
        {
            int[] lengths = new int[2];

            for (int i = 0; i < lengths.Length; i++)
            {
                Console.Write($"Укажите размерность {(i == 0 ? "первого" : "второго")} измерения массива (минимум 2): ");
                if (!int.TryParse(Console.ReadLine(), out lengths[i]) || lengths[i] < 2)
                {
                    Console.WriteLine("Некорректный ввод. Повторите попытку.\n");
                    i--;
                }
            }

            Console.WriteLine();

            return new int[lengths[0], lengths[1]];
        }

        /// <summary>
        /// Задача 1. Реализация 1. Вывод элементов двумерного массива, идущих по диагонали.
        /// </summary>
        /// <param name="array">Двумерный массив для заполнения и вывода.</param>
        static void Task1_1(int[,] array)
        {
            string diagonals1 = "Диагональ 1:", diagonals2 = "Диагональ 2:";
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(0, 10);
                    if (i == j || j == array.GetLength(1) - (i + 1))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (i == j)
                    {
                        diagonals1 += $" {array[i, j]}";
                    }

                    if (j == array.GetLength(1) - (i + 1))
                    {
                        diagonals2 += $" {array[i, j]}";
                    }

                    Console.Write($"{array[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\n{diagonals1}\n{diagonals2}");
        }

        /// <summary>
        /// Задача 1. Реализация 2. Вывод всех элементов двумерного массива по диагонали.
        /// </summary>
        /// <param name="array">Двумерный массив для заполнения и вывода.</param>
        static void Task1_2(int[,] array)
        {
            string offset = "";
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(0, 10);
                    Console.WriteLine($"{offset}{array[i, j]}");
                    offset += "  ";
                }
            }
        }

        /// <summary>
        /// Задача 2. Вывод телефонного справочника.
        /// </summary>
        static void Task2()
        {
            do
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
            }
            while (OutputSubMenu());
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
            do
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
            }
            while (OutputSubMenu());
        }

        /// <summary>
        /// Задача 4. Вывод игрового поля для морского боя.
        /// </summary>
        static void Task4()
        {
            do
            {
                SeaBattle.DrawField();
            }
            while (OutputSubMenu());
        }
    }
}
