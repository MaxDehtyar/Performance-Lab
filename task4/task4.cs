using System;
using static System.Runtime.InteropServices.JavaScript.JSType;


class Program
{
    static void Main()
    {
        // Пути к файлам
        string valuesFilePath = "Array elements.txt";

        // Обработка данных
        NumberMoves(valuesFilePath);
    }

    static void NumberMoves(string valuesFilePath)
    {
        // Чтение данных из Array elements.txt
        string values = File.ReadAllText(valuesFilePath);

        // Разбитие на элементы
        string[] stringNumbers = values.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Массив того же размера что и полученный ранее
        int[] numbers = new int[stringNumbers.Length];

        // Заполнение массива
        for (int i = 0; i < stringNumbers.Length; i++)
        {
            // Преобразование строки в int
            numbers[i] = int.Parse(stringNumbers[i]);
        }

        float arithmeticMean = 0;
        foreach (var figure in numbers)
        {
            arithmeticMean = arithmeticMean + figure; //считаем сумму всех элементов массива
        }

        arithmeticMean = (float)Math.Round(arithmeticMean / numbers.Length);  // считаем среднее арифметическое, округлённое в большую сторону
        int counter=0;

        while (AreAllElementsEqual(numbers) == false)
        {
            recalculatElements(numbers, arithmeticMean);
            counter++;
        } 

        // Вывод счетчика - количества ходов
        Console.WriteLine(counter);
    }
    // Коректируем элементы массива
    static void recalculatElements(int[] numbers, float arithmeticMean)
    {
         for (int i=0;i< numbers.Length;i++)
         {
            if (numbers [i]< arithmeticMean)
            {
                numbers[i]++;
                break; //сперва думал что за ход все элементы массива можно менять на 1 за 1 шаг
            }
            else
            {
                if(numbers[i] != arithmeticMean)
                {
                    numbers[i]--;
                    break;
                }
            }
                
         }
    }
    // Проверяем все ли элементы равны
    static bool AreAllElementsEqual(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            return true; // Пустой массив или null считаются состоящими из одинаковых элементов
        }

        int firstElement = array[0];

        foreach (int element in array)
        {
            if (element != firstElement)
            {
                return false; // Найден элемент, не равный первому
            }
        }

        return true; // Все элементы равны первому элементу
    }
}
