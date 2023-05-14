using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{
    public class MyDeserializeFromFile
    {
        private StreamReader reader { get; set; }
        private Dictionary<int, string>  filedNamesInFile = new Dictionary<int, string>(); 

        public MyDeserializeFromFile(string fileName)
        {
            reader = new StreamReader(fileName);
            var strObj = reader.ReadLine();
            string[] strFields = strObj.Split(';');
            int i = 1;
            foreach (string strField in strFields) 
            {
                filedNamesInFile.Add(i++, strField);
            }
        }

        public List<F> DeserializeObject()
        {
            List<F> MyDeserializedObjects = new List<F>(); ;
            var type = typeof(F);
            while (!reader.EndOfStream)
            {
                var strObj = reader.ReadLine();
                if (strObj != null && strObj.Length > 1)
                {
                    string[] strFields = strObj.Split(';');
                    F myDeserializedObj = null;
                    int i = 1;
                    foreach (var strField in strFields)
                    {
                        if (strField != null && strField.Length > 0)
                        {
                            string fieldName = filedNamesInFile[i++];
                            string fieldValue = strField;
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

        public void CloseFile()
        {
            reader.Close();
        }
    }
}
