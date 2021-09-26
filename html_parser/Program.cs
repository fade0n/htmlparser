using System;
using System.Collections.Generic;
using System.IO;

namespace html_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string text; string extension;

            Dictionary<string, int> words = new Dictionary<string, int>(); ;

            db_work a = new db_work(); ;

            Console.Write("Путь к файлу: ");
            text = Console.ReadLine();
            FileInfo path = new System.IO.FileInfo(text);

            extension = Path.GetExtension(text);
            switch (extension)
            {
                case ".html":
                    {
                        html html = new html();
                        words = html.Count(text);
                        html.Show(words);
                        a.insert(words);
                        Console.ReadKey();
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Неверный формат");
                        Console.ReadKey();
                        return;
                    }
            }

        }
    }
}