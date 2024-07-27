Console.WriteLine("Введите размер массива:");
int n = int.Parse(Console.ReadLine());

Console.WriteLine("Введите длину обхода:");
int m = int.Parse(Console.ReadLine());

//создём исходный массив
int[] mass = new int[n];

// Заполняем массив числами от 1 до размера массива
for (int i = 0; i < n; i++)
{
    mass[i] = i + 1; 
}

//конечный путь
string way = "";

//текущая стартовая позиция интервала
int current_start = 0;
//последняя позиция текущего интервала
int current_last = 0;

//поиск интервалов
do
{
    //создём массив для интервала
    int[] mass_interval = new int[m];
    for (int i = 0; i < m; i++)
    {
        mass_interval[i] = mass[current_start];
        current_start++;

        if (current_start == n) //если дошли до конца начального массива, возвращаемся в его начало
            current_start = 0;
    }
    current_last = mass_interval[m-1]; //запоминаем значение последнего элемента интервала
    current_start = current_last-1; //задаём это значение для вледующего интервала (-1, т.к. в mass элемент со значением 3, будет под индексом 2

    way = way + mass_interval[0].ToString(); //добавляем начало интервала в конечный путь
    
} while (current_last != mass[0]); //перебор пока последний элемент интервала не будет = первому в заданном массиве

// Выводим путь в консоль
Console.WriteLine("Путь: " + way);


