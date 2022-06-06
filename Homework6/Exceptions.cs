using System;
using System.Diagnostics;

namespace Homework6
{
    /// <summary>
    /// Команда с переданным именем не найдена или пустая.
    /// </summary>
    public class CommandNotFoundException : Exception
    {
        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Команда не указана или такой команды нет.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="command">Имя команды.</param>
        public CommandNotFoundException(string message, string command) : base(message)
        {
            Command = command;
        }
    }

    /// <summary>
    /// Параметр команды не указан или команда не содержит такого параметра.
    /// </summary>
    public class OptionNotFoundException : Exception
    {
        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Имя параметра. Null, если не указзан.
        /// </summary>
        public string Option { get; private set; }

        /// <summary>
        /// Параметр команды не указан или команда не содержит такого параметра.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="command">Имя команды.</param>
        /// <param name="option">Имя параметра.</param>
        public OptionNotFoundException(string message, string command, string option) : base(message)
        {
            Command = command;
            Option = option;
        }
    }

    /// <summary>
    /// Указано два или более одинаковых параметра.
    /// </summary>
    public class IdenticalOptionsException : Exception
    {
        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Имя параметра.
        /// </summary>
        public string Option { get; private set; }

        /// <summary>
        /// Указано два или более одинаковых параметра.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="command">Имя команды.</param>
        /// <param name="option">Имя параметра.</param>
        public IdenticalOptionsException(string message, string command, string option) : base(message)
        {
            Command = command;
            Option = option;
        }
    }

    /// <summary>
    /// Указно недопустимое значение для параметра.
    /// </summary>
    public class InvalidOptionValueException : Exception
    {
        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Имя параметра.
        /// </summary>
        public string Option { get; private set; }

        /// <summary>
        /// Значение параметра. Null, если значение не указано.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Указно недопустимое значение для параметра.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="command">Имя команды.</param>
        /// <param name="option">Имя параметра.</param>
        /// <param name="value">Значение параметра. Null, если значение не указано.</param>
        public InvalidOptionValueException(string message, string command, string option, string value) : base(message)
        {
            Command = command;
            Option = option;
            Value = value;
        }
    }

    /// <summary>
    /// Процесс не найден.
    /// </summary>
    public class ProcessNotFoundException : Exception
    {
        /// <summary>
        /// ID или имя процесса, вызвавший исключение..
        /// </summary>
        public string Process { get; private set; }

        /// <summary>
        /// Процесс не найден.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="process">ID или имя процесса, вызвавший исключение..</param>
        public ProcessNotFoundException(string message, string process) : base(message)
        {
            Process = process;
        }
    }

    /// <summary>
    /// Нет доступа к процессу или его параметру.
    /// </summary>
    public class ProcessAccessException : Exception
    {
        /// <summary>
        /// Процесс, вызвавший исключение.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// Нет доступа к процессу или его параметру.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="process">Процесс, вызвавший исключение.</param>
        public ProcessAccessException(string message, Process process) : base(message)
        {
            Process = process;
        }
    }
}
