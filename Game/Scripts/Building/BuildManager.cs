﻿using System.Collections.Generic;
using Game.Scripts.Building.Rooms;
using Game.Scripts.DTO;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Building
{
    public class BuildManager : MonoBehaviour
    {
        [HideInInspector] public static BuildManager instance;

        public List<GameObject> spawnerdMakets; //list of active makets go
        public GameObject spawnMaketsGO;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        /**
         * create hangar(ROOM4) and elevator(ROOM1)
         */
        private void Start()
        {
            spawnerdMakets = new List<GameObject>();
            RoomsData buidler = new RoomsData();
            Build(buidler.GetBySize(Enums.RoomSize.Room4), 0, Constants.gridMaxY - 1);
            Build(buidler.GetBySize(Enums.RoomSize.Room1), 4, Constants.gridMaxY - 1);
        }

        /**
         * Destroy active build makets
         */
        public void destroyMarkers()
        {
            spawnerdMakets.ForEach(Destroy);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                destroyMarkers();
            }
        }

        /**
         * Show Available area for Room 
         */
        public void ShowAvailableAreaToBuild(RoomDTO roomDto)
        {
            destroyMarkers();

            var freeSectors = TileManager.instance.getAvailableToBuildArea(roomDto.GetRoomSize());
            freeSectors.ForEach(sector =>
                                {
                                    var spawnerdMaket = Instantiate(Resources.Load<GameObject>(roomDto.view.maket), sector.transform.position, Quaternion.identity);
                                    spawnerdMaket.transform.SetParent(spawnMaketsGO.transform, false);
                                    spawnerdMaket.GetComponent<MaketController>().SetRoom(roomDto);
                                    spawnerdMakets.Add(spawnerdMaket);
                                });
        }

        /**
         * Build room in start tile coord
         */
        public void Build(RoomDTO roomDto, int startX, int y)
        {
            //set room size (how many tiles will be occuppied);
            int roomSize = (int) roomDto.GetRoomSize() + startX;

            for (int offset = startX; offset < roomSize; offset++)
            {
                var c = TileManager.instance.blocks[offset, y].GetComponent<TileBlock>();
                c.Occupied = true;
                c.SetRoomSize(roomDto.GetRoomSize());
            }

            var firstTile = TileManager.instance.blocks[startX, y];

            //add empty room GO
            var pos = new Vector3(firstTile.transform.position.x + (roomDto.size - 1), firstTile.transform.position.y, firstTile.transform.position.z);
            GameObject roomGO = Instantiate(new GameObject(roomDto.name), pos, Quaternion.identity);
            roomGO.transform.SetParent(transform);

            //attach model
            var model = Instantiate(Resources.Load<GameObject>(roomDto.view.model), Vector3.zero, Quaternion.identity);
            model.transform.SetParent(roomGO.transform, false);

            //attach roomController
            Room controller = roomGO.AddComponent<Room>();
            controller.SetRoom(roomDto, model);

            //add collider to room GO
            var goCollider = roomGO.AddComponent<BoxCollider>();
            goCollider.size = model.GetComponentInChildren<Renderer>().bounds.size;
            goCollider.center = new Vector3(0, 1.5f, 0);

            //after

            roomGO.layer = (int) Enums.GameLayer.Room;

            RoomsManager.instance.AddRoom(roomGO);
            spawnerdMakets.ForEach(Destroy);
        }
    }
}