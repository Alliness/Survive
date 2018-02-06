using System;
using System.Collections.Generic;
using Game.Scripts.DTO;
using Game.Scripts.Managers;
using Game.Scripts.Utils;
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
        }


        public void DragStart()
        {
            offsetX = transform.position.x - Input.mousePosition.x;
            offsetY = transform.position.y - Input.mousePosition.y;
        }

        public void OnDrag()
        {
            transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
        }


        public void ShowRooms()
        {
            List<PopupItemDTO> list = new List<PopupItemDTO>();
            JArray array = FReader.FileToArray(Constants.Dir.STREAMING_ASSETS + "/Rooms.json");

            foreach (var jToken in array)
            {
                Room room = JsonSerializer.Deserialize<Room>((JObject) jToken);
                var dto = new PopupItemDTO();
                dto.title = room.name;
                dto.imagePath = room.view.icon;
                dto.onClickAction = () => BuildManager.instance.selectSize(room);
                list.Add(dto);
            }

            createPopup("Rooms", list);
        }

        public void createPopup(String titleText, List<PopupItemDTO> content)
        {
            if (popup.activeSelf)
            {
                hide();
            }

            show();
            Text popupText = popup.transform.Find("Title").GetComponent<Text>();
            Button closeButton = popup.transform.Find("Close").GetComponent<Button>();
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

        private void clearContent()
        {
            foreach (Transform child in contentBlock.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void hide()
        {
            
            clearContent();
            popup.SetActive(false);
        }

        private void show()
        {
            popup.SetActive(true);
        }

        public void ShowStorage()
        {
            throw new NotImplementedException();
        }
    }
}