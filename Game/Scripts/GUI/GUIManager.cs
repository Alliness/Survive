using UnityEngine;

namespace Game.Scripts.GUI
{
    public class GUIManager : MonoBehaviour
    {
        public GameObject popup;

        public GameObject menu;

        public static GUIManager Instance;

        public Enums.GuiMode CurrentGuiMode;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetGuiMode(Enums.GuiMode.Base);
        }

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

        #region BaseGuiMode

        private void SetBaseMode()
        {
            var controller = menu.GetComponent<MenuController>();
            controller.Clear();

            controller.AddButton("Build", () => popup.GetComponent<PopupController>().ShowRooms());
            controller.AddButton("Map", () => Debug.Log("map"));
            controller.AddButton("Storage", ()=> popup.GetComponent<PopupController>().ShowStorage());
            controller.AddButton("Expedition", ()=>Debug.Log("Expedition"));
        }

        #endregion
    }
}