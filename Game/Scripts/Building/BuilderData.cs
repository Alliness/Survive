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
    public class BuilderData
    {
        private List<Room> rooms;

        public BuilderData()
        {
            rooms = new List<Room>();

            JArray roomsArray = FReader.FileToArray(Path.Combine(Constants.Dir.STREAMING_ASSETS, "Rooms.json"));

            foreach (var jToken in roomsArray)
            {
                Room room = JsonSerializer.Deserialize<Room>((JObject) jToken);
                rooms.Add(room);
            }
        }

        public Room GetRoom(Enums.RoomSize size, Enums.RoomType type)
        {
            foreach (var room in GetByType(type))
            {
                
                if (room.GetRoomSize().Equals(size))
                {
                    return room;
                }
            }

            return null;
        }

        public List<Room> GetByType(Enums.RoomType type)
        {
            List<Room> result = new List<Room>();
            foreach (var room in rooms)
            {
                if (room.GetRoomType().Equals(type))
                {
                    result.Add(room);
                }
            }
            return result;
        }

        public List<Room> GetBySize(Enums.RoomSize size)
        {
            List<Room> result = new List<Room>();
            foreach (var room in rooms)
            {
                if (room.GetRoomSize().Equals(size))
                {
                    result.Add(room);
                }
            }

            return result;
        }
    }
}