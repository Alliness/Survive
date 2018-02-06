using System.Collections.Generic;
using Game.Scripts.Building;
using Game.Scripts.DTO;
using UnityEngine;
using UnityEngine.VR.WSA;

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
            Build(buidler.GetBySize(Enums.RoomSize.Room4), 0, Constants.gridMaxY - 1);
            Build(buidler.GetBySize(Enums.RoomSize.Room1), 4, Constants.gridMaxY - 1);
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

        public void selectSize(Room room)
        {
            destroyMarkers();

            GameObject selectedObjectMaket = buildingMakets[room.id];
            var freeSectors = TileManager.instance.getAvailableToBuildArea(room.GetRoomSize());
            freeSectors.ForEach(sector =>
                                {
                                    var spawnerdMaket = Instantiate(selectedObjectMaket, sector.transform.position, Quaternion.identity);
                                    spawnerdMaket.GetComponent<MaketController>().SetRoom(room);
                                    spawnerdMakets.Add(spawnerdMaket);
                                    spawnerdMaket.transform.SetParent(spawnMaketsGO.transform);
                                });
        }

        public void Build(Room room, int startX, int y)
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

            //add empty room GO
            var pos = new Vector3(firstTile.transform.position.x + (room.size -1), firstTile.transform.position.y, firstTile.transform.position.z);
            GameObject roomGO = Instantiate(new GameObject(room.name), pos, Quaternion.identity);
            roomGO.transform.SetParent(transform);

            //attach model
            var model = Instantiate(buildingsObjects[(int) room.GetRoomSize()],Vector3.zero, Quaternion.identity);
            model.transform.SetParent(roomGO.transform, false);

            //attach roomController
            RoomController controller = roomGO.AddComponent<RoomController>();
            controller.SetRoom(room, model);

            //add collider to room GO
            var goCollider = roomGO.AddComponent<BoxCollider>();
            goCollider.size = model.GetComponentInChildren<Renderer>().bounds.size;
            goCollider.center = new Vector3(0, 1.5f, 0);

            //after
            EventController.instance.buildNotify(model);

            roomGO.layer = (int) Enums.GameLayer.Room;
            
            spawnerdMakets.ForEach(Destroy);
        }

        private void CreateMaket(int x, int y, Enums.RoomSize roomSize)
        {
            Instantiate(buildingMakets[(int) roomSize], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}