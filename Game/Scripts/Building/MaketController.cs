﻿using Game.Scripts.DTO;
using UnityEngine;

namespace Game.Scripts.Building
{
    public class MaketController : MonoBehaviour
    {

        private Room _room;
    
        private void OnMouseOver()
        {
            if (Input.GetMouseButton(0))
            {
                BuildManager.instance.Build(_room, (int) transform.position.x/Constants.tileSizeX, (int) transform.position.y/Constants.tileSizeY);
            }
        }

        public void SetRoom(Room size)
        {
            _room = size;
        }
    }
}