using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{    public class F
    {
        public int i1, i2, i3, i4, i5;
        public F()
        {

        }
        public override string ToString()
        {
            var type = typeof(F);
            string str = string.Empty;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                str += $"{field.Name}:{field.GetValue(this)};";
            }
            str = $"{{{str[..^1]}}}";
            return str;
        }
    }
}
