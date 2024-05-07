using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CosmicExplorer
{
    public static class DataManager
    {
        public static readonly string saveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CosmicExplorer");
        private static readonly string saveFilePath = Path.Combine(saveDirectory, "save_data.txt");
        private static readonly string header = "Save Data Version:0.1";

        static DataManager()
        {
            InitializeSaveFile();
        }

        private static void InitializeSaveFile()
        {
            if (!File.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveDirectory);
                using (StreamWriter writer = File.CreateText(saveFilePath))
                {
                    writer.WriteLine(header);
                }
            }
        }

        public static void SaveData<T>(string category, string key, T data)
        {
            string saveString = $"{category},{key},{JsonConvert.SerializeObject(data)}";

            using (StreamWriter writer = File.AppendText(saveFilePath))
            {
                writer.WriteLine(saveString);
            }
        }

        public static T LoadData<T>(string category, string key)
        {
            string[] lines = File.ReadAllLines(saveFilePath);
            if (lines.Length == 0 || lines[0] != header)
            {
                throw new InvalidDataException("Invalid header in data file.");
            }

            for (int i = 1; i < lines.Length; i++) // Start at index 1 to skip header
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 3 && parts[0] == category && parts[1] == key)
                {
                    string jsonData = string.Join(",", parts, 2, parts.Length - 2);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
            }

            throw new FileNotFoundException($"Data not found for category '{category}' and key '{key}'.");
        }
        public static void DeleteSaveFiles()
        {
            // Pfad zu den Dateien
            string saveDataFilePath = Path.Combine(saveDirectory, "save_data.txt");
            string worldDataFilePath = Path.Combine(saveDirectory, "world_data.txt");

            try
            {
                // Löschen der save_data.txt
                if (File.Exists(saveDataFilePath))
                {
                    File.Delete(saveDataFilePath);
                    Console.WriteLine("save_data.txt wurde gelöscht.");
                }
                else
                {
                    Console.WriteLine("save_data.txt existiert nicht.");
                }

                // Löschen der world_data.txt
                if (File.Exists(worldDataFilePath))
                {
                    File.Delete(worldDataFilePath);
                    Console.WriteLine("world_data.txt wurde gelöscht.");
                }
                else
                {
                    Console.WriteLine("world_data.txt existiert nicht.");
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Fehler beim Löschen der Dateien: {ex.Message}");
            }
            InitializeSaveFile();
        }
    }

    public static class WorldDataManager
    {
        private static readonly string header = "World Data Version:0.1";

        public static void SaveWorldData<T>(T[,] data)
        {
            string filePath = Path.Combine(DataManager.saveDirectory, "world_data.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(header);
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        writer.Write($"{data[i, j]}");
                        if (j < data.GetLength(1) - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }

        public static T[,] LoadWorldData<T>()
        {
            string filePath = Path.Combine(DataManager.saveDirectory, "world_data.txt");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string fileHeader = reader.ReadLine();
                    if (fileHeader != header)
                    {
                        throw new InvalidDataException("Invalid header in world data file.");
                    }

                    List<List<T>> dataList = new List<List<T>>();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines
                        string[] values = line.Split(',');
                        List<T> row = new List<T>();
                        foreach (string value in values)
                        {
                            T enumValue = (T)Enum.Parse(typeof(T), value.Trim());
                            row.Add(enumValue);
                        }
                        dataList.Add(row);
                    }

                    T[,] dataArray = new T[dataList.Count, dataList[0].Count];
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        for (int j = 0; j < dataList[i].Count; j++)
                        {
                            dataArray[i, j] = dataList[i][j];
                        }
                    }

                    return dataArray;
                }
            }
            else
            {
                throw new FileNotFoundException("World data file not found.");
            }
        }
    }
}