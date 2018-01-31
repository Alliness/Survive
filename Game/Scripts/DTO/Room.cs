using System.Collections.Generic;

namespace Game.Scripts.DTO
{
    public class View
    {
        public string model { get; set; }
        public string icon { get; set; }
    }

    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int size { get; set; }
        public string buildTime { get; set; }
        public string level { get; set; }
        public string description { get; set; }
        public View view { get; set; }
        public List<object> upgrades { get; set; }
        public List<object> requiredResources { get; set; }
        public List<object> requiredTechnology { get; set; }


        public Enums.RoomSize GetRoomSize()
        {
            return (Enums.RoomSize) size;
        }

        public Enums.RoomType GetRoomType()
        {
            return (Enums.RoomType) type;
        }
    }
}