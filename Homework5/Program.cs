using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace Homework5
{
    class Program
    {
        /// <summary>
        /// Точка входа в приложение. Главное меню приложеня.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SaveCurrentDateTime("startup.json");

            while (true)
            {
                Console.WriteLine("\n\n -------------- Домашняя работа №5 -------------- \n");
                Console.WriteLine("*    1 - Запись введенного текста в файл          *");
                Console.WriteLine("*    2 - Запись времени запуска программы в файл  *");
                Console.WriteLine("*    3 - Запись ряда чисел в бинарный файл        *");
                Console.WriteLine("*    4 - Запись дерева каталогов и файлов в файл  *");
                Console.WriteLine("*    5 - Список задач                             *");
                Console.WriteLine("*                                                 *");
                Console.WriteLine("*  ESC - Выход                                    *");
                Console.WriteLine();
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        Task1();
                        break;
                    }
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        Task2();
                        break;
                    }
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    {
                        Task3();
                        break;
                    }
                    else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                    {
                        Task4();
                        break;
                    }
                    else if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                    {
                        Task5();
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Завершение работы приложения...\nНажмите любую клавишу.");
                        Console.ReadKey(true);
                        return;
                    }
                }
            }
        }

        #region Задача 1

        /// <summary>
        /// Задача 1. Запись введенного текста в файл.
        /// </summary>
        static void Task1()
        {
            string fileName = "task1.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + fileName;

            Console.WriteLine("\n\n------ Задача 1. Запись введенного текста в файл. ------\n");

            while (true)
            {
                bool isExistsFile = File.Exists(path);

                Console.WriteLine($"\n*  1 - Новый  *  {(isExistsFile ? "2 - Посмотреть  *  3 - Удалить  *  " : "")}ESC - Выйти  *\n");
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        SaveTextToFile(path);
                        break;
                    }
                    else if (isExistsFile && (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2))
                    {
                        PrintTextFromFile(path);
                        break;
                    }
                    else if (isExistsFile && (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3))
                    {
                        RemoveFileWithText(path);
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape) return;
                }
            }
        }

        /// <summary>
        /// Выводит текст из файла в консоль.
        /// </summary>
        /// <param name="path">Путь к файлу для чтения.</param>
        static void PrintTextFromFile(string path)
        {
            string text = ReadFromFile(path);

            Console.WriteLine("\n");
            if (text == null)
            {
                Console.WriteLine("\tНет сохраненных данных.\n");
                return;
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Сохраняет введенный текст в файл.
        /// </summary>
        /// <param name="path">Путь к файлу для сохранения.</param>
        static void SaveTextToFile(string path)
        {
            bool finishWrite = false;

            Console.Write("\n\nВведите текст для сохранения в файл: ");
            string text = Console.ReadLine();

            if (File.Exists(path))
            {
                Console.WriteLine("\n*  1 - Добавить  *  2 - Перезаписать  *  ESC - Отмена  *\n");
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        finishWrite = key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1;
                        Console.WriteLine();
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine();
                        return;
                    }
                }
            }

            if (WriteToFile(path, text, finishWrite))
                Console.WriteLine("\n\tТекст успешно сохранен.");
            else Console.WriteLine("\n\tОшибка записи.");
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="path">Путь к файлу для удаления.</param>
        static void RemoveFileWithText(string path)
        {
            Console.Write("\n\nВы уверены (Y / N)...");

            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Y || char.ToLower(key.KeyChar) == 'н') break;
                else if (key.Key == ConsoleKey.N || char.ToLower(key.KeyChar) == 'т') return;
            }

            if (RemoveFile(path))
                Console.WriteLine("\n\n\tВсе сохраненные данные успешно удалены.");
            else Console.WriteLine("\n\n\tОшибка: не удалось удалить данные.");
        }


        /// <summary>
        /// Запись текста в файл.
        /// </summary>
        /// <param name="path">Путь к файлу для записи.</param>
        /// <param name="text">Текст для записи.</param>
        /// <param name="finishWriting">True - дописать текст в конец файла. False - перезаписать файл.</param>
        /// <returns>True - Текст записан в файл. False - Текст не записан в файл.</returns>
        static bool WriteToFile(string path, string text, bool finishWriting)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, finishWriting))
                {
                    streamWriter.WriteLine(text);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Чтение текста из файла.
        /// </summary>
        /// <param name="path">Путь к файлу для чтения.</param>
        /// <returns>Текст из файла. Null - если текст не был прочитан.</returns>
        static string ReadFromFile(string path)
        {
            string result = null;

            try
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string str = null;
                    while ((str = streamReader.ReadLine()) != null)
                        result += str + "\n";
                }
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="path">Путь к файлу для удаления.</param>
        /// <returns>True - Файл был удален. False - Файл не был удален.</returns>
        static bool RemoveFile(string path)
        {
            if (!File.Exists(path))
                return true;

            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Задача 2

        /// <summary>
        /// Задача 2. Запись времени запуска программы в файл.
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("\n\n-- Задача 2. Запись времени запуска программы в файл. --\n");

            string fileName = "startup.json";

            while (true)
            {
                Console.WriteLine($"\n*  1 - Последний старт  *  2 - Все  *  3 - Указать дату  *  ESC - Выйти  *\n");
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        var dateTimes = GetStartupTimes(fileName);
                        if (dateTimes.Count == 0)
                            Console.WriteLine("\n\n\tЗаписи отсутствуют.");
                        else
                            Console.WriteLine($"\n\n\tПоследний запуск программы: {dateTimes[dateTimes.Count - 1].ToString("dd.MM.yyyy HH:mm:ss")}");
                        break;
                    }
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        PrintSturtupTimes(fileName);
                        break;
                    }
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    {
                        InputDate(fileName);
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape) return;
                }
            }
        }

        /// <summary>
        /// Запись текущей даты в файл.
        /// </summary>
        /// <param name="fileName">Путь к файлу для записи.</param>
        static void SaveCurrentDateTime(string fileName)
        {
            List<DateTime> dateTimes = GetStartupTimes(fileName);

            dateTimes.Add(DateTime.Now);

            try
            {
                File.WriteAllText(fileName, JsonSerializer.Serialize(dateTimes));
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Чтение списка дат из файла.
        /// </summary>
        /// <param name="fileName">Путь к файлу для чтения.</param>
        /// <returns>Список дат, считанный из файла.</returns>
        static List<DateTime> GetStartupTimes(string fileName)
        {
            List<DateTime> dateTimes;

            try
            {
                dateTimes = JsonSerializer.Deserialize<List<DateTime>>(File.ReadAllText(fileName));
            }
            catch (Exception)
            {
                return new List<DateTime>();
            }

            return dateTimes;
        }

        /// <summary>
        /// Вывод списка дат и времени из файла в консоль.
        /// </summary>
        /// <param name="fileName">Путь к файлу для чтения списка дат и времени.</param>
        /// <param name="dateTime">Если дата не null, выводит время только в переданную дату.</param>
        static void PrintSturtupTimes(string fileName, DateTime? dateTime = null)
        {
            var date = dateTime != null ? (DateTime)dateTime : DateTime.Now;

            var dateTimes = GetStartupTimes(fileName);
            if (dateTimes.Count == 0)
            {
                Console.WriteLine("\n\tЗаписи отсутствуют.");
                return;
            }
            Console.WriteLine("\n");
            int counter = 0;
            foreach (var dt in dateTimes)
            {
                if (dateTime == null || (dateTime != null && date.Day == dt.Day && date.Month == dt.Month && date.Year == dt.Year))
                {
                    Console.WriteLine($"\t{dt.ToString("dd.MM.yyyy HH:mm:ss")}");
                    counter++;
                }
            }

            if (counter == 0) Console.WriteLine("\tЗаписи отсутствуют.");
        }

        /// <summary>
        /// Попытка приведения строки к дате.
        /// </summary>
        /// <param name="dateTimeStr">Строка с предполагаемой датой.</param>
        /// <param name="dateTime">Переменная для сохранения даты. При неудачном приведении возвращает текущую дату.</param>
        /// <returns>True - если приведение прошло удачно. False - приведение не удалось.</returns>
        static bool TryParseDateTime(string dateTimeStr, out DateTime dateTime)
        {
            string[] patterns = new string[] { "dd.MM.yyyy", "d.MM.yyyy", "dd.M.yyyy", "d.M.yyyy", "dd.MM.yy", "d.MM.yy", "dd.M.yy", "d.M.yy" };
            return DateTime.TryParseExact(dateTimeStr, patterns, new CultureInfo("ru-Ru"), DateTimeStyles.None, out dateTime);
        }

        /// <summary>
        /// Ввод даты через консоль.
        /// </summary>
        /// <param name="fileName">Файл для чтения и сохрания списка дат. Нужен для вывода последней даты и времени запуска приложения.</param>
        static void InputDate(string fileName)
        {
            Console.Write($"\n\nУкажите дату в формате \"{DateTime.Now.ToString("dd.MM.yyyy")}\": ");
            if (TryParseDateTime(Console.ReadLine(), out DateTime dateTime))
            {
                PrintSturtupTimes(fileName, dateTime);
                return;
            }
            Console.WriteLine("\nДата указана некорректно.");
        }

        #endregion

        #region Задача 3

        /// <summary>
        /// Задача 3. Запись ряда чисел в бинарный файл.
        /// </summary>
        static void Task3()
        {
            Console.WriteLine("\n\n----- Задача 3. Запись ряда чисел в бинарный файл. -----\n");

            string fileName = "bytes.bin";
            while (true)
            {
                Console.WriteLine("\n*  Введите \"В\" для выхода в главное меню.  *\n");
                Console.WriteLine("Укажите произвольный ряд чисел от 0 до 255 через пробел:");
                string inputStr = Console.ReadLine().Trim();

                if (inputStr.ToLower() == "в") return;

                string[] inputArr = inputStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (inputArr.Length == 0)
                {
                    Console.WriteLine("\n\tВы ничего не ввели. Повторите попытку.");
                    continue;
                }

                byte[] numbers = new byte[inputArr.Length];

                bool isParse = true;

                for (int i = 0; i < inputArr.Length; i++)
                {
                    if (!byte.TryParse(inputArr[i], out numbers[i]))
                    {
                        isParse = false;
                        Console.WriteLine("\n\tНекорректный ввод. Повторите попытку.");
                        break;
                    }
                }

                if (!isParse) continue;

                try
                {
                    Console.WriteLine("\nЗапись введенных чисел в бинарный файл...");
                    File.WriteAllBytes(fileName, numbers);
                    Console.WriteLine("Загрузка чисел из бинарного файла...");
                    numbers = File.ReadAllBytes("bytes.bin");
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\tЧто-то пошло не так...");
                    continue;
                }

                if (numbers == null || numbers.Length == 0)
                {
                    Console.WriteLine("\n\tЧто-то пошло не так...");
                    continue;
                }

                Console.WriteLine("\nВаш числовой ряд, загруженный из бинарного файла:");
                foreach (var number in numbers)
                {
                    Console.Write($"{number} ");
                }

                Console.WriteLine();
            }
        }

        #endregion

        #region Задача 4

        /// <summary>
        /// Задача 4. Запись дерева каталогов и файлов в файл.
        /// </summary>
        static void Task4()
        {
            Console.WriteLine("\n\n-- Задача 4. Запись дерева каталогов и файлов в файл. --\n");

            bool isShow = false;
            string path = Directory.GetCurrentDirectory();
            string newPath = path;

            string file = AppDomain.CurrentDomain.BaseDirectory + "tree.txt";
            string fileRec = AppDomain.CurrentDomain.BaseDirectory + "tree_rec.txt";

            while (true)
            {
                if (!OutputSubdirsAndFiles(newPath, isShow))
                {
                    Console.WriteLine($"\nУказанной директории не существует или у вас нет доступа.");
                    newPath = path;
                    continue;
                }
                path = newPath;

                Console.WriteLine($"\n*  1 - {(isShow ? "Свернуть" : "Развернуть")}  *  2 - Перейти  *  3 - Запись  *  4 - Запись(рек.)  *  ESC - Выйти  *\n");
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        isShow = !isShow;
                        Console.WriteLine();
                        break;
                    }
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        newPath = UpdatePath(path);
                        break;
                    }
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    {
                        if (WriteToFile(file, GetTree(new DirectoryInfo(path)), false))
                            Console.WriteLine($"\n\nДерево успешно сохранено в файл:\n{file}");
                        else Console.WriteLine($"\n\nОшибка записи в файл:\n{file}");
                        break;
                    }
                    else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                    {
                        if (WriteToFile(fileRec, GetTreeRecursion(new DirectoryInfo(path)), false))
                            Console.WriteLine($"\n\nДерево успешно сохранено в файл:\n{fileRec}");
                        else Console.WriteLine($"\n\nОшибка записи в файл:\n{fileRec}");
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape) return;
                }
            }
        }

        /// <summary>
        /// Класс, хранящий символы для папок и файлов.
        /// </summary>
        static public class Icon
        {
            public const string folder = "\u25BA";

            public const string openFolder = "\u25BC";

            public const string file = " ";
        }

        /// <summary>
        /// Вывод сабдиректорий и файлов переданной директории.
        /// </summary>
        /// <param name="path">Путь к корневой директории.</param>
        /// <param name="isShow">False - не выводит сабдиректории и файлы, только путь.</param>
        /// <returns>False - если директория не существует или к ней нет доступа.</returns>
        static bool OutputSubdirsAndFiles(string path, bool isShow)
        {
            if (!Directory.Exists(path)) return false;

            string[] directories, files;

            try
            {
                directories = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
            }
            catch (Exception)
            {
                return false;
            }

            Console.WriteLine($"\n{(isShow ? Icon.openFolder : Icon.folder)} {path}");

            if (!isShow) return true;

            Array.Sort(directories);
            foreach (string directory in directories)
                Console.WriteLine($"  {Icon.folder} {Path.GetFileName(directory)}");

            Array.Sort(files);
            foreach (string file in files)
                Console.WriteLine($"  {Icon.file} {Path.GetFileName(file)}");

            return true;
        }

        /// <summary>
        /// Изменение пути к директории.
        /// </summary>
        /// <param name="path">Корневая директория.</param>
        /// <returns>Измененый путь.</returns>
        static string UpdatePath(string path)
        {
            Console.WriteLine("\n\n* Введите \"..\" чтоб перейти на директорию выше. *\n");
            Console.Write($"{path}{(path[path.Length - 1] == '/' ? "" : "/")}");
            return Path.GetFullPath(Path.Combine(path, Console.ReadLine()));
        }

        /// <summary>
        /// Получение дерева каталогов и файлов из указанной директории.
        /// </summary>
        /// <param name="directory">Корневая директория.</param>
        /// <returns>Дерево каталогов и файлов.</returns>
        static string GetTree(DirectoryInfo directory)
        {
            if (directory == null) return string.Empty;

            string result = string.Empty;

            List<(DirectoryInfo Dir, string Indent, bool IsFinalDir)> directories = new List<(DirectoryInfo, string, bool)>() { (directory, "", true) };

            for (int i = 0; i < directories.Count; i++)
            {
                result += directories[i].Indent + (directories[i].IsFinalDir ? "└" : "├") + directories[i].Dir.Name + "\n";

                DirectoryInfo[] subDirs;
                FileInfo[] files;
                try
                {
                    subDirs = directories[i].Dir.GetDirectories();
                    files = directories[i].Dir.GetFiles();
                }
                catch (Exception)
                {
                    continue;
                }

                for (int j = 0; j < files.Length; j++)
                    result += directories[i].Indent + (directories[i].IsFinalDir ? " " : "│") + (j == files.Length - 1 && subDirs.Length == 0 ? "└" : "├") + files[j].Name + "\n";

                directories.InsertRange(i + 1, ToDirectories(subDirs, directories[i].Indent + (directories[i].IsFinalDir ? " " : "│")));
            }

            return result;
        }

        /// <summary>
        /// Преобразует массив директорий в список кортежей.
        /// </summary>
        /// <param name="dirs">Директория.</param>
        /// <param name="indent">Отступ.</param>
        /// <returns>Список кортежей c дополнительной информацией для директорий.</returns>
        static List<(DirectoryInfo Dir, string Indent, bool IsFinalDir)> ToDirectories(DirectoryInfo[] dirs, string indent)
        {
            if (dirs == null || dirs.Length == 0)
                return new List<(DirectoryInfo Dir, string Indent, bool IsFinalDir)>();

            List<(DirectoryInfo Dir, string Indent, bool IsFinalDir)> dirsList = new List<(DirectoryInfo, string, bool)>();

            for (int i = 0; i < dirs.Length; i++)
                dirsList.Add((dirs[i], indent, i == dirs.Length - 1));

            return dirsList;
        }

        /// <summary>
        /// Получение дерева каталогов и файлов из указанной директории с помощью рекурсии.
        /// </summary>
        /// <param name="dir">Корневая директория.</param>
        /// <param name="indent">Отступ.</param>
        /// <param name="isFinalDir">Последняя директория в списке.</param>
        /// <returns>Дерево каталогов и файлов.</returns>
        static string GetTreeRecursion(DirectoryInfo dir, string indent = "", bool isFinalDir = true)
        {
            if (dir == null) return string.Empty;

            string result = indent + (isFinalDir ? "└" : "├") + dir.Name + "\n";
            indent += isFinalDir ? " " : "│";

            DirectoryInfo[] subDirs;
            FileInfo[] files;

            try
            {
                subDirs = dir.GetDirectories();
                files = dir.GetFiles();
            }
            catch (Exception)
            {
                return result;
            }

            for (int i = 0; i < files.Length; i++)
                result += indent + (i == files.Length - 1 && subDirs.Length == 0 ? "└" : "├") + files[i].Name + "\n";

            for (int i = 0; i < subDirs.Length; i++)
                result += GetTreeRecursion(subDirs[i], indent, i == subDirs.Length - 1);

            return result;
        }

        #endregion

        #region Задача 5

        /// <summary>
        /// Задача 5. Список задач.
        /// </summary>
        static void Task5()
        {
            Console.WriteLine("\n\n---------------- Задача 5. Список задач. ---------------\n");

            string fileName = "tasks.json";
            var organizer = new Organizer(fileName);

            while (true)
            {
                Console.WriteLine($"\n{organizer}");
                Console.WriteLine($"\n*  1 - Добавить  *  {(organizer.ToDoCount > 0 ? "2 - Удалить  *  3 - Изменить статус  *  " : "")}ESC - Выйти  *\n");
                Console.Write("Нажмите клавишу, соответствующую пункту меню...");

                ConsoleKeyInfo key;

                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        Console.Write("\n\nОпишите новую задачу: ");
                        organizer.AddToDo(new ToDo(Console.ReadLine()));
                        organizer.SaveToDoListToFile(fileName);
                        break;
                    }
                    else if (organizer.ToDoCount > 0 && (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2))
                    {
                        Console.Write("\n\nУкажите номер задачи для удаления: ");
                        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > organizer.ToDoCount)
                        {
                            Console.WriteLine("\n\nНекорректный ввод.");
                            break;
                        }
                        organizer.RemoveToDoAt(index - 1);
                        organizer.SaveToDoListToFile(fileName);
                        break;
                    }
                    else if (organizer.ToDoCount > 0 && (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3))
                    {
                        Console.Write("\n\nУкажите номер задачи для изменения статуса: ");
                        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > organizer.ToDoCount)
                        {
                            Console.WriteLine("\n\nНекорректный ввод.");
                            break;
                        }
                        organizer.ChangeStatusToDoAt(index - 1);
                        organizer.SaveToDoListToFile(fileName);
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape) return;
                }
            }
        }

        #endregion
    }
}
