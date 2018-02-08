using UnityEngine;

namespace Game.Scripts.Building
{
    public class TileBlock : MonoBehaviour
    {

        public bool _Occupied;
        public Enums.RoomSize RoomSize;
        public bool isElevator;


        public bool Occupied
        {
            get { return _Occupied; }
            set
            {
                _Occupied = value;
            }
        }

        public void SetRoomSize(Enums.RoomSize size)
        {
            RoomSize = size;
            isElevator = RoomSize == Enums.RoomSize.Room1;
            gameObject.layer = (int) Enums.GameLayer.Room;
        }

    }
}