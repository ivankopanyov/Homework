using System;
using System.Diagnostics;
using System.Linq;

namespace Homework6
{
    /// <summary>
    /// Класс для работы с процессами оперционной системы.
    /// </summary>
    public static class TaskManager
    {
        /// <summary>
        /// Параметры для сортировки массива процессов.
        /// </summary>
        public enum Options
        {
            /// <summary>
            /// Пустой параметр.
            /// </summary>
            None,

            /// <summary>
            /// Id процесса.
            /// </summary>
            Id,

            /// <summary>
            /// Имя процесса.
            /// </summary>
            Name,

            /// <summary>
            /// Память процесса.
            /// </summary>
            Memory
        }

        /// <summary>
        /// Выполняет переданную команду.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <exception cref="CommandNotFoundException">Возбуждается, если команда пустая или не найдена.</exception>
        /// <exception cref="OptionNotFoundException">Возбуждается, если параметр не содержится в указанной команде.</exception>
        /// <exception cref="IdenticalOptionsException">Возбуждается, если указано два и более одинаковых параметра.</exception>
        /// <exception cref="InvalidOptionValueException">Возбуждается, если указано недопустимое знчение для параметра.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось получить доступ к процессу или его параметру.</exception>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если процесс не найден.</exception>
        public static void Execute(string command)
        {
            if (string.IsNullOrEmpty(command)) throw new CommandNotFoundException("Команда пустая или не найдена.", null);

            var options = command.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (options.Length == 0) throw new CommandNotFoundException("Команда пустая или не найдена.", null);

            if (options[0] == "print" || options[0] == "p") ExecutePrint(Shift(options));
            else if (options[0] == "kill" || options[0] == "k") ExecuteKill(string.Join(' ', Shift(options)));
            else if (options[0] == "info" || options[0] == "i") ExecuteInfo(string.Join(' ', Shift(options)));
            else if (options[0] == "exit" || options[0] == "e") Exit(string.Join(' ', Shift(options)));
            else throw new CommandNotFoundException("Команда пустая или не найдена.", options[0]);
        }

