using Game.Scripts.Actor;
using Game.Scripts.Building;
using Game.Scripts.Building.Rooms;
using Game.Scripts.DTO.Actor;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class GUIManager : MonoBehaviour
    {
        public GameObject popup;

        public GameObject menu;

        [HideInInspector] public static GUIManager Instance;

        public Enums.GuiMode CurrentGuiMode;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetGuiMode(Enums.GuiMode.Base);
        }

        //todo create states from other events
        public void SetGuiMode(Enums.GuiMode mode)
        {
            if (CurrentGuiMode == mode)
            {
                return;
            }

            switch (mode)
            {
                case Enums.GuiMode.Base:
                    SetBaseMode();
                    break;
                case Enums.GuiMode.Room:
                    break;
                case Enums.GuiMode.Item:
                    //todo
                    break;
            }

            CurrentGuiMode = mode;
        }


        private void OnGUI()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RoomsManager.instance.UnsetActiveRoom();
                BuildManager.instance.destroyMarkers();
            }
        }

        private void SetBaseMode()
        {
            var controller = menu.GetComponent<MenuController>();
            controller.Clear();

            controller.AddButton("Build", () => popup.GetComponent<PopupController>().ShowRooms());
            controller.AddButton("Add Survivor", () =>
                                                 {
                                                     SurvivorDTO dto = new SurvivorsData().GetAll()[0];
                                                     SurvivorsManager.instance.SpawnSurvivor(dto);
                                                 });
            controller.AddButton("Storage", () => popup.GetComponent<PopupController>().ShowStorage());
            controller.AddButton("Expedition", () => Debug.Log("Expedition"));
        }
    }
}