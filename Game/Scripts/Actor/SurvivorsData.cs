using System;
using System.Collections.Generic;
using System.IO;
using Game.Scripts.DTO.Actor;
using Game.Scripts.Utils;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Scripts.Actor
{
    public class SurvivorsData // todo change it to actors data driven.
    {
        private List<SurvivorDTO> survivors;

        public SurvivorsData()
        {
            survivors = new List<SurvivorDTO>();
            JArray survivorsArray = FReader.FileToArray(Path.Combine(Constants.Dir.STREAMING_ASSETS, "survivors.json"));
            foreach (var jToken in survivorsArray)
            {
                SurvivorDTO dto = Serializer.Deserialize<SurvivorDTO>((JObject) jToken);
                survivors.Add(dto);
            }
        }

        public List<SurvivorDTO> GetAll()
        {
            return survivors;
        }

        public List<SurvivorDTO> GetSurvivors()
        {
            //todo 
            throw new NotImplementedException();
        }
    }
}