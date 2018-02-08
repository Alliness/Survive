using System;
using System.Collections.Generic;
using Game.Scripts.Building;
using Game.Scripts.Building.Rooms;
using Game.Scripts.DTO;
using Game.Scripts.Managers;
using Game.Scripts.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class PopupController : MonoBehaviour
    {
        public GameObject popup;

        public GameObject contentBlock;

        public GameObject popupItemPrefab;


        private float offsetX;
        private float offsetY;

        private void Start()
        {
            hide();
            EventController.instance.OnActiveRoomChangeSubscribles += hidePopupOnRoomSelect;
        }

        /**
         * Close popup if some room is selected
         */
        private void hidePopupOnRoomSelect(RoomController room)
        {
            hide();
        }


        private void Update()
        {
            if (popup.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                hide();
            }
        }

        #region DragPopupMethods

        [UsedImplicitly]
        public void DragStart()
        {
            offsetX = transform.position.x - Input.mousePosition.x;
            offsetY = transform.position.y - Input.mousePosition.y;
        }

        [UsedImplicitly]
        public void OnDrag()
        {
            transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
        }

        #endregion

        
        /**
         * Show list of available to build rooms from @Resousrce : Rooms.json
         */
        public void ShowRooms()
        {
            List<PopupItemDTO> list = new List<PopupItemDTO>();
            JArray array = FReader.FileToArray(Constants.Dir.STREAMING_ASSETS + "/Rooms.json");

            foreach (var jToken in array)
            {
                Room room = Serializer.Deserialize<Room>((JObject) jToken);
                var dto = new PopupItemDTO();
                dto.title = room.name;
                dto.imagePath = room.view.icon;
                dto.onClickAction = () =>
                                    {
                                        hide();
                                        BuildManager.instance.ShowAvailableAreaToBuild(room);
                                    };
                list.Add(dto);
            }

            createPopup("Rooms", list);
        }

        /**
         * show popup with List of content PopupItemDTO
         */
        public void createPopup(String titleText, List<PopupItemDTO> content)
        {
            if (popup.activeSelf)
            {
                hide();
            }

            show();
            Text popupText = transform.FindDeepChild("Title").GetComponent<Text>();
            Button closeButton = transform.FindDeepChild("Close").GetComponent<Button>();
            popupText.text = titleText;

            closeButton.onClick.AddListener(hide);
            for (var i = 0; i < content.Count; i++)
            {
                GameObject prefab = Instantiate(popupItemPrefab);
                prefab.GetComponent<PopupItem>().Set(content[i]);
                prefab.transform.SetParent(contentBlock.transform, false);
            }

            RectTransform rect = contentBlock.GetComponent<RectTransform>();

            //y = item size + border * rows count + first row;
            var y = (popupItemPrefab.GetComponent<RectTransform>().rect.height + 5) * (content.Count / 3 + 1);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, y);
        }

        /**
         * Clear popup content(Not hide popup instance)
         */
        private void clearContent()
        {
            foreach (Transform child in contentBlock.transform)
            {
                Destroy(child.gameObject);
            }
        }

        
        /**
         * hide popup and clear content
         */
        private void hide()
        {
            clearContent();
            popup.SetActive(false);
        }

        /**
         * show popup
         */
        private void show()
        {
            popup.SetActive(true);
        }

        //todo list of storage items
        public void ShowStorage()
        {
            throw new NotImplementedException();
        }
    }
}