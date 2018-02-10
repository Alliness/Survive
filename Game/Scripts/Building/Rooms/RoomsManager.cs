using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Building.Rooms
{
    public class RoomsManager : MonoBehaviour
    {
        private List<GameObject> rooms; // list of builded rooms
        private Room activeRoom; // current selected room

        [HideInInspector] public static RoomsManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            rooms = new List<GameObject>();

        }

        /**
         * Add created Room GO  to Rooms List
         */
        public void AddRoom(GameObject room)
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

        public Room GetActiveRoom()
        {
            return activeRoom;
        }

        /**
         * set Active room
         */

        public void SetActiveRoom(Room room)
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