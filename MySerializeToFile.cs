using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{
    internal class MySerializeToFile
    {
        private StreamWriter writer { get; set; }
        public MySerializeToFile(string fileName, object obj) 
        { 
            writer = new StreamWriter(fileName, false);
            var type = obj.GetType();
            string str = string.Empty;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                str += $"{field.Name};";
            }
            str = $"{str[..^1]}";

            writer.WriteLine(str);
        }
        public string SerializeObject(object obj)
        {
            var type = obj.GetType();
            string str = string.Empty;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                str += $"{field.GetValue(obj)};";
            }
            str = $"{str[..^1]}";
            writer.WriteLine(str);
            return str;
        }

        public void CloseFile()
        {
            writer.Close();
        }
    }
}
