using UnityEngine;

namespace Game.Scripts.Building
{
    public class TileBlock : MonoBehaviour
    {
        private int x;
        private int y;

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
        

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        private void Start()
        {
            x = (int) transform.position.x;
            y = (int) transform.position.y;
        }

        public void SetRoomSize(Enums.RoomSize size)
        {
            RoomSize = size;
            isElevator = RoomSize == Enums.RoomSize.Room1;
            gameObject.layer = (int) Enums.GameLayer.Room;
        }

        public bool IsElevator()
        {
            return isElevator;
        }

        public Enums.RoomSize GetRoomType()
        {
            return RoomSize;
        }
    }
}