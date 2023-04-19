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
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔═══╗╔═══╗╔═══╗╔═══╗╔══╗╔╗───╔═══╗     ╔═══╗╔══╗╔═╗─╔╗╔═══╗╔═══╗╔═══╗\r\n║╔═╗║║╔═╗║║╔═╗║║╔══╝╚╣─╝║║───║╔══╝     ║╔══╝╚╣─╝║║╚╗║║╚╗╔╗║║╔══╝║╔═╗║\r\n║╚═╝║║╚═╝║║║─║║║╚══╗─║║─║║───║╚══╗     ║╚══╗─║║─║╔╗╚╝║─║║║║║╚══╗║╚═╝║\r\n║╔══╝║╔╗╔╝║║─║║║╔══╝─║║─║║─╔╗║╔══╝     ║╔══╝─║║─║║╚╗║║─║║║║║╔══╝║╔╗╔╝\r\n║║───║║║╚╗║╚═╝║║║───╔╣─╗║╚═╝║║╚══╗     ║║───╔╣─╗║║─║║║╔╝╚╝║║╚══╗║║║╚╗\r\n╚╝───╚╝╚═╝╚═══╝╚╝───╚══╝╚═══╝╚═══╝     ╚╝───╚══╝╚╝─╚═╝╚═══╝╚═══╝╚╝╚═╝");
        Console.Write("\n");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Вводите ник без пробелов и ошибок. Так же можно просто скопировать ник с рабочей таблицы и вставить в консоль.\n");

        string username;
        bool found;

        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите ник профиля: ");
            username = Console.ReadLine()!;
            string keyword = $"\"username\":\"{username}\"";
            found = false;

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
                Console.WriteLine("\nПрофиль не найден.\n\nВозможно профиль не находится в папке с Bv Gamer.\nПопробуйте активировать профиль \"Эмуляция отключена\", в Bv Gamer Tools, и повторить попытку.\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Желаете повторить ввод ника? (Y/N): ");
                string answer = Console.ReadLine()!.Trim().ToLower();
                if (answer != "y") return;
            }

        } while (!found);

        Console.ForegroundColor = ConsoleColor.Green;
        Environment.Exit(0);
    }
}
