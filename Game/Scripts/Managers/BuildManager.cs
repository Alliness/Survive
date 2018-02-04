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
            Build(buidler.GetRoom(Enums.RoomSize.Room4, Enums.RoomType.Hangar), 0, Constants.gridMaxY - 1);
            Build(buidler.GetRoom(Enums.RoomSize.Room1, Enums.RoomType.Elevator), 4, Constants.gridMaxY - 1);
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
            GameObject roomGO = Instantiate(new GameObject(room.name), firstTile.transform.position, Quaternion.identity);
            roomGO.transform.SetParent(transform);

            //attach model
            var pos = new Vector3(room.size / 2 - 1, 0, 0);
            Debug.Log(pos);
            Debug.Log(roomSize);
            var model = Instantiate(buildingsObjects[(int) room.GetRoomSize()],pos, Quaternion.identity);
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
            spawnerdMakets.ForEach(Destroy);
        }

        private void CreateMaket(int x, int y, Enums.RoomSize roomSize)
        {
            Instantiate(buildingMakets[(int) roomSize], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}