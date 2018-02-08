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
        private List<Room> rooms;

        public RoomsData()
        {
            rooms = new List<Room>();

            JArray roomsArray = FReader.FileToArray(Path.Combine(Constants.Dir.STREAMING_ASSETS, "Rooms.json"));

            foreach (var jToken in roomsArray)
            {
                Room room = Serializer.Deserialize<Room>((JObject) jToken);
                rooms.Add(room);
            }
        }
        
        public Room GetBySize(Enums.RoomSize size)
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