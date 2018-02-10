using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Building.Rooms
{
    public class Room : MonoBehaviour
    {
        private DTO.RoomDTO _roomDtoData;
        private bool _isSelected;
        public bool isElevator;

        private GameObject _model;

        private Material _matDefault;
        private Material _matSelected;

        private MeshRenderer _meshRenderer;
        private bool coolliderEnabled;

        private void Start()
        {
            EventController.instance.LayerChangeSubscribles += ToggleCollider;
        }

        private void ToggleCollider(Enums.GameLayer layer)
        {
            GetComponent<Collider>().enabled = layer == Enums.GameLayer.Game || layer == Enums.GameLayer.Room;
        }

        public void SetRoom(DTO.RoomDTO roomDto, GameObject model)
        {
            isElevator = roomDto.size == 1;
            _roomDtoData = roomDto;
            _model = model;
            name = roomDto.name;

            var prefab = Resources.Load("Prefabs/Building/Rooms/Materials/Selected");

            _matSelected = Instantiate(prefab) as Material;
            _meshRenderer = _model.GetComponentInChildren<MeshRenderer>();
            _matDefault = _meshRenderer.material;
            Unselect();
        }

        public DTO.RoomDTO GetRoom()
        {
            return _roomDtoData;
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
            _model.GetComponentInChildren<MeshRenderer>().material = _matDefault;
            _isSelected = false;
        }

        public void Select()
        {
            _model.GetComponentInChildren<MeshRenderer>().material = _matSelected;
            _isSelected = true;
            RoomsManager.instance.SetActiveRoom(this);
        }

        private void OnMouseDown()
        {
            Select();
        }

//        private void OnMouseOver()
//        {
//            _model.GetComponentInChildren<MeshRenderer>().material = _matSelected;
//        }
//
//        private void OnMouseExit()
//        {
//            if (!_isSelected)
//                _model.GetComponentInChildren<MeshRenderer>().material = _matDefault;
//        }
    }
}