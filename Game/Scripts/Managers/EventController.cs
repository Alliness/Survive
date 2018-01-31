using System;
using Game.Scripts.DTO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Managers
{
    public class EventController : MonoBehaviour
    {
        public static EventController instance;

        #region LayerListener

        public event OnLayerChange LayerChangeSubscribles;

        public delegate void OnLayerChange(Enums.GameLayer layer);

        #endregion

        #region KeyboardKeyListener

        public event OnKeyDown OnkeyDownSubscribles;

        public delegate void OnKeyDown(KeyCode code);

        #endregion


        #region GameObjectRaycastListener

        public event OnGameObjectHover GameObjectHoverSubscribles;

        public delegate void OnGameObjectHover(GameObject gameObject);


        public event OnGameObjectChange GameObjectChangeSubscribles;

        public delegate void OnGameObjectChange(GameObject lastGameObject, GameObject newGameObject);

        #endregion

        #region RoomBuildListener

        public event OnBuildSuccess OnBuildSuccessSubscribles;

        public delegate void OnBuildSuccess(GameObject room);

        #endregion

        private EventSystem _eventSystem;

        private Enums.GameLayer currentLayer;

        private GameObject currentGameObject;
        private GameObject previousGameObject;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _eventSystem = EventSystem.current;
        }


        private void Update()
        {
            checkLayer();
//            KeysListener();
        }

        public Enums.GameLayer GetLayer()
        {
            return currentLayer;
        }

        void checkLayer()
        {
            Enums.GameLayer newLayer;
            if (_eventSystem.IsPointerOverGameObject())
            {
                newLayer = Enums.GameLayer.GUI;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    GameObject go = hit.transform.gameObject;

                    if (currentGameObject != go)
                    {
                        previousGameObject = currentGameObject;
                        currentGameObject = go;
                        if (GameObjectHoverSubscribles != null) GameObjectHoverSubscribles(go);
                        if (GameObjectChangeSubscribles != null)
                            GameObjectChangeSubscribles(previousGameObject, currentGameObject);
                    }

                    newLayer = (Enums.GameLayer) currentGameObject.layer;
                }
                else
                {
                    newLayer = Enums.GameLayer.GAME;
                }
            }

            if (currentLayer != newLayer)
            {
                currentLayer = newLayer;
                if (LayerChangeSubscribles != null) LayerChangeSubscribles(newLayer);
            }
        }

        void KeysListener()
        {
            //fixme require rework for key (wasd, esc, etc)
            if (!string.IsNullOrEmpty(Input.inputString))
            {
                KeyCode code = (KeyCode) Enum.Parse(typeof(KeyCode), Input.inputString);
                Debug.Log(code);
                if (OnkeyDownSubscribles != null) OnkeyDownSubscribles(code);
            }
        }

        public void buildNotify(GameObject room)
        {
            if (OnBuildSuccessSubscribles != null) OnBuildSuccessSubscribles(room);
        }
    }
}