using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class Serializer
    {
        public static T Deserialize<T>(JObject json)
        {
            try
            {
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
                return JsonConvert.DeserializeObject<T>(json.ToString(), settings);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        public static JObject Serialize(object obj)
        {
            try
            {
                var settings = new JsonSerializerSettings
                               {
                                   ContractResolver = new CamelCasePropertyNamesContractResolver()
                               };
                return JObject.FromObject(obj, Newtonsoft.Json.JsonSerializer.Create(settings));
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
        
        
    }
}