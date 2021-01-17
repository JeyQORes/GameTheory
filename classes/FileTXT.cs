using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Practice.classes
{
    class FileTXT : IFileHelper
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
                foreach (var str in array)
                {
                    var checkArray = str.Split(' ');
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
                foreach (var line in data)
                {
                    stream.WriteLine(line);
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
