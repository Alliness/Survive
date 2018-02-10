using System.Collections;
using Game.Scripts.Building.Rooms;
using Game.Scripts.DTO.Actor;
using UnityEngine;


//todo fix bug with collider survivor -> room;
namespace Game.Scripts.Actor
{
    public class Survivor : MonoBehaviour
    {
        public SurvivorDTO data { get; set; }

        public bool _isSelected;
        private Material _matSelected;
        private Material _matDefault;

        private MeshRenderer _meshRenderer;
        private GameObject _model;
        public float movementSpeed = 0.05f;

        public void Set(SurvivorDTO dto, GameObject model)
        {
            data = dto;
            _model = model;
            var prefab = Resources.Load("Prefabs/Actor/Active");
            _matSelected = Instantiate(prefab) as Material;
            _meshRenderer = _model.GetComponentInChildren<MeshRenderer>();
            _matDefault = _meshRenderer.material;
        }


        private void OnMouseDown()
        {
            Select();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnSelect();
            }

            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    GameObject go = hit.transform.gameObject;
                    if (go.layer.Equals((int) Enums.GameLayer.Room))
                    {
                        StartCoroutine(MoveTo(go.GetComponent<Room>()));
                    }
                }
            }
        }

        IEnumerator MoveTo(Room room)
        {
            var targetPos = room.gameObject.transform.position;
            while (Vector3.Distance(targetPos, transform.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);
                yield return null;
            }
        }

        public void Select()
        {
            _isSelected = true;
            SurvivorsManager.instance.SetActiveSurvivor(this);
            _meshRenderer.material = _matSelected;
        }

        public void UnSelect()
        {
            _meshRenderer.material = _matDefault;
            _isSelected = false;
        }
    }
}