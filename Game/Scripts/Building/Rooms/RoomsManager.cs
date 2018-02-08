using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Building.Rooms
{
    public class RoomsManager : MonoBehaviour
    {
        private List<GameObject> rooms; // list of builded rooms
        private RoomController activeRoom; // current selected room

        [HideInInspector] public static RoomsManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        void Start()
        {
            rooms = new List<GameObject>();
            EventController.instance.OnBuildSuccessSubscribles += addRoom;
        }

        /**
         * Add created Room GO  to Rooms List
         */
        private void addRoom(GameObject room)
        {
            rooms.Add(room);
            if (activeRoom != null)
            {
                activeRoom.Unselect();
                activeRoom = null;
            }
        }

        public List<GameObject> GetRooms()
        {
            return rooms;
        }
        /**
         * Return active (selected) Room
         */
        public RoomController GetActiveRoom()
        {
            return activeRoom;
        }

        /**
         * set Active room
         */
        public void SetActiveRoom(RoomController room)
        {
            UnsetActiveRoom();
            activeRoom = room;
            EventController.instance.activeRoomNotify(activeRoom);
        }

        /**
         * Unset active room
         */
        public void UnsetActiveRoom()
        {
            if (activeRoom != null)
            {
                activeRoom.Unselect();
                activeRoom = null;
            }
        }
    }
}