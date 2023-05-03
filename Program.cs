using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// See https://aka.ms/new-console-template for more information
int countIteration = 1000;
var sw = new Stopwatch();
var mc = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };// new MyClass();
string str = string.Empty;
sw.Start();
for (var i = 1; i <= countIteration; i++)
{
    str += $"{MySerialize(mc)};";
}
str = str[0..^1];
sw.Stop();
Console.WriteLine(" My serialized time");
Console.WriteLine(sw.Elapsed);
//Console.WriteLine(str);
sw.Restart();
var s = MyDeserialize(str); 
sw.Stop();
Console.WriteLine(" My deserialized time");
Console.WriteLine(sw.Elapsed);
//s.ForEach(Console.WriteLine);

str = string.Empty;
sw.Restart();
for (var i = 1; i <= countIteration; i++)
{
    str += $"{JsonConvert.SerializeObject(mc)};";
}
str = str[0..^1];
sw.Stop();
Console.WriteLine(" Newtonsoft serialized time");
Console.WriteLine(sw.Elapsed);
//Console.WriteLine(str);
sw.Restart();
s = NewtonsoftDeserialize(str);
sw.Stop();
Console.WriteLine(" Newtonsoft deserialized time");
Console.WriteLine(sw.Elapsed);
//s.ForEach(Console.WriteLine);

string MySerialize(object obj)
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

List<F> MyDeserialize(string strSer)
{
    List<F> MyDeserializedObjects = new List<F>(); ;
    var type = typeof(F);
    string[] strObjects = strSer.Split(';');
    foreach (var strObj in strObjects)
    {
        if (strObj != null && strObj.Length > 1)
        {
            string obj = strObj[1..^1];
            string[] strFields = obj.Split(',');
            F myDeserializedObj = null;
            foreach (var strField in strFields)
            {
                if (strField != null && strField.Length > 0)
                {
                    string fieldName = strField.Substring(0, strField.IndexOf(':'));
                    string fieldValue = strField.Substring(strField.IndexOf(':') + 1, strField.Length - fieldName.Length - 1);
                    if (fieldName.Length > 0 && fieldValue.Length > 0)
                    {
                        if (myDeserializedObj == null)
                            myDeserializedObj = new F();
                        FieldInfo fieldInfo = type.GetField(fieldName);
                        fieldInfo.SetValue(myDeserializedObj, Convert.ChangeType(fieldValue, fieldInfo.FieldType));
                    }
                }
            }
            if (myDeserializedObj != null)
                MyDeserializedObjects.Add(myDeserializedObj);
        }
    }
    return MyDeserializedObjects;
}

List<F> NewtonsoftDeserialize(string strSer)
{
    List<F> deserializedObjects = new List<F>(); ;
    var type = typeof(F);
    //strSer = strSer[1..^1];
    string[] strObjects = strSer.Split(';');
    foreach (var strObj in strObjects)
    {
        if (strObj != null && strObj.Length > 1)
        {
            //string obj = strObj[1..];
            F deserializedObj = JsonConvert.DeserializeObject<F>(strObj);
            if (deserializedObj != null)
                deserializedObjects.Add(deserializedObj);
        }
    }
    return deserializedObjects;
}

//public class F { int i1, i2, i3, i4, i5; Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
public class F {
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
