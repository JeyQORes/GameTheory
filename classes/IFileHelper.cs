using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.classes
{
    public interface IFileHelper
    {
        public string[] Import(string path);
        public void Export(string[] data, string path);
    }
}
