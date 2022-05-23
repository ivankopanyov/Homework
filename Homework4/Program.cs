using System;
using System.Text.RegularExpressions;

namespace Homework4
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
            string message = string.Empty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("================ Домашняя работа №4 ================");
                Console.WriteLine("1 - Вывод списка с ФИО.");
                Console.WriteLine("2 - Вычисление суммы введенных чисел.");
                Console.WriteLine("3 - Определение времени года по порядковому номеру месяца.");
                Console.WriteLine("4 - Определение числа из ряда Фибоначчи.");
                Console.WriteLine();
                Console.WriteLine("0 - Выход.");
                Console.WriteLine("====================================================");
                Console.WriteLine();

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }

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
                    case 0:
                        Console.WriteLine("\nЗавершение работы приложения...\nНажмите любую клавишу");
                        Console.ReadKey();
                        return;
                    default:
                        message = "Некорректный ввод. Повторите попытку.";
                        break;
                }
            }
        }

        // ----------------------------------- Задача 1. -----------------------------------

        /// <summary>
        /// Задача 1. Вывод списка с ФИО.
        /// </summary>
        static void Task1()
        {
            string message = string.Empty;

            while (true)
            {
                OutputPersonsList();

                Console.WriteLine("=========================");
                Console.WriteLine("1 - Добавить.");
                if (persons != null && persons.Length > 0)
                    Console.WriteLine("2 - Удалить.");
                Console.WriteLine();
                Console.WriteLine("0 - Выход в главное меню.");
                Console.WriteLine("=========================");
                Console.WriteLine();

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }

                int maxInputNumber = persons != null && persons.Length > 0 ? 2 : 1;

                int number;
                Console.Write("Укажите номер пункта меню: ");
                if (!int.TryParse(Console.ReadLine(), out number) || number < 0 || number > maxInputNumber)
                    number = -1;

                switch (number)
                {
                    case 1:
                        AddPerson();
                        break;
                    case 2:
                        RemovePerson();
                        break;
                    case 0:
                        return;
                    default:
                        message = "Некорректный ввод. Повторите попытку.";
                        break;
                }
            }
        }

        /// <summary>
        /// Массив c введенными ФИО.
        /// </summary>
        static (string FirstName, string LastName, string Patronymic)[] persons;

        /// <summary>
        /// Вывод списка ФИО в консоль.
        /// </summary>
        static void OutputPersonsList()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Задача 1. Вывод списка ФИО. --------------------\n");
            Console.WriteLine("Текущий список:\n");

            if (persons == null || persons.Length == 0)
                Console.WriteLine("\tСписок пуст.");
            else
            {
                int maxOffset = persons.Length.ToString().Length;

                for (int i = 0; i < persons.Length; i++)
                {
                    string offset = new string(' ', maxOffset - (i + 1).ToString().Length);
                    Console.WriteLine($"\t{offset}{i + 1}. {GetFullName(persons[i].FirstName, persons[i].LastName, persons[i].Patronymic)}");
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Добавление нового ФИО в список.
        /// </summary>
        static void AddPerson()
        {
            string errorMsg = "Некорректный ввод. Повторите попытку.\n";
            int maxLength = 100;

            string lastName = null;
            do
            {
                OutputPersonsList();

                if (lastName != null)
                    Console.WriteLine(errorMsg);

                Console.WriteLine("* Введите 0 для отмены. *");
                Console.WriteLine("* Допускаются только буквенные символы и дефис. *");
                Console.WriteLine($"* Максимально длина {maxLength}. *\n");

                Console.Write("Укажите фамилию: ");
                lastName = Console.ReadLine();

                if (lastName == "0")
                    return;
            }
            while (!TryDisplayLastName(ref lastName, maxLength));

            string firstName = null;
            do
            {
                OutputPersonsList();

                if (firstName != null)
                    Console.WriteLine(errorMsg);

                Console.WriteLine("* Введите 0 для отмены. *");
                Console.WriteLine("* Допускаются только буквенные символы. *");
                Console.WriteLine($"* Максимально длина {maxLength}. *\n");

                Console.Write("Укажите имя: ");
                firstName = Console.ReadLine();

                if (firstName == "0")
                    return;
            }
            while (!TryDisplayFirstName(ref firstName, maxLength));

            string patronymic = null;
            do
            {
                OutputPersonsList();

                if (patronymic != null)
                    Console.WriteLine(errorMsg);

                Console.WriteLine("* Введите 0 для отмены. *");
                Console.WriteLine("* Допускаются только буквенные символы. *");
                Console.WriteLine($"* Максимально длина {maxLength}. *");
                Console.WriteLine("* Необязательное поле. *\n");

                Console.Write("Укажите отчество: ");
                patronymic = Console.ReadLine().Trim();

                if (patronymic == "0")
                    return;

                patronymic = patronymic.Trim();
            }
            while (patronymic != string.Empty && !TryDisplayFirstName(ref patronymic, maxLength));

            Push(ref persons, (firstName, lastName, patronymic));

            Console.WriteLine();
        }

        /// <summary>
        /// Удаление ФИО из списка.
        /// </summary>
        static void RemovePerson()
        {
            string message = string.Empty;

            while (true)
            {
                OutputPersonsList();

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                }

                Console.WriteLine("* Введите 0 для отмены. *\n");

                int number;
                Console.Write("Укажите номер пункта списка для удаления: ");
                if (!int.TryParse(Console.ReadLine(), out number) || number < 0 || number > persons.Length)
                    number = -1;

                if (number == 0)
                    return;

                if (number == -1)
                {
                    message = "Некорректный ввод. Повторите попытку.";
                    continue;
                }

                Remove(ref persons, number - 1);
                return;

            }
        }

        /// <summary>
        /// Проверка имени на недопустимые символы и максимальную длину.
        /// Попытка преобразовать строку в формат "Имя".
        /// </summary>
        /// <param name="firstName">Строка с именем. При прохождении проверки преобразуется в формат "Имя".</param>
        /// <param name="maxLength">Максимально допустимая длина имени.</param>
        /// <returns>Возвращает true, если имя была преобразовано.</returns>
        static bool TryDisplayFirstName(ref string firstName, int maxLength = int.MaxValue)
        {
            var temp = firstName.Trim();

            if (temp == string.Empty || temp.Length > maxLength)
                return false;

            foreach (char symbol in temp)
            {
                if (!char.IsLetter(symbol))
                    return false;
            }

            firstName = char.ToUpper(temp[0]) + temp.Remove(0, 1).ToLower();
            return true;
        }

        /// <summary>
        /// Проверка фамилии на недопустимые символы и максимальную длину с учетом двойной фамилии.
        /// Попытка преобразовать строку в формат "Фамилия" или "Фамилия-Фамилия".
        /// </summary>
        /// <param name="lastName">Строка с фамилией. При прохождении проверки преобразуется в формат "Фамилия" или "Фамилия-Фамилия".</param>
        /// <param name="maxLength">Максимально допустимая длина фамилии.</param>
        /// <returns>Возвращает true, если строка фамилия преобразована.</returns>
        static bool TryDisplayLastName(ref string lastName, int maxLength = int.MaxValue)
        {
            var temp = lastName.Trim();

            if (temp == string.Empty)
                return false;

            var array = temp.Split("-", StringSplitOptions.RemoveEmptyEntries);

            if (array.Length < 1 || array.Length > 2)
                return false;

            var result = string.Empty;

            for (int i = 0; i < array.Length; i++)
            {
                if (!TryDisplayFirstName(ref array[i]))
                    return false;

                string separator = i > 0 ? "-" : string.Empty;

                result += $"{separator}{array[i]}";
            }

            if (result.Length > maxLength)
                return false;

            lastName = result;
            return true;
        }

        /// <summary>
        /// Добавление нового элемента в конец массива.
        /// </summary>
        /// <param name="array">Массив для добавления нового элемента.</param>
        /// <param name="element">Новый элемент для добавления в массив.</param>
        static void Push(ref (string, string, string)[] array, (string, string, string) element)
        {
            if (array == null)
                array = new (string, string, string)[0];

            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        /// <summary>
        /// Удаление элемента из массива по переданному индексу.
        /// </summary>
        /// <param name="array">Массив для удаления элемента.</param>
        /// <param name="index">Индекс элемента массива для удаления.</param>
        static void Remove(ref (string, string, string)[] array, int index)
        {
            if (array == null || array.Length == 0 || index < 0 || index >= array.Length)
                return;

            (string, string, string)[] newArray = new (string, string, string)[array.Length - 1];

            Array.Copy(array, newArray, index);
            Array.Copy(array, index + 1, newArray, index, newArray.Length - index);

            array = newArray;
        }

        /// <summary>
        /// Склейка имени, фамилии и отчества в одну строку.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="patronymic">Отчество.</param>
        /// <returns>Строка с именем, фамилией и отчеством</returns>
        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            return $"{lastName.Trim()} {firstName.Trim()} {patronymic.Trim()}";
        }

        // ----------------------------------- Задача 2. -----------------------------------

        /// <summary>
        /// Задача 2. Вычисление суммы введенных чисел.
        /// </summary>
        static void Task2()
        {
            string message = string.Empty;
            string lastInput = string.Empty;
            double sum = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("------------ Задача 2. Вычисление суммы введенных чисел. ------------\n");

                if (lastInput != string.Empty)
                {
                    Console.WriteLine($"Вы ввели:\n\n{lastInput}\n");
                    Console.WriteLine($"Сумма введенных вами чисел: {sum}\n");
                }

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");

                Console.WriteLine("Введите числа для вычисления суммы через пробел:\n");

                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "в")
                    return;

                string[] inputNumbers = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (inputNumbers.Length == 0)
                {
                    message = "Некорректный ввод. Повторите попытку.";
                    continue;
                }

                double[] numbers = new double[inputNumbers.Length];

                bool isParse = true;

                for (int i = 0; i < inputNumbers.Length; i++)
                {
                    if (!double.TryParse(inputNumbers[i], out numbers[i]))
                    {
                        message = "Некорректный ввод. Повторите попытку.";
                        isParse = false;
                        continue;
                    }
                }

                if (!isParse)
                    continue;

                lastInput = input;
                sum = CalcSum(numbers);
            }
        }

        /// <summary>
        /// Вычисление суммы элементов массива.
        /// </summary>
        /// <param name="numbers">Массив для вычисление суммы элементов.</param>
        /// <returns>Сумма элементов переданного массива. Вернет 0 если массив не инициализирован.</returns>
        static double CalcSum(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            double sum = 0;

            foreach (double number in numbers)
                sum += number;

            return sum;
        }

        // ----------------------------------- Задача 3. -----------------------------------

        /// <summary>
        /// Задача 3. Определение времени года по порядковому номеру месяца.
        /// </summary>
        static void Task3()
        {
            string message = string.Empty;
            string seasonName = string.Empty;
            string input = string.Empty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-- Задача 3. Определение времени года по порядковому номеру месяца. --\n");

                if (input != string.Empty)
                    Console.Write($"Вы ввели: {input}. ");

                if (seasonName != string.Empty)
                {
                    Console.WriteLine($"Время года: {seasonName}.\n");
                    seasonName = string.Empty;
                }

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");

                Console.Write("Укажите порядковый номер месяца: ");

                input = Console.ReadLine();

                if (input.Trim().ToLower() == "в")
                    return;

                if (!int.TryParse(input, out int monthNumber) || !GetSeason(monthNumber, out Season season))
                {
                    message = "\n\nОшибка: введите число от 1 до 12.";
                    continue;
                }

                seasonName = GetSeasonName(season);
            }
        }

        /// <summary>
        /// Перечисление времен года.
        /// </summary>
        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Autumn
        }

        /// <summary>
        /// Определение времени года по порядковому номеру месяца.
        /// </summary>
        /// <param name="monthNumber">Порядковый номер месяца.</param>
        /// <param name="season">Переменная для записи времени года.</param>
        /// <returns>Возвращает false, если переданный номер месяца меньше 1 или больше 12.</returns>
        static bool GetSeason(int monthNumber, out Season season)
        {
            switch (monthNumber)
            {
                case 12:
                case 1:
                case 2:
                    season = Season.Winter;
                    return true;
                case 3:
                case 4:
                case 5:
                    season = Season.Spring;
                    return true;
                case 6:
                case 7:
                case 8:
                    season = Season.Summer;
                    return true;
                case 9:
                case 10:
                case 11:
                    season = Season.Autumn;
                    return true;
                default:
                    break;
            }

            season = Season.Winter;
            return false;
        }

        /// <summary>
        /// Получение названия времени года.
        /// </summary>
        /// <param name="season">Время года из перечисления.</param>
        /// <returns>Название времени года.</returns>
        static string GetSeasonName(Season season)
        {
            switch (season)
            {
                case Season.Winter:
                    return "зима";
                case Season.Spring:
                    return "весна";
                case Season.Summer:
                    return "лето";
                case Season.Autumn:
                    return "осень";
            }

            return string.Empty;
        }

        // ----------------------------------- Задача 4. -----------------------------------

        /// <summary>
        /// Задача 4. Определение числа из ряда Фибоначчи.
        /// </summary>
        static void Task4()
        {
            string message = string.Empty;
            string result = string.Empty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------- Задача 4. Определение числа из ряда Фибоначчи. -----------\n");

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }
                else if (result != string.Empty)
                {
                    Console.WriteLine($"{result}\n");
                    result = string.Empty;
                }

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");

                Console.Write("Введите порядковый номер числа из ряда Фибоначчи: ");

                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "в")
                    return;

                if (!int.TryParse(input, out int number))
                {
                    message = "Некорректный ввод. Повторите попытку.";
                    continue;
                }

                result = $"{number}-ое число в ряде Фибоначчи: {GetFibonacci(number)}";
            }
        }

        /// <summary>
        /// Определение числа в ряде Фибоначчи.
        /// </summary>
        /// <param name="number">Порядковый номер числа.</param>
        /// <returns>Число Фибоначчи.</returns>
        static int GetFibonacci(int number)
        {
            if (number <= 2)
                return 1;

            return GetFibonacci(number - 1) + GetFibonacci(number - 2);
        }
    }
}
