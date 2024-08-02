using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Cosmic_Explorer;
using Newtonsoft.Json;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.



// For transparency, I have to say that I have not written this code myself. I let ChatGPT generate it for me (it needed 4 Hours to get a working Code). I have only made minor changes to the code.
// But Everything else in this Project is written by Myself.

namespace CosmicExplorer
{
    public static class DataManager
    {
        public static readonly string saveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CosmicExplorer");
        private static readonly string saveFilePath = Path.Combine(saveDirectory, "save_data.txt");
        private static readonly string header = "Save Data Version:0.1";

        static DataManager()
        {
            InitializeSaveFileFirst();
        }
        private static Game game;
        public static void Init(Game game1)
        {
            game = game1;
        }

        public static void InitializeSaveFileFirst()
        {
            if (!File.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveDirectory);
                using (StreamWriter writer = File.CreateText(saveFilePath))
                {
                    writer.WriteLine(header);
                    writer.WriteLine("saveFile,newGame,true");
                }
            }
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
                Console.ForegroundColor = ConsoleColor.Red;
                // Löschen der save_data.txt
                if (File.Exists(saveDataFilePath))
                {
                    File.Delete(saveDataFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("save_data.txt wurde gelöscht.");
                    }
                }
                else
                {
                    if(game.debug)
                    {
                        Console.WriteLine("save_data.txt existiert nicht.");
                    }
                }

                // Löschen der world_data.txt
                if (File.Exists(worldDataFilePath))
                {
                    File.Delete(worldDataFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("world_data.txt wurde gelöscht.");
                    }
                }
                else
                {
                    if (game.debug)
                    {
                        Console.WriteLine("world_data.txt existiert nicht.");
                    }
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Löschen der Dateien: {ex.Message}");
                Console.ResetColor();
            }
            InitializeSaveFileFirst();
        }
        public static void BackupSaveFiles()
        {
            // Pfad zu den Dateien
            string saveDataFilePath = Path.Combine(saveDirectory, "save_data.txt");
            string worldDataFilePath = Path.Combine(saveDirectory, "world_data.txt");
            string saveDataBackupFilePath = Path.Combine(saveDirectory, "save_dataBACKUP.txt");
            string worldDataBackupFilePath = Path.Combine(saveDirectory, "world_dataBACKUP.txt");

            try
            {
                // Löschen der vorhandenen Backup-Dateien, falls vorhanden
                Console.ForegroundColor = ConsoleColor.Red;
                if (File.Exists(saveDataBackupFilePath))
                {
                    File.Delete(saveDataBackupFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("Vorhandenes Backup von save_data.txt wurde gelöscht.");
                    }
                }
                if (File.Exists(worldDataBackupFilePath))
                {
                    File.Delete(worldDataBackupFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("Vorhandenes Backup von world_data.txt wurde gelöscht.");
                    }
                }

                // Umbenennen der aktuellen Dateien zu Backup-Dateien
                if (File.Exists(saveDataFilePath))
                {
                    File.Move(saveDataFilePath, saveDataBackupFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("save_data.txt wurde zu save_dataBACKUP.txt umbenannt.");
                    }
                }
                else
                {
                    if(game.debug)
                    {
                        Console.WriteLine("save_data.txt existiert nicht.");
                    }
                }

                if (File.Exists(worldDataFilePath))
                {
                    File.Move(worldDataFilePath, worldDataBackupFilePath);
                    if (game.debug)
                    {
                        Console.WriteLine("world_data.txt wurde zu world_dataBACKUP.txt umbenannt.");
                    }
                }
                else
                {
                    if(game.debug)
                    {
                        Console.WriteLine("world_data.txt existiert nicht.");
                    }
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Erstellen der Backup-Dateien: {ex.Message}");
                Console.ResetColor();
            }
            InitializeSaveFile();
        }

        public static void RestoreBackupFiles()
        {
            // Pfad zu den Dateien
            string saveDataFilePath = Path.Combine(saveDirectory, "save_data.txt");
            string worldDataFilePath = Path.Combine(saveDirectory, "world_data.txt");
            string saveDataBackupFilePath = Path.Combine(saveDirectory, "save_dataBACKUP.txt");
            string worldDataBackupFilePath = Path.Combine(saveDirectory, "world_dataBACKUP.txt");

            try
            {
                if(File.Exists(saveDataFilePath))
                {
                    File.Delete(saveDataFilePath);
                }
                if(File.Exists(worldDataFilePath))
                {
                    File.Delete(worldDataFilePath);
                }
                // Umbenennen der Backup-Dateien zu den aktuellen Dateinamen
                Console.ForegroundColor = ConsoleColor.Red;
                if (File.Exists(saveDataBackupFilePath))
                {
                    File.Move(saveDataBackupFilePath, saveDataFilePath);
                    if(game.debug)
                    {
                        Console.WriteLine("save_dataBACKUP.txt wurde zu save_data.txt zurückbenannt.");
                    }
                }
                else
                {
                    if(game.debug)
                    {
                        Console.WriteLine("Kein Backup von save_data.txt vorhanden.");
                    }
                    else
                    {
                        Console.WriteLine("Es wurde kein Backup für die Speicherdatei gefunden!");
                    }
                }

                if (File.Exists(worldDataBackupFilePath))
                {
                    File.Move(worldDataBackupFilePath, worldDataFilePath);
                    if (game.debug)
                    {
                        Console.WriteLine("world_dataBACKUP.txt wurde zu world_data.txt zurückbenannt.");
                    }
                }
                else
                {
                    if (game.debug)
                    {
                        Console.WriteLine("Kein Backup von world_data.txt vorhanden.");
                    }
                    else
                    {
                        Console.WriteLine("Es wurde kein Backup für die Weltdatei gefunden!");
                    }
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Wiederherstellen der Backup-Dateien: {ex.Message}");
                Console.ResetColor();
            }
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