using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OtusEducationHomework5;
// See https://aka.ms/new-console-template for more information
int countIteration = 10000;
var sw = new Stopwatch();
var mc = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };// new MyClass();
string str = string.Empty;
var mySerialize = new MySerialize();
sw.Start();
for (var i = 1; i <= countIteration; i++)
{
    str += $"{mySerialize.SerializeObject(mc)};";
}
str = str[0..^1];
sw.Stop();
Console.WriteLine(" My serialized time");
Console.WriteLine($"{sw.ElapsedMilliseconds / 1000.0}");
//Console.WriteLine(str);
var myDeserialize = new MyDeserialize();
sw.Restart();
var s = myDeserialize.DeserializeObject(str); 
sw.Stop();
Console.WriteLine(" My deserialized time");
Console.WriteLine($"{sw.ElapsedMilliseconds / 1000.0}");
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
Console.WriteLine($"{sw.ElapsedMilliseconds / 1000.0}");
//Console.WriteLine(str);
var newtonsoftDeserialize = new NewtonsoftDeserialize();
sw.Restart();
s = newtonsoftDeserialize.NewtonsoftDeserializeObject(str);
sw.Stop();
Console.WriteLine(" Newtonsoft deserialized time");
Console.WriteLine($"{sw.ElapsedMilliseconds / 1000.0}");
//s.ForEach(Console.WriteLine);




