using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace html_parser
{
    interface IFile
    {
        public string Clean(string text);
        public Dictionary<string, int> Count(string text);
        public void Show(dynamic list);
    }

    class html : IFile
    {
        public string Clean(string text)
        {
            text = Regex.Replace(text, @"<[^>]*>", " ");// удаление хтмл тегов
            text = Regex.Replace(text, @"[&.+;]", " ");//  спецсимволов хтмл
            text = Regex.Replace(text, @"[._^%$#!~@,;:]\s", " ");// знаков препинания
            text = Regex.Replace(text, @"[()]", " ");// скобок
            text = Regex.Replace(text, @"\s-\s", " ");// тире
            text = Regex.Replace(text, @"\s+", " ");// лишних пробелов

            return text;
        }

        public Dictionary<string, int> Count(string text)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            int script = 0;
            try
            {
                foreach (string line in File.ReadLines(text))
                {
                    string temp;

                    if (line.Contains("</script>") || line.Contains("</style>"))//пропуск <script> и <style>
                    {
                        script = 0;
                    }
                    else if ((line.Contains("<script>")) || ((line.Contains("<style>")) || script > 0))
                    {
                        script++;
                    }
                    else
                    {
                        temp = line.ToUpperInvariant();
                        temp = Clean(temp);
                        string[] words = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in words)
                        {
                            if (!list.ContainsKey(word))
                            {
                                list.Add(word, 1);
                            }
                            else
                            {
                                list[word] += 1;
                            }
                        }
                    }


                }

                list = list.OrderByDescending(x => x.Value).ToDictionary(z => z.Key, y => y.Value);

                return list;
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Environment.Exit(-1);
                return list;
            }

        }
        public void Show(dynamic words)
        {
            foreach (var word in words)
            {
                Console.WriteLine(word.Key + "-" + word.Value);
            }

        }
    }
}