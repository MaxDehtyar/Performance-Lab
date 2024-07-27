using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Путь и имя файлов
        string filePath1 = "Координаты центра окружности.txt";
        string filePath2 = "Координаты точек.txt";

        // Запись данных в файлы
        File.WriteAllText(filePath1, "1 1\n5");
        File.WriteAllText(filePath2, "0 0\n1 6\n6 6\n2 3\n-6 1");

        // Обработка файлов
        ProcessPoints(filePath1, filePath2);
    }

    static void ProcessPoints(string filePath1, string filePath2)
    {
        try
        {
            // Чтение и парсинг центра окружности и радиуса
            string[] circleData = File.ReadAllLines(filePath1);
            string[] centerCoords = circleData[0].Split();
            float centerX = float.Parse(centerCoords[0]);
            float centerY = float.Parse(centerCoords[1]);
            float radius = float.Parse(circleData[1]);

            // Чтение и парсинг точек
            string[] pointLines = File.ReadAllLines(filePath2);
            foreach (string line in pointLines)
            {
                string[] pointCoords = line.Split();
                float pointX = float.Parse(pointCoords[0]);
                float pointY = float.Parse(pointCoords[1]);

                // Расстояние от центра окружности до точки
                float distance = (float)Math.Sqrt(Math.Pow(pointX - centerX, 2) + Math.Pow(pointY - centerY, 2));

                // Определение положения точки относительно окружности
                if (distance < radius)
                {
                    Console.WriteLine("1"); // Точка внутри окружности
                }
                else if (Math.Abs(distance - radius) < 0.00001) // Учитываем возможные погрешности вычислений
                {
                    Console.WriteLine("0"); // Точка на окружности
                }
                else
                {
                    Console.WriteLine("2"); // Точка снаружи окружности
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
