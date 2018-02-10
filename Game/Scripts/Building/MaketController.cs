using Game.Scripts.DTO;
using UnityEngine;

namespace Game.Scripts.Building
{
    public class MaketController : MonoBehaviour
    {

        private RoomDTO _roomDto;
    
        private void OnMouseOver()
        {
            if (Input.GetMouseButton(0))
            {
                BuildManager.instance.Build(_roomDto, (int) transform.position.x/Constants.tileSizeX, (int) transform.position.y/Constants.tileSizeY);
            }
        }

        public void SetRoom(RoomDTO size)
        {
            _roomDto = size;
        }
    }
}