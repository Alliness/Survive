namespace Game.Scripts.DTO
{
    public class RoomDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string buildTime { get; set; }
        public string description { get; set; }
        public ViewDTO view { get; set; }

        public Enums.RoomSize GetRoomSize()
        {
            return (Enums.RoomSize) size;
        }
    }
}