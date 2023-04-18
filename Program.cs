using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Получаем путь к папке из файла dir.txt
        string path = File.ReadAllText("dir.txt");

        // Ищем файл Player.log в заданной папке и всех подпапках
        string[] logFiles = Directory.GetFiles(path, "Player.log", SearchOption.AllDirectories);

        Console.Write("Введите ник профиля: ");
        string username = Console.ReadLine()!;
        string keyword = $"\"username\":\"{username}\"";
        bool found = false;

        // Ищем ключевое слово в каждом файле Player.log
        foreach (string file in logFiles)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;
                    if (line.Contains(keyword))
                    {
                        found = true;
                        string folderPath = Path.GetDirectoryName(file)!;
                        System.Diagnostics.Process.Start("explorer.exe", folderPath);
                        break;
                    }
                }
            }
            if (found) break;
        }

        if (!found)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Профиль не найден.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Нажмите любую кнопку для закрытия...");
            Console.ReadKey();
        }
    }
}
