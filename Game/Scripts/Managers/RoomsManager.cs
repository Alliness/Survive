using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class RoomsManager : MonoBehaviour
    {
        // Use this for initialization
        private List<GameObject> rooms;
        private RoomController activeRoom;


        void Start()
        {
            rooms = new List<GameObject>();
            EventController.instance.OnBuildSuccessSubscribles += addRoom;
        }

        private void addRoom(GameObject room)
        {
            rooms.Add(room);
        }

        public List<GameObject> GetRooms()
        {
            return rooms;
        }

        public void SetActiveRoom(RoomController room)
        {
            if (activeRoom != room)
            {
                if (activeRoom != null)
                    activeRoom.Unselect();
                activeRoom = room;
            }
        }

        public void UnsetRoom()
        {
            activeRoom = null;
        }
    }
}