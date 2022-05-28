using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Homework5
{
    /// <summary>
    /// Органайзер. Класс для работы со списком задач.
    /// </summary>
    public class Organizer
    {
        /// <summary>
        /// Текущее колличество задач в списке.
        /// </summary>
        public int ToDoCount => toDoList == null ? 0 : toDoList.Count;

        /// <summary>
        /// Список задач.
        /// </summary>
        List<ToDo> toDoList;

        /// <summary>
        /// Конструктор для объекта с пустым списком задач.
        /// </summary>
        public Organizer()
        {

        }

        /// <summary>
        /// Конструктор для объекта с загруженным списком задач.
        /// </summary>
        /// <param name="fileName">Файл для чтения списка задач.</param>
        public Organizer(string fileName)
        {
            LoadToDoListFromFile(fileName);
        }

        /// <summary>
        /// Добавить новую задачу в список.
        /// </summary>
        /// <param name="toDo">Задача для добавления в список.</param>
        public void AddToDo(ToDo toDo)
        {
            if (toDoList == null)
                toDoList = new List<ToDo>();

            if (toDoList.Exists(t => t == toDo))
                return;

            toDoList.Add(toDo);
        }

        /// <summary>
        /// Удаление задачи из списка по индексу.
        /// </summary>
        /// <param name="index">Индекс задачи для удаления.</param>
        public void RemoveToDoAt(int index)
        {
            if (toDoList == null || toDoList.Count == 0 || index < 0 || index >= toDoList.Count)
                return;

            toDoList.RemoveAt(index);
        }

        /// <summary>
        /// Изменение статуса задачи из списка по индексу.
        /// </summary>
        /// <param name="index">Индекс задачи для изменения статуса.</param>
        public void ChangeStatusToDoAt(int index)
        {
            if (toDoList == null || toDoList.Count == 0 || index < 0 || index >= toDoList.Count)
                return;

            toDoList[index].IsDone = !toDoList[index].IsDone;
        }

        /// <summary>
        /// Сохранение списка задач в файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения.</param>
        public void SaveToDoListToFile(string fileName)
        {
            try
            {
                File.WriteAllText(fileName, JsonSerializer.Serialize(toDoList));
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Загрузка списка задач из файла.
        /// </summary>
        /// <param name="fileName">Путь к файлу для чтения списка задач.</param>
        public void LoadToDoListFromFile(string fileName)
        {
            if (fileName == null || !File.Exists(fileName))
                return;

            try
            {
                toDoList = JsonSerializer.Deserialize<List<ToDo>>(File.ReadAllText(fileName));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Вывод списка задач на экран.
        /// </summary>
        /// <returns>Строка со списком задач.</returns>
        public override string ToString()
        {
            string result = "Список задач:\n\n";

            if (toDoList == null || toDoList.Count == 0)
                return result + "\tСписок задач пуст.";
            
            for (int i = 0; i < toDoList.Count; i++)
                result += $"\t[{(toDoList[i].IsDone ? "x" : " ")}] {i + 1}. {toDoList[i].Title}\n";

            return result;
        }
    }
}
