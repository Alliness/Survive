using Game.Scripts.DTO;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class RoomController : MonoBehaviour
    {
        private Room _room;
        private GameObject _selectedFrame;
        private bool _isSeleted;

        private void Start()
        {
            _selectedFrame = transform.Find("selected").gameObject;
            Unselect();
        }

        public void SetRoom(Room room)
        {
            _room = room;
            name = room.name;
        }


        public Room GetRoom()
        {
            return _room;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unselect();
            }
        }

        public void Unselect()
        {
            _isSeleted = false;
            _selectedFrame.SetActive(false);
        }

        public void Select()
        {
            _isSeleted = true;
            _selectedFrame.SetActive(true);
            GetComponentInParent<RoomsManager>().SetActiveRoom(this);
        }

        private void OnMouseDown()
        {
           Select();
        }

        private void OnMouseOver()
        {
            _selectedFrame.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (!_isSeleted)
            {
                _selectedFrame.SetActive(false);
            }
        }
    }
}