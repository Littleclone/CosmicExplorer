using Cosmic_Explorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Saver
{
    private static string saveFilePath;
    private static string saveFolderPath;

    static Saver()
    {
        // Festlege den Pfad und Dateinamen für die Save-Datei
        saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CosmicExplorer/save_Data.txt";
        saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CosmicExplorer"; // Versteckter Ordner
        //Console.WriteLine(saveFilePath);
        // Überprüfe, ob der versteckte Ordner bereits existiert, wenn nicht, erstelle ihn
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
            // Mache den Ordner versteckt (Windows-spezifisch)
            MakeFolderHidden(saveFolderPath);
        }


        if (!File.Exists(saveFilePath))
        {
            // Wenn die Datei nicht existiert, erstelle sie und füge eine Kategoriezeile hinzu
            using (StreamWriter sw = File.AppendText(saveFilePath))
            {
                sw.WriteLine("### SAVE DATA ###" + "\n" +
                             "DELETE NOTHING EXCEPT THE DEV TELL YOU TO DO IT\nSaveFileVersion: 0.5");
            }
        }

    }
    public static void MakeFolderHidden(string folderPath)
    {
        // Windows-spezifischer Befehl, um den Ordner als versteckt zu markieren
        File.SetAttributes(folderPath, File.GetAttributes(folderPath) | FileAttributes.Hidden);
    }

    public static void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            // Wenn die Datei gelöscht wurde, erstelle eine neue
            using (StreamWriter sw = File.AppendText(saveFilePath))
            {
                sw.WriteLine("### SAVE DATA ###" + "\n" +
                             "DELETE NOTHING EXCEPT THE DEV TELL YOU TO DO IT");
            }
        }
    }

    public static void Save(string category, string key, object value)
    {
        string dataType = value.GetType().ToString();
        string dataValue = value.ToString();

        List<string> lines = File.ReadLines(saveFilePath).ToList();
        bool entryExists = false;

        // Überprüfe, ob ein Eintrag mit derselben Kategorie und demselben Schlüssel bereits existiert
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith("[" + category + "] [" + key + "]"))
            {
                // Wenn ein Eintrag gefunden wurde, überschreibe ihn mit dem neuen Wert
                lines[i] = "[" + category + "] [" + key + "] " + dataType + "|" + dataValue;
                entryExists = true;
                break;
            }
        }

        // Wenn kein vorhandener Eintrag gefunden wurde, füge den neuen Eintrag hinzu
        if (!entryExists)
        {
            lines.Add("[" + category + "] [" + key + "] " + dataType + "|" + dataValue);
        }

        // Schreibe die aktualisierten Einträge zurück in die Datei
        File.WriteAllLines(saveFilePath, lines);
    }

    public static T Load<T>(string category, string key)
    {
        List<string> lines = File.ReadLines(saveFilePath).ToList();
        string dataType = typeof(T).ToString();
        string decryptedKey = key;
        string decryptedValue = null;

        for (int i = lines.Count - 1; i >= 0; i--)
        {
            var line = lines[i];
            if (line.StartsWith("[" + category + "] [" + key + "]"))
            {
                string encryptedData = line.Substring(line.IndexOf("|") + 1);
                return (T)Convert.ChangeType(encryptedData, typeof(T));
            }
        }
        return default(T);
    }

    // Speichert ein 2D-Array als ein einzelnes Objekt
    public static void SaveArray<T>(string category, T[,] array)
    {
        // Konvertiere das gesamte 2D-Array in eine Zeichenfolge
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                stringBuilder.Append(array[i, j].ToString()).Append(",");
            }
            stringBuilder.AppendLine();
        }

        // Speichere die Zeichenfolge als ein einzelnes Objekt
        Save(category, "ArrayData", stringBuilder.ToString());
    }


    // Lädt ein Array aus den gespeicherten Einträgen, wobei der Schlüssel der Index ist
    public static T[,] LoadArray<T>(string category, int length1, int length2)
    {
        T[,] array = new T[length1,length2];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                array[i,j] = Load<T>(category, i.ToString());
            }
        }
        return array;
    }
}
