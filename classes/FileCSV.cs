using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Practice.classes
{
    class FileCSV : IFileHelper
    {
        public string[] Import(string path)
        {
            List<String> array = new List<string>();

            try
            {
                using (var file = new StreamReader(path))
                {
                    string s;
                    while ((s = file.ReadLine()) != null)
                    {
                        array.Add(s);
                    }
                }
                int index = 0;
                for (int i = 0; i < array.Count; i++)
                {
                    array[i] = array[i].Replace(';', ' ');
                    var checkArray = array[i].Split(' ');
                    foreach (var item in checkArray)
                    {
                        if (!IsDigit(item))
                        {
                            throw new Exception("Файл некоректный");
                        }
                    }
                }
                return array.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Export(string[] data, string path)
        {
            using(var stream = new StreamWriter(path))
            {
                foreach (var str in data)
                {
                    var line = str.Split(' ');
                    for (int i = 0; i < line.Length-1; i++)
                    {
                        stream.Write($"{line[i]};");
                    }
                    stream.WriteLine($"{line[line.Length - 1]}");
                }
            }
            
        }
        private bool IsDigit(string item)
        {
            foreach (var digit in item)
            {
                if (!Char.IsDigit(digit))
                {
                    return false;
                }
            }
            if (item.ToString() == "")
                return false;
            return true;
        }

    }
}
