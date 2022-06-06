using System;
using System.Diagnostics;
using System.Linq;

namespace Homework6
{
    public static class Program
    {
        /// <summary>
        /// Точка входа в приложение. Командная строка.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var info = "\nДля вывода списка команд, параметров и допустимых значений введите info.";

            Console.WriteLine("Добро пожаловать в Task Manager!\n");
            TaskManager.Info();

            while (true)
            {
                Console.Write("\nВведите команду: ");
                try
                {
                    var command = Console.ReadLine();
                    Console.WriteLine();
                    TaskManager.Execute(command);
                }
                catch (CommandNotFoundException ex)
                {
                    if (ex.Command == null) Console.WriteLine("Введите команду." + info);
                    else Console.WriteLine($"Команда {ex.Command} не найдена." + info);
                }
                catch (OptionNotFoundException ex)
                {
                    if (ex.Option == null) Console.WriteLine($"Не указан параметр для команды {ex.Command}." + info);
                    else Console.WriteLine($"Команда {ex.Command} не содержит параметра {ex.Option}." + info);
                }
                catch (IdenticalOptionsException ex)
                {
                    Console.WriteLine($"Команда {ex.Command} не может содержать более одного параметра {ex.Option}." + info);
                }
                catch (InvalidOptionValueException ex)
                {
                    if (ex.Value == null) Console.WriteLine($"Не указано значение параметра {ex.Option} для команды {ex.Command}." + info);
                    else Console.WriteLine($"Указано недопустимое значение {ex.Value} для параметра {ex.Option} команды {ex.Command}." + info);
                }
                catch (ProcessNotFoundException ex)
                {
                    Console.WriteLine($"Процесс {ex.Process} не найден.");
                }
                catch (ProcessAccessException ex)
                {
                    Console.WriteLine($"Нет доступа к процессу {ex.Process.Id} или его параметру.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
