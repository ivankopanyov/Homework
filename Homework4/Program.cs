using System;

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
                Console.WriteLine();
                Console.WriteLine("=================== Домашняя работа №4 ===================");
                Console.WriteLine("1 - Вывод списка с ФИО.");
                Console.WriteLine("2 - Вычисление суммы введенных чисел.");
                Console.WriteLine("3 - Определение времени года по порядковому номеру месяца.");
                Console.WriteLine("4 - Определение числа из ряда Фибоначчи.");
                Console.WriteLine();
                Console.WriteLine("0 - Выход.");
                Console.WriteLine("==========================================================");
                Console.WriteLine();

                if (message != string.Empty)
                {
                    Console.WriteLine($"{message}\n");
                    message = string.Empty;
                }

                Console.Write("Укажите номер пункта меню: ");
                if (!int.TryParse(Console.ReadLine(), out int number)) number = -1;

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
            Console.WriteLine("-------------------- Задача 1. Вывод списка ФИО. --------------------\n");
            int number = 3;

            while (true)
            {
                switch (number)
                {
                    case 1:
                        AddPerson();
                        number = 3;
                        break;
                    case 2:
                        RemovePerson();
                        number = 3;
                        break;
                    case 0: return;
                    default:
                        number = OutputMenu(number == -1 ? "Некорректный ввод. Повторите попытку." : string.Empty);
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
            Console.WriteLine("\n\nТекущий список:\n");

            if (persons == null || persons.Length == 0) Console.WriteLine("\tСписок пуст.");
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
        /// Вывод меню Задания 1 в консоль.
        /// </summary>
        /// <param name="message">Сообение для пользователя.</param>
        /// <returns>Возвращает номер выбранного пункта меню. -1 при некорректном вводе.</returns>
        static int OutputMenu(string message = "")
        {
            OutputPersonsList();
            Console.WriteLine("* 1 - Добавить. *");
            if (persons != null && persons.Length > 0) Console.WriteLine("* 2 - Удалить. *");
            Console.WriteLine("* 0 - Выход в главное меню. *\n");

            if (message != string.Empty) Console.WriteLine($"{message}\n");

            int maxInputNumber = persons != null && persons.Length > 0 ? 2 : 1;

            Console.Write("Укажите номер пункта меню: ");
            if (!int.TryParse(Console.ReadLine(), out int number) || number < 0 || number > maxInputNumber) return -1;

            return number;
        }

        /// <summary>
        /// Добавление нового ФИО в список.
        /// </summary>
        static void AddPerson()
        {
            int maxLength = 100;

            string lastName = null;
            do
            {
                lastName = InputField("фамилию",
                    lastName != null ? "Некорректный ввод. Повторите попытку." : string.Empty,
                    "Введите 0 для отмены.",
                    "Допускаются только буквенные символы и дефис.",
                    $"Максимальная длина {maxLength}.");

                if (lastName == "0") return;
            }
            while (!IsValidLastName(lastName, maxLength));

            string firstName = null;
            do
            {
                firstName = InputField("имя",
                    firstName != null ? "Некорректный ввод. Повторите попытку." : string.Empty,
                    "Введите 0 для отмены.",
                    "Допускаются только буквенные символы.",
                    $"Максимальная длина {maxLength}.");

                if (firstName == "0") return;
            }
            while (!IsValidFirstName(firstName, maxLength));

            string patronymic = null;
            do
            {
                patronymic = InputField("отчество",
                    patronymic != null ? "Некорректный ввод. Повторите попытку." : string.Empty,
                    "Введите 0 для отмены.",
                    "Допускаются только буквенные символы.",
                    $"Максимальная длина {maxLength}.",
                    "Необязательное поле.");

                if (patronymic == "0") return;
            }
            while (patronymic != string.Empty && !IsValidFirstName(patronymic, maxLength));

            Push(ref persons, (ToDisplayFirstName(firstName), ToDisplayLastName(lastName), ToDisplayFirstName(patronymic)));

            Console.WriteLine();
        }

        /// <summary>
        /// Ввод данных пользователем.
        /// </summary>
        /// <param name="fieldName">Нащвание заполняемого поля в винительном падеже.</param>
        /// <param name="message">Сообщение для пользователя.</param>
        /// <param name="descriptions">Дополнительное описание.</param>
        /// <returns>Возвращает введенную пользователем строку.</returns>
        static string InputField(string fieldName, string message, params string[] descriptions)
        {
            OutputPersonsList();

            if (message != null && message != string.Empty) Console.WriteLine($"{message}\n");

            foreach (string description in descriptions)
                Console.WriteLine($"* {description} *");
            Console.WriteLine();

            Console.Write($"Укажите {fieldName}: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Проверка имени на валиднось.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="maxLength">Максимально допустимая длина после тримминга.</param>
        /// <returns>Возвращает результат проверки.</returns>
        static bool IsValidFirstName(string firstName, int maxLength = int.MaxValue)
        {
            if (firstName == null) return false;
            firstName = firstName.Trim();
            if (firstName == string.Empty || firstName.Length > maxLength) return false;

            foreach (char symbol in firstName)
                if (!char.IsLetter(symbol)) return false;

            return true;
        }

        /// <summary>
        /// Проверка фамилии на валиднось с учетом двойной фамилии.
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="maxLength">Максимально допустимая длина после тримминга.</param>
        /// <returns>Возвращает результат проверки.</returns>
        static bool IsValidLastName(string lastName, int maxLength = int.MaxValue)
        {
            if (lastName == null) return false;
            lastName = lastName.Trim();
            if (lastName == string.Empty) return false;

            var array = lastName.Split("-", StringSplitOptions.RemoveEmptyEntries);
            if (array.Length < 1 || array.Length > 2) return false;

            int charsCount = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (!IsValidFirstName(array[i]))
                    return false;
                charsCount += array[i].Length;
            }

            if (charsCount > maxLength) return false;

            return true;
        }

        /// <summary>
        /// Приведение имени к формату "Имя".
        /// </summary>
        /// <param name="firstName">Имя для приведения.</param>
        /// <returns>Результат приведения.</returns>
        static string ToDisplayFirstName(string firstName)
        {
            if (firstName == null) return firstName;
            firstName = firstName.Trim();
            if (firstName.Length <= 1) return firstName;
            return char.ToUpper(firstName[0]) + firstName.Remove(0, 1).ToLower();
        }

        /// <summary>
        /// Приведение фамилии к формату "Фамилия" или "Фамилия-Фамилия"
        /// </summary>
        /// <param name="lastName">Фамилия для приведения.</param>
        /// <returns>Результат приведения.</returns>
        static string ToDisplayLastName(string lastName)
        {
            if (lastName == null) return lastName;
            lastName = lastName.Trim();

            var array = lastName.Split("-", StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0) return lastName;
            lastName = string.Empty;

            for (int i = 0; i < array.Length; i++)
            {
                string separator = array.Length > 1 && i < array.Length - 1 ? "-" : string.Empty;
                lastName += $"{ToDisplayFirstName(array[i])}{separator}";
            }

            return lastName;
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

                if (message != string.Empty) Console.WriteLine($"{message}\n");

                Console.WriteLine("* Введите 0 для отмены. *\n");

                Console.Write("Укажите номер пункта списка для удаления: ");
                if (!int.TryParse(Console.ReadLine(), out int number) || number < 0 || number > persons.Length) number = -1;

                if (number == 0) return;

                if (number == -1)
                {
                    message = "Некорректный ввод. Повторите попытку.";
                    continue;
                }

                Remove(ref persons, number - 1);
                break;
            }
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
            Console.WriteLine("------------ Задача 2. Вычисление суммы введенных чисел. ------------\n");

            string result = string.Empty;

            while (true)
            {
                if (result != string.Empty) Console.WriteLine($"\n{result}\n");

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");
                Console.WriteLine("Введите числа для вычисления суммы через пробел:\n");
                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "в") return;

                result = $"Вы ввели: {input}\n";

                string[] inputNumbers = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                double[] numbers;
                if (inputNumbers.Length == 0 || !TryParseArray(inputNumbers, out numbers))
                {
                    result += "Некорректный ввод. Повторите попытку.";
                    continue;
                }

                result += $"Сумма введенных вами чисел: {CalcSum(numbers)}";
            }
        }

        /// <summary>
        /// Вычисление суммы элементов массива.
        /// </summary>
        /// <param name="numbers">Массив для вычисление суммы элементов.</param>
        /// <returns>Сумма элементов переданного массива. Вернет 0 если массив не инициализирован.</returns>
        static double CalcSum(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0) return 0;

            double sum = 0;
            foreach (double number in numbers)
                sum += number;

            return sum;
        }

        /// <summary>
        /// Попытка привести массив строк к массиву чисел.
        /// </summary>
        /// <param name="str">Массив строк для приведения.</param>
        /// <param name="numbers">Массив чисел для записи результата приведения.</param>
        /// <returns>Возвращает true, если все элементы массива строк были удачно приведены к типу double.</returns>
        static bool TryParseArray(string[] str, out double[] numbers)
        {
            numbers = new double[str.Length];

            for (int i = 0; i < str.Length; i++)
                if (!double.TryParse(str[i], out numbers[i])) return false;

            return true;
        }

        // ----------------------------------- Задача 3. -----------------------------------

        /// <summary>
        /// Задача 3. Определение времени года по порядковому номеру месяца.
        /// </summary>
        static void Task3()
        {
            Console.WriteLine("-- Задача 3. Определение времени года по порядковому номеру месяца. --\n");

            string result = string.Empty;

            while (true)
            {
                if (result != string.Empty) Console.WriteLine($"\n{result}\n");

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");
                Console.Write("Укажите порядковый номер месяца: ");
                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "в") return;

                result = $"Вы ввели: {input}. ";

                if (!int.TryParse(input, out int monthNumber) || !GetSeason(monthNumber, out Season season))
                {
                    result += "\n\nОшибка: введите число от 1 до 12.";
                    continue;
                }

                result += $"Время года: {GetSeasonName(season)}.";
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
            season = Season.Winter;

            switch (monthNumber)
            {
                case 3:
                case 4:
                case 5:
                    season = Season.Spring;
                    break;
                case 6:
                case 7:
                case 8:
                    season = Season.Summer;
                    break;
                case 9:
                case 10:
                case 11:
                    season = Season.Autumn;
                    break;
            }

            return monthNumber >= 1 && monthNumber <= 12;
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
                case Season.Winter: return "зима";
                case Season.Spring: return "весна";
                case Season.Summer: return "лето";
                case Season.Autumn: return "осень";
            }

            return string.Empty;
        }

        // ----------------------------------- Задача 4. -----------------------------------

        /// <summary>
        /// Задача 4. Определение числа из ряда Фибоначчи.
        /// </summary>
        static void Task4()
        {
            Console.WriteLine("----------- Задача 4. Определение числа из ряда Фибоначчи. -----------\n");

            string result = string.Empty;

            while (true)
            {
                if (result != string.Empty) Console.WriteLine($"\n{result}\n");

                Console.WriteLine("* Введите \"в\" для выхода в главное меню. *\n");
                Console.Write("Введите порядковый номер числа из ряда Фибоначчи: ");
                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "в") return;

                if (!int.TryParse(input, out int number))
                {
                    result = "Некорректный ввод. Повторите попытку.";
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
