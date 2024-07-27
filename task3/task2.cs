using System;
using System.IO;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        // Пути к файлам
        string valuesFilePath = "values.json";
        string testsFilePath = "tests.json";
        string reportFilePath = "report.json";

        // Обработка данных
        ProcessPoints(valuesFilePath, testsFilePath, reportFilePath);
    }

    static void ProcessPoints(string valuesFilePath, string testsFilePath, string reportFilePath)
    {
        // Чтение и парсинг данных из values.json
        string valuesJson = File.ReadAllText(valuesFilePath);
        JObject valuesData = JObject.Parse(valuesJson);

        // Чтение и парсинг данных из tests.json
        string testsJson = File.ReadAllText(testsFilePath);
        JObject testsData = JObject.Parse(testsJson);

        // Преобразование valuesData в словарь для быстрого поиска по id
        var valuesDict = new Dictionary<int, string>();
        foreach (var valueObj in valuesData["values"])
        {
            int id = (int)valueObj["id"];
            string value = (string)valueObj["value"];
            valuesDict[id] = value;
        }

        // Рекурсивное обновление полей value в testsData
        UpdateValues(testsData, valuesDict);

        // Запись обновленных данных в report.json
        File.WriteAllText(reportFilePath, testsData.ToString());
    }

    static void UpdateValues(JToken node, Dictionary<int, string> valuesDict)
    {
        if (node is JObject)
        {
            var obj = (JObject)node;
            if (obj["id"] != null && obj["value"] != null && string.IsNullOrEmpty((string)obj["value"]))
            {
                int id = (int)obj["id"];
                if (valuesDict.TryGetValue(id, out string value))
                {
                    obj["value"] = value;
                }
            }

            // Рекурсивный вызов для всех свойств объекта
            foreach (var property in obj.Properties())
            {
                UpdateValues(property.Value, valuesDict);
            }
        }
        else if (node is JArray)
        {
            foreach (var item in (JArray)node)
            {
                UpdateValues(item, valuesDict);
            }
        }
    }
}
