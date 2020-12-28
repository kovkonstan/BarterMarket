using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace BarterMarket.HtmlHelpers
{
    public class JsonHelper
    {
        public static T Deserialise<T>(String json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serialiser = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serialiser.ReadObject(ms);
            }
            return obj;
        }
    }
}