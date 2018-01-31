using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Game.Scripts.Utils
{
    public class FReader
    {
        public static JArray FileToArray(String path)
        {
            return JArray.Parse(File.ReadAllText(path));
        }

        public static JObject FileToObject(String path)
        {
            return JObject.Parse(File.ReadAllText(path));
        }
    }
}