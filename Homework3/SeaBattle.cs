using System;

namespace Homework3
{
    /// <summary>
    /// Морской бой.
    /// </summary>
    public static class SeaBattle
    {
        /// <summary>
        /// Длина стороны игрового поля по осям X и Y.
        /// </summary>
        private const int lengthSide = 10;

        /// <summary>
        /// Массив, содержащий результат заполнения клеток игрового поля.
        /// </summary>
        private static int[,] field;

        /// <summary>
        /// Создание и заполнение игрового поля.
        /// </summary>
        private static void CreateField()
        {
            int currentDecksNumber = 4;
            int typesShipsNumber = 4;
            Random random = new Random();

            for (int shipsNumber = 1; shipsNumber <= typesShipsNumber; shipsNumber++)
            {
                for (int i = 0; i < shipsNumber; i++)
                {
                    bool check = true;
                    int x, y, axis, direction;

                    do
                    {
                        x = random.Next(0, lengthSide);
                        y = random.Next(0, lengthSide);
                        axis = random.Next(0, 2);
                        direction = random.Next(0, 2) == 1 ? 1 : -1;
                        int tempX = x;
                        int tempY = y;

                        for (int j = 0; j < currentDecksNumber; j++)
                        {
                            check = CheckCell(tempX, tempY);

                            if (!check)
                            {
                                break;
                            }

                            if (axis == 0)
                            {
                                tempX += direction;
                            }
                            else
                            {
                                tempY += direction;
                            }
                        }
                    }
                    while (!check);

                    for (int j = 0; j < currentDecksNumber; j++)
                    {
                        field[x, y] = 1;

                        if (axis == 0)
                        {
                            x += direction;
                        }
                        else
                        {
                            y += direction;
                        }
                    }
                }

                currentDecksNumber--;
            }
        }

        /// <summary>
        /// Проверка клетки игрового поля.
        /// </summary>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Возвращает true, если клетка и клетки вокруг нее свободны.</returns>
        private static bool CheckCell(int x, int y)
        {
            if (x < 0 || x >= lengthSide || y < 0 || y >= lengthSide || field[x, y] == 1)
            {
                return false;
            }

            (int, int)[] displacements = new (int, int)[] { (0, 1), (1, 1),
                (1, 0), (1, -1), (0, -1), (-1, -1),  (-1, 0), (-1, 1) };

            foreach (var displacement in displacements)
            {
                int disX = x + displacement.Item1;
                int disY = y + displacement.Item2;

                if (disX < 0 || disX >= lengthSide || disY < 0 || disY >= lengthSide)
                {
                    continue;
                }

                if (field[disX, disY] == 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Вывод игрового поля в консоль.
        /// </summary>
        public static void DrawField()
        {
            field = new int[lengthSide, lengthSide];

            string abc = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ";

            CreateField();

            Console.Write("\n    ");
            for (int i = 0; i < lengthSide; i++)
            {
                Console.Write($"{abc[i]} ");
            }
            Console.Write("\n    ");

            for (int i = 0; i < lengthSide * 2 - 1; i++)
            {
                Console.Write("_");
            }

            Console.WriteLine();

            for (int y = 0; y < field.GetLength(1); y++)
            {
                Console.Write($"{y + 1} ");
                if (y < 9)
                {
                    Console.Write(" ");
                }
                Console.Write("|");

                for (int x = 0; x < field.GetLength(0); x++)
                {
                    string cell = field[x, y] == 1 ? "X" : ".";
                    string separator = x == field.GetLength(0) - 1 ? "|" : " ";
                    Console.Write($"{cell}{separator}");
                }
                Console.Write("\n");
            }

            Console.Write("    ");

            for (int i = 0; i < lengthSide * 2 - 1; i++)
            {
                Console.Write("¯");
            }

            Console.WriteLine();
        }
    }
}
