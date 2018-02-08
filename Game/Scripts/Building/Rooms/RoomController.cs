using Game.Scripts.DTO;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Building.Rooms
{
    public class RoomController : MonoBehaviour
    {
        private Room _room;
        private bool _isSeleted;

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

        public void SetRoom(Room room, GameObject model)
        {
            _room = room;
            _model = model;
            name = room.name;

            var prefab = Resources.Load("Prefabs/Building/Rooms/Materials/Selected");

            _matSelected = Instantiate(prefab) as Material;
            _meshRenderer = _model.GetComponentInChildren<MeshRenderer>();
            _matDefault = _meshRenderer.material;
            Unselect();
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
            _model.GetComponentInChildren<MeshRenderer>().material = _matDefault;
            _isSeleted = false;
        }

        public void Select()
        {
            _model.GetComponentInChildren<MeshRenderer>().material = _matSelected;
            _isSeleted = true;
            RoomsManager.instance.SetActiveRoom(this);
        }

        private void OnMouseDown()
        {
            Select();
        }

        private void OnMouseOver()
        {
            _model.GetComponentInChildren<MeshRenderer>().material = _matSelected;
        }

        private void OnMouseExit()
        {
            if (!_isSeleted)
                _model.GetComponentInChildren<MeshRenderer>().material = _matDefault;
        }
    }
}