using System;

namespace Homework2
{
    class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("1 - Рассчет среднесуточной температуры.");
            Console.WriteLine("2 - Вывод названия месяца по его порядковому номеру.");
            Console.WriteLine("3 - Определение четности числа.");
            Console.WriteLine("4 - Схематичное отображение чека.");
            Console.WriteLine("5 - Проверка на дождливую зиму.");
            Console.WriteLine("6 - График работы.");
            Console.WriteLine("");
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("====================================================");
            Console.WriteLine("");
            Console.Write("Укажите номер задачи: ");
            int number = int.Parse(Console.ReadLine());
            switch (number)
            {
                case 1:
                    Console.WriteLine("\n-------- Рассчет среднесуточной температуры --------\n");
                    Task1();
                    break;

                case 2:
                    Console.WriteLine("\n-- Вывод названия месяца по его порядковому номеру --\n");
                    Task2();
                    break;

                case 3:
                    Console.WriteLine("\n------------ Определение четности числа ------------\n");
                    Task3();
                    break;

                case 4:
                    Console.WriteLine("\n----------- Схематичное отображение чека ----------\n");
                    Task4();
                    break;

                case 5:
                    Console.WriteLine("\n------------ Проверка на дождливую зиму ------------\n");
                    Task5();
                    break;

                case 6:
                    Console.WriteLine("\n------------------- График работы -------------------\n");
                    Task6();
                    break;

                case 0:
                    Console.WriteLine("\nЗавершение работы приложения...\nНажмите любую клавишу");
                    Console.ReadKey();
                    return;

                default:
                    Console.WriteLine("\nНекорректный ввод.\nПовторите попытку.");
                    Console.ReadKey();
                    break;
            }
        }

        /// <summary>
        /// Задача 1. Рассчет среднесуточной температуры.
        /// </summary>
        /// <param name="readKey">Продолжить по нажатию клавиши</param>
        /// <returns>Среднесуточная температура</returns>
        static int Task1(bool readKey = true)
        {
            Console.Write("Укажите минимальную температуру за сутки: ");
            int minTemperature = int.Parse(Console.ReadLine());

            Console.Write("Укажите максимальную температуру за сутки: ");
            int maxTemperature = int.Parse(Console.ReadLine());

            int averageTemperature = CalcAverage(minTemperature, maxTemperature);

            Console.WriteLine($"Среднесуточная температура: {averageTemperature}");

            if (readKey)
            {
                Console.ReadKey();
            }

            return averageTemperature;
        }

        /// <summary>
        /// Рассчет среднего значения двух целых чисел.
        /// </summary>
        /// <param name="value1">Первое число</param>
        /// <param name="value2">Второе число</param>
        /// <returns>Среднее значение</returns>
        static int CalcAverage(int value1, int value2)
        {
            return (value1 + value2) / 2;
        }

        /// <summary>
        /// Задача 2. Вывод названия месяца по его порядковому номеру.
        /// </summary>
        /// <param name="readKey">Продолжить по нажатию клавиши</param>
        /// <returns>Порядковый номер месяца</returns>
        static int Task2(bool readKey = true)
        {
            Console.Write("Укажите порядковый номер месяца: ");
            int number = int.Parse(Console.ReadLine());

            if (number < 1 || number > 12)
            {
                Console.WriteLine("Месяца с таким порядковым номером не существует.\nПовторите попытку.");
                if (readKey)
                {
                    Console.ReadKey();
                }
                return -1;
            }

            DateTime dateTime = new DateTime(2022, number, 1);
            string month = dateTime.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("ru-RU"));

            Console.WriteLine($"Месяц: {month}.");

            if (readKey)
            {
                Console.ReadKey();
            }
            return number;
        }

        /// <summary>
        /// Задача 3. Определение четности числа.
        /// </summary>
        static void Task3()
        {
            Console.Write("Укажите число для проверки его четности: ");
            int number = int.Parse(Console.ReadLine());

            string not = number % 2 != 0 ? "не" : "";

            Console.WriteLine($"Число {number} является {not}четным.");
            Console.ReadKey();
        }

        /// <summary>
        /// Задача 4. Схематичное отображение чека.
        /// </summary>
        static void Task4()
        {
            string shopName = "ООО Магазин";

            Product product1 = new Product();
            product1.name = "Товар 1";
            product1.quantity = 1;
            product1.price = 123.45;

            Product product2 = new Product();
            product2.name = "Товар 2";
            product2.quantity = 2;
            product2.price = 678.90;

            Product product3 = new Product();
            product3.name = "Товар 3";
            product3.quantity = 3;
            product3.price = 1357.24;

            double total = product1.Total + product2.Total + product3.Total;

            string strDateTime = new DateTime(2022, 5, 15, 12, 34, 25).ToString("dd.MM.yyyy HH:mm:ss");

            Console.WriteLine(" ----------------------------------");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"|        {strDateTime}       |");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"|            {shopName}           |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"| {product1.name}    {product1.price:F2}  x  {product1.quantity}   {product1.Total:F2} |");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"| {product2.name}    {product2.price:F2}  x  {product2.quantity}  {product2.Total:F2} |");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"| {product3.name}   {product3.price:F2}  x  {product3.quantity}  {product3.Total:F2} |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("| - - -                            |");
            Console.WriteLine("|                                  |");
            Console.WriteLine($"| Итог: {total:F2}                    |");
            Console.WriteLine("|                                  |");
            Console.WriteLine(" ---------------------------------- ");

            Console.ReadKey();
        }

        /// <summary>
        /// Структура товара.
        /// </summary>
        struct Product
        {
            /// <summary>
            /// Hазвание товара.
            /// </summary>
            public string name;

            /// <summary>
            /// Цена товара.
            /// </summary>
            public double price;

            /// <summary>
            /// Колличество единиц товара.
            /// </summary>
            public int quantity;

            /// <summary>
            /// Общая сумма.
            /// </summary>
            public double Total => price * quantity;
        }

        /// <summary>
        /// Задача 5. Проверка на дождливую зиму.
        /// </summary>
        static void Task5()
        {
            double temperature = Task1(false);
            int monthNumber = Task2(false);

            if ((monthNumber == 1 || monthNumber == 2 || monthNumber == 12) && temperature > 0)
            {
                Console.WriteLine("Дождливая зима!");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Задача 6. Вывод графика рабочих дней.
        /// </summary>
        static void Task6()
        {
            Office office1 = new Office();
            office1.workingDays = DaysOfWeek.Вторник | DaysOfWeek.Среда | DaysOfWeek.Четверг;

            Office office2 = new Office();
            office2.workingDays = DaysOfWeek.Понедельник | DaysOfWeek.Вторник | DaysOfWeek.Среда | DaysOfWeek.Четверг | DaysOfWeek.Пятница | DaysOfWeek.Суббота;

            Console.WriteLine("График работы:\n");
            Console.WriteLine($"Офис 1: {office1.workingDays}");
            Console.WriteLine($"Офис 2: {office2.workingDays}");

            Console.ReadKey();
        }

        /// <summary>
        /// Дни недели в двоичном представлении.
        /// </summary>
        [Flags]
        public enum DaysOfWeek
        {
            Понедельник = 0b_0000001,
            Вторник = 0b_0000010,
            Среда = 0b_0000100,
            Четверг = 0b_0001000,
            Пятница = 0b_0010000,
            Суббота = 0b_0100000,
            Воскресенье = 0b_1000000
        }

        /// <summary>
        /// Структура офиса.
        /// </summary>
        public struct Office
        {
            /// <summary>
            /// Рабочие дни офиса.
            /// </summary>
            public DaysOfWeek workingDays;
        }
    }
}