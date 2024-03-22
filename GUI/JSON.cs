using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Logic
{
    static public class JSON
    {
        public static void JSONPUSH(Document Document, Stack<string> JsonStack)
        {
            string json = JsonConvert.SerializeObject(Document.VisualGeometries,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            JsonStack.Push(json);
        }
        public static void JSONPOP(Document Document, Stack<string> JsonStack)
        {
            if (JsonStack.Count > 0)
            {
                string json = JsonStack.Pop();
                List<IVisualGeometry> restoredList = JsonConvert.DeserializeObject<List<IVisualGeometry>>(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                Document.VisualGeometries = restoredList;
            }
        }
    }
}
