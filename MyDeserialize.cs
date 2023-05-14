using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{
    public class MyDeserialize
    {
        public List<F> DeserializeObject(string strSer)
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

    }
}
