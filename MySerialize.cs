using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{
    public class MySerialize
    {
        public string SerializeObject(object obj)
        {
            var type = obj.GetType();
            string str = string.Empty;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                str += $"{field.Name}:{field.GetValue(obj)},";
            }
            str = $"{{{str[..^1]}}}";
            return str;
        }
    }
}
