using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Game.Scripts.DTO;
using Game.Scripts.Utils;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Hardware;
using UnityEngine;

namespace Game.Scripts.Building
{
    public class RoomsData
    {
        private List<RoomDTO> rooms;

        public RoomsData()
        {
            rooms = new List<RoomDTO>();

            JArray roomsArray = FReader.FileToArray(Path.Combine(Constants.Dir.STREAMING_ASSETS, "Rooms.json"));

            foreach (var jToken in roomsArray)
            {
                RoomDTO roomDto = Serializer.Deserialize<RoomDTO>((JObject) jToken);
                rooms.Add(roomDto);
            }
        }
        
        public RoomDTO GetBySize(Enums.RoomSize size)
        {
            foreach (var room in rooms)
            {
                if (room.GetRoomSize().Equals(size))
                {
                    return room;
                }
            }

            return null;
        }
    }
}