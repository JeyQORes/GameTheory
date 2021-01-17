using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Practice.classes
{
    class FileJSON : IFileHelper
    {
        public string[] Import(string path)
        {
            string json;
            using (var stream = new StreamReader(path))
            {
                json = stream.ReadToEnd();
            }
            try
            {
                var result = (string[])JsonSerializer.Deserialize(json, typeof(string[]));
                foreach (var str in result)
                {
                    var checkArray = str.Split(' ');
                    foreach (var item in checkArray)
                    {
                        if (!IsDigit(item))
                            throw new Exception();
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Файл некоректный");
            }
        }
        public void Export(string[] data, string path)
        {
            string json = JsonSerializer.Serialize<string[]>(data);
            using (var stream = new StreamWriter(path))
            {
                stream.Write(json);
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
