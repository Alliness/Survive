using System;
using System.Collections.Generic;
using Game.Scripts.Building;
using Game.Scripts.DTO;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class BuildManager : MonoBehaviour
    {
        [HideInInspector] public static BuildManager instance;

        public GameObject[] buildingsObjects;
        public GameObject[] buildingMakets;

        public List<GameObject> spawnerdMakets;
        public GameObject spawnMaketsGO;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            spawnerdMakets = new List<GameObject>();
            BuilderData buidler = new BuilderData();
            build(buidler.GetRoom(Enums.RoomSize.ROOM_4, Enums.RoomType.HANGAR), 0, Constants.gridMaxY - 1);
            build(buidler.GetRoom(Enums.RoomSize.ROOM_1, Enums.RoomType.ELEVATOR), 4, Constants.gridMaxY - 1);
        }

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

        public void selectSize(ContentItem item)
        {
            destroyMarkers();

            GameObject selectedObjectMaket = buildingMakets[item.GetMaketId()];

            var freeSectors = TileManager.instance.getAvailableToBuildArea(item.getRoom().GetRoomSize());
            freeSectors.ForEach(sector =>
                                {
                                    var spawnerdMaket = Instantiate(selectedObjectMaket, sector.transform.position, Quaternion.identity);
                                    spawnerdMaket.GetComponent<MaketController>().SetRoom(item.getRoom());
                                    spawnerdMakets.Add(spawnerdMaket);
                                    spawnerdMaket.transform.SetParent(spawnMaketsGO.transform);
                                });
        }

        public void build(Room room, int startX, int y)
        {
            //set room size (how many tiles will be occuppied);
            int roomSize = (int) room.GetRoomSize() + startX;

            for (int offset = startX; offset < roomSize; offset++)
            {
                var c = TileManager.instance.blocks[offset, y].GetComponent<TileBlock>();
                c.Occupied = true;
                c.SetRoomSize(room.GetRoomSize());
            }

            var firstTile = TileManager.instance.blocks[startX, y];
            var building = Instantiate(buildingsObjects[(int) room.GetRoomSize()], firstTile.transform.position, Quaternion.identity);
            building.GetComponentInChildren<RoomController>().SetRoom(room);
            building.transform.SetParent(transform);
            EventController.instance.buildNotify(building);
            spawnerdMakets.ForEach(Destroy);
        }

        private void createMaket(int x, int y, Enums.RoomSize roomSize)
        {
            Instantiate(buildingMakets[(int) roomSize], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}