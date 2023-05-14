using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusEducationHomework5
{
    public class NewtonsoftDeserialize
    {
        public List<F> NewtonsoftDeserializeObject(string strSer)
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
    }
}
