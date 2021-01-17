using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Practice.classes
{
    class FileXML : IFileHelper
    {
        public void Export(string[] data, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(string[]));
            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, data);
            }
        }

        public string[] Import(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(string[]));
            try
            {
                using (var stream = new StreamReader(path))
                {
                    string[] result = (string[])serializer.Deserialize(stream);
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
            }
            catch (Exception)
            {
                throw new Exception("Файл некоректный");
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