        /// <summary>
        /// Выводит список процессов, отсортированный по переданным параметрам.
        /// </summary>
        /// <param name="option">Параметр сортировки.</param>
        /// <param name="reverse">Обратная сртировка.</param>
        /// <param name="limit">Колличество выводимых процессов</param>
        /// <exception cref="InvalidOptionValueException">Возбуждается, если параметр limit меньше или равен 0.</exception>
        public static void Print(Options option = Options.None, bool reverse = false, int limit = int.MaxValue)
        {
            Process[] processes = Process.GetProcesses();

            if (option != Options.None) processes = OrderBy(processes, option, reverse);

            if (limit <= 0) throw new InvalidOptionValueException("Передано некорректное значение параметра.", "print", "limit", limit.ToString());

            if (limit >= processes.Length) limit = processes.Length;

            var maxLength = GetMaxLengthsItems(processes);

            Console.WriteLine();
            Console.WriteLine($" {new string(' ', maxLength.Id - 2)}ID  |  Name{new string(' ', maxLength.Name - 4)}  |  {new string(' ', maxLength.Memory - 3)}Memory");
            Console.WriteLine(new string('-', maxLength.Id + maxLength.Name + maxLength.Memory + 15));

            for (int i = 0; i < limit; i++)
            {
                var indent = maxLength.Id - processes[i].Id.ToString().Length;
                string id = (indent > 0 ? new string(' ', indent) : string.Empty) + processes[i].Id;

                indent = maxLength.Name - processes[i].ProcessName.Length;
                string name = processes[i].ProcessName + (indent > 0 ? new string(' ', indent) : string.Empty);

                string memory = ToKB(processes[i].WorkingSet64);
                indent = maxLength.Memory - memory.Length;
                memory = (indent > 0 ? new string(' ', indent) : string.Empty) + memory;

                Console.WriteLine($" {id}  |  {name}  |  {memory} KB");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Завершение работы процесса по переданному имени.
        /// </summary>
        /// <param name="name">Имя процесса.</param>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если процесс с переданным именем не найден.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если процесс не удалось завершить.</exception>
        public static void Kill(string name)
        {
            var processes = Process.GetProcessesByName(name);

            if (processes.Length == 0) throw new ProcessNotFoundException("Процесс не найден", name);

            try
            {
                processes[0].Kill();
            }
            catch
            {
                throw new ProcessAccessException("Ошибка доступа к процессу.", processes[0]);
            }
        }

        /// <summary>
        /// Завершение работы процесса по переданному ID.
        /// </summary>
        /// <param name="id">ID процесса.</param>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если процесс с переданным ID не найден.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если процесс не удалось завершить.</exception>
        public static void Kill(int id)
        {
            Process process;

            try
            {
                process = Process.GetProcessById(id);
            }
            catch
            {
                throw new ProcessNotFoundException("Процесс не найден.", id.ToString());
            }

            try
            {
                process.Kill();
            }
            catch
            {
                throw new ProcessAccessException("Ошибка доступа к процессу.", process);
            }
        }

        /// <summary>
        /// Вывод информации о доступных командах, их параметров и допустимых значениях.
        /// </summary>
        public static void Info()
        {
            Console.WriteLine(" -------------------------------------------------------------------");
            Console.WriteLine("| Команды | Параметры | Допустимые значения |        Описание       |");
            Console.WriteLine("|---------|-----------|---------------------|-----------------------|");
            Console.WriteLine("| print   |           |                     |          Вывод списка |");
            Console.WriteLine("|         |           |                     |             процессов |");
            Console.WriteLine("|         |-----------|---------------------|-----------------------|");
            Console.WriteLine("|         |    sort   |                  id |      Сортировка по ID |");
            Console.WriteLine("|         |           |---------------------|-----------------------|");
            Console.WriteLine("|         |           |          id reverse |   Обратная сортировка |");
            Console.WriteLine("|         |           |                     |                 по ID |");
            Console.WriteLine("|         |           |---------------------|-----------------------|");
            Console.WriteLine("|         |           |                name |   Сортировка по имени |");
            Console.WriteLine("|         |           |---------------------|-----------------------|");
            Console.WriteLine("|         |           |        name reverse |   Обратная сортировка |");
            Console.WriteLine("|         |           |                     |              по имени |");
            Console.WriteLine("|         |           |---------------------|-----------------------|");
            Console.WriteLine("|         |           |              memory |         Сортировка по |");
            Console.WriteLine("|         |           |                     |                памяти |");
            Console.WriteLine("|         |           |---------------------|-----------------------|");
            Console.WriteLine("|         |           |      memory reverse |   Обратная сортировка |");
            Console.WriteLine("|         |           |                     |             по памяти |");
            Console.WriteLine("|         |-----------|---------------------|-----------------------|");
            Console.WriteLine("|         |   limit   |   Натуральное число | Колличество выводимых |");
            Console.WriteLine("|         |           |                     |             процессов |");
            Console.WriteLine("|---------|-----------|---------------------|-----------------------|");
            Console.WriteLine("| kill    |           | ID или имя процесса |     Завершение работы |");
            Console.WriteLine("|         |           |                     |              процесса |");
            Console.WriteLine("|---------|-----------|---------------------|-----------------------|");
            Console.WriteLine("| info    |           |                     | Вывод текущей таблицы |");
            Console.WriteLine("|         |-----------|---------------------|-----------------------|");
            Console.WriteLine("|         |           |         ID процесса |      Вывод информации |");
            Console.WriteLine("|         |           |                     |            о процессе |");
            Console.WriteLine("|---------|-----------|---------------------|-----------------------|");
            Console.WriteLine("| exit    |           |                     |   Выход из приложения |");
            Console.WriteLine(" -------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("* Допускается ввод только первой буквы команды, параметра или значения. *");
            Console.WriteLine("*     print sort id reverse limit 10     *     p s i r l 10     *");
            Console.WriteLine("* Приведенные выше команды идентичны. *");
        }

        /// <summary>
        /// Выводит информацию о процессе по переданному ID.
        /// </summary>
        /// <param name="id">ID процесса для вывода информации.</param>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если не удалось найти процесс с переданным ID.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось получить доступ к параметру процесса.</exception>
        public static void Info(int id)
        {
            Process process;

            try
            {
                process = Process.GetProcessById(id);
            }
            catch
            {
                throw new ProcessNotFoundException("Процесс не найден.", id.ToString());
            }

            Info(process);
        }

        /// <summary>
        /// Выводит информацию о процессе по переданному имени.
        /// </summary>
        /// <param name="id">Имя процесса для вывода информации.</param>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если не удалось найти процесс с переданным именем.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось получить доступ к параметру процесса.</exception>
        public static void Info(string name)
        {
            var processes = Process.GetProcessesByName(name);

            if (processes.Length == 0) throw new ProcessNotFoundException("Процесс не найден.", name);

            for (int i = 0; i < processes.Length; i++)
            {
                try
                {
                    Info(processes[i]);
                    break;
                }
                catch (ProcessNotFoundException ex)
                {
                    if (i == processes.Length - 1) throw ex;
                }
                catch (ProcessAccessException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Выводит информацию о переданном процессе.
        /// </summary>
        /// <param name="process">Процесс для вывода информации.</param>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если переданный процесс не инициализирован.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось получить доступ к параметру процесса.</exception>
        public static void Info(Process process)
        {
            if (process == null) throw new ProcessNotFoundException("Процесс не найден.", string.Empty);

            try
            {
                Console.WriteLine("ID ..................................... " + process.Id);
                Console.WriteLine("Имя .................................... " + process.ProcessName);
                Console.WriteLine("ID сессии .............................. " + process.SessionId);
                Console.WriteLine("Имя компьютера ......................... " + process.MachineName);
                Console.WriteLine("Время запуска .......................... " + process.StartTime.ToString("dd.MM.yyyy HH:mm:ss"));
                Console.WriteLine("Размер памяти .......................... " + ToKB(process.WorkingSet64) + " KB");
                Console.WriteLine("Максимально допустимый размер памяти ... " + ToKB(process.MaxWorkingSet.ToInt64()) + " KB");
                Console.WriteLine("Минимально допустимый размер памяти  ... " + ToKB(process.MinWorkingSet.ToInt64()) + " KB");
            }
            catch
            {
                throw new ProcessAccessException("Ошибка доступа к процессу.", process);
            }
        }

        /// <summary>
        /// Вывод списка процессов по переданным параметрам.
        /// </summary>
        /// <param name="options">Массив параметров и значений команды.</param>
        /// <exception cref="OptionNotFoundException">Возбуждается, если команда не содержит указанный параметр.</exception>
        /// <exception cref="IdenticalOptionsException">Возбуждается, если команда содержит два одинаковых параметра.</exception>
        /// <exception cref="InvalidOptionValueException">Возбуждается, если параметр содержит недопустимое значение.</exception>
        static void ExecutePrint(string[] options)
        {
            if (options == null || options.Length == 0)
            {
                Print();
                return;
            }

            (Options Value, bool IsRewrite) sort = (Options.None, false);
            bool reverse = false;
            (int Value, bool IsRewrite) limit = (int.MaxValue, false);

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = options[i].ToLower().Trim();

                if (string.IsNullOrEmpty(options[i].ToLower().Trim())) continue;
                else if (options[i] == "sort" || options[i] == "s")
                {
                    if (sort.IsRewrite) throw new IdenticalOptionsException("Команда не должна содержать идентичные параметры.", "print", "sort");

                    if (options.Length < i + 2 || string.IsNullOrEmpty(options[i + 1]))
                        throw new InvalidOptionValueException("Передано некорректное значение параметра.", "print", "sort", null);

                    i++;

                    var option = options[i].ToLower().Trim();

                    if (option == "name" || option == "n") sort = (Options.Name, true);
                    else if (option == "id" || option == "i") sort = (Options.Id, true);
                    else if (option == "memory" || option == "m") sort = (Options.Memory, true);
                    else throw new InvalidOptionValueException("Передано некорректное значение параметра.", "print", "sort", option[1].ToString());

                    if (options.Length >= i + 2 && (options[i + 1].ToLower().Trim() == "reverse" || options[i + 1].ToLower().Trim() == "r"))
                    {
                        reverse = true;
                        i++;
                    }
                }
                else if (options[i] == "limit" || options[i] == "l")
                {
                    if (limit.IsRewrite) throw new IdenticalOptionsException("Команда не должна содержать идентичные параметры.", "print", "limit");

                    if (options.Length < i + 2 || string.IsNullOrEmpty(options[i + 1]))
                        throw new InvalidOptionValueException("Передано некорректное значение параметра.", "print", "limit", null);

                    i++;

                    if (!int.TryParse(options[i], out int limitValue) || limitValue <= 0)
                        throw new InvalidOptionValueException("Передано некорректное значение параметра.", "print", "limit", limitValue.ToString());
                    limit = (limitValue, true);
                }
                else throw new OptionNotFoundException("Указанный параметр команды не найден.", "print", options[i]);
            }

            Print(sort.Value, reverse, limit.Value);
        }

        /// <summary>
        /// Завершение раюоты процесса по переданному ID или имени.
        /// </summary>
        /// <param name="title">ID или имя процесса для завершения.</param>
        /// <exception cref="InvalidOptionValueException">Возбуждается, если параметр title не инициализирован, пустой или содержит только знаки пробела.</exception>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось завершить процесс с переданным ID или именем.</exception>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если не удалось найти процесс с переданным ID или именем.</exception>
        static void ExecuteKill(string title)
        {
            title = title.Trim();

            if (string.IsNullOrEmpty(title))
                throw new InvalidOptionValueException("Передано некорректное значение параметра.", "kill", string.Empty, null);

            if (int.TryParse(title, out int id))
                Kill(id);
            else
                Kill(title);
        }

        /// <summary>
        /// Вывод информации о процессе по переданному ID или имени. 
        /// </summary>
        /// <param name="title">ID или имя процесса для вывода информации.
        /// Если параметр title не инициализирован, пустой или содержит только знаки пробела, выводится список команд.</param>
        /// <exception cref="ProcessAccessException">Возбуждается, если не удалось завершить процесс с переданным ID или именем.</exception>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если не удалось найти процесс с переданным ID или именем.</exception>
        static void ExecuteInfo(string title)
        {
            title = title.Trim();

            if (string.IsNullOrEmpty(title))
            {
                Info();
                return;
            }

            if (int.TryParse(title, out int id))
                Info(id);
            else
                Info(title);
        }

        /// <summary>
        /// Завршение работы приложения.
        /// </summary>
        /// <param name="title">Параметры введенной команды.</param>
        /// <exception cref="OptionNotFoundException">Возбуждается, если с командой exit переданы дополнительные параметры.</exception>
        static void Exit(string title)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
                throw new OptionNotFoundException("Параметр пустой или не найден.", "exit", title);

            Console.Write("Вы уверены, что хотите завершить работу приложения (Y/N): ");
            var key = Console.ReadKey(true);

            while (true)
            {
                if (key.Key == ConsoleKey.Y || char.ToLower(key.KeyChar) == 'н')
                    Process.GetCurrentProcess().Kill();
                else if (key.Key == ConsoleKey.N || char.ToLower(key.KeyChar) == 'т')
                    break;
            }

            Console.WriteLine(key.KeyChar);
        }

        /// <summary>
        /// Получение макимальных длин полей процессов из массива.
        /// </summary>
        /// <param name="processes">Массив процессов для подсчета максимальных длин полей.</param>
        /// <returns>Кортеж с максимальными длинами полей процессов из переданногомассива</returns>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если переданный массив процессов пустой, не инициализирован или содержит неинициализированный элемент.</exception>
        static (int Id, int Name, int Memory) GetMaxLengthsItems(Process[] processes)
        {
            if (processes == null || processes.Length == 0)
                throw new ProcessNotFoundException("Массив процессов не инициализирован или пустой.", null);

            if (Array.IndexOf(processes, null) != -1)
                throw new ProcessNotFoundException("Массив процессов содержит не инициализированный элемент.", null);

            if (processes.Length == 1)
                return (processes[0].Id.ToString().Length, processes[0].ProcessName.Length, ToKB(processes[0].WorkingSet64).Length);

            int[] lengths = new int[3];

            var id = processes.OrderByDescending(p => p.Id.ToString().Length).ToArray()[0].Id.ToString().Length;
            var name = processes.OrderByDescending(p => p.ProcessName.Length).ToArray()[0].ProcessName.Length;
            var memory = ToKB(processes.OrderByDescending(p => p.WorkingSet64.ToString().Length).ToArray()[0].WorkingSet64).Length;

            return (id, name, memory);
        }

        /// <summary>
        /// Сортировка массива процессов по переданным параметрам.
        /// </summary>
        /// <param name="processes">Массив для сортировки.</param>
        /// <param name="option">Параметр, по которому сортируется массив.</param>
        /// <param name="reverse">Произвести сортировку в обратном порядке.</param>
        /// <returns>Отсортированны массив процессов.</returns>
        /// <exception cref="ProcessNotFoundException">Возбуждается, если переданный массив процессов пустой, не инициализирован или содержит неинициализированный элемент.</exception>
        static Process[] OrderBy(Process[] processes, Options option, bool reverse = false)
        {
            if (processes == null || processes.Length == 0)
                throw new ProcessNotFoundException("Массив процессов не инициализирован или пустой.", null);

            if (Array.IndexOf(processes, null) != -1)
                throw new ProcessNotFoundException("Массив процессов содержит не инициализированный элемент.", null);

            switch (option)
            {
                case Options.Name:
                    processes = processes.OrderBy(p => p.ProcessName).ToArray();
                    break;
                case Options.Id:
                    processes = processes.OrderBy(p => p.Id).ToArray();
                    break;
                case Options.Memory:
                    processes = processes.OrderBy(p => p.WorkingSet64).ToArray();
                    break;
            }

            if (reverse) Array.Reverse(processes);

            return processes;
        }

        /// <summary>
        /// Удаляет первый элемент массива.
        /// </summary>
        /// <param name="array">Массива для удалаения первого элемента.</param>
        /// <returns>Новый массив с удаленным первым элементом из переданного массива.</returns>
        static string[] Shift(string[] array)
        {
            if (array == null || array.Length == 0) return array;
            if (array.Length == 1) return new string[0];

            var result = new string[array.Length - 1];
            Array.Copy(array, 1, result, 0, array.Length - 1);

            return result;
        }

        /// <summary>
        /// Переводит байты в килобайты и преобразует результат в форматированную строку.
        /// </summary>
        /// <param name="value">Колличество байт.</param>
        /// <returns>Форматированная строка с коллиством килобайт.</returns>
        static string ToKB(long value)
        {
            return (value / 1024).ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
        }
    }
}
