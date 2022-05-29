using System;

namespace Homework5
{
    /// <summary>
    /// Класс для описания задачи.
    /// </summary>
    public class ToDo
    {
        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Title
        {
            get => title;
            set => title = (value.Trim() == string.Empty) ? "Без описания" : value;
        }

        /// <summary>
        /// Статус задачи.
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Описание задачи.
        /// </summary>
        string title;

        /// <summary>
        /// Консруктор по умолчанию.
        /// </summary>
        public ToDo()
        {
        }

        /// <summary>
        /// Конструктор класса с указанием описания и статуса задачи.
        /// </summary>
        /// <param name="title">Описание задачи.</param>
        /// <param name="isDone">Статус задачи.</param>
        public ToDo(string title, bool isDone = false)
        {
            Title = title;
            IsDone = isDone;
        }
    }
}
