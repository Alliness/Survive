using System;
using Game.Scripts.Building;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Managers
{
    public class GUIManager : MonoBehaviour
    {
        [HideInInspector] public static GUIManager instance;

        public Text peoples;
        
        public GameObject contentForm;
        public GameObject contentItemPrefab;
        public GameObject contentTooltip;

        private BuilderData builderData;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            builderData = new BuilderData();
        }

        private void Start()
        {
            contentTooltip.SetActive(false);
        }


        private void DestroyContent()
        {
            foreach (Transform child in contentForm.transform)
            {
                Destroy(child.gameObject);
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DestroyContent();
            }
        }

        [UsedImplicitly]
        public void SetBuildingSize(int roomSize)
        {

            DestroyContent();
            BuildManager.instance.destroyMarkers();
            var contentRect = contentForm.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(0, contentRect.sizeDelta.y);

            Enums.RoomSize size = (Enums.RoomSize) Enum.Parse(typeof(Enums.RoomSize), roomSize.ToString());
            var result = builderData.GetBySize(size);

            int offset = 64;
            int width = 128;

            result.ForEach(row =>
                           {
                               contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x + width, contentRect.sizeDelta.y);
                               var showItem = Instantiate(contentItemPrefab);
                               var itemRect = showItem.GetComponent<RectTransform>();
                               var itemInfo = showItem.GetComponent<ContentItem>();

                               itemInfo.Set(row);
                               showItem.transform.SetParent(contentForm.transform);
                               //                               itemRect.localPosition.Set(itemRect.position.x+offset, itemRect.position.y, itemRect.position.z);
                               var pos = itemRect.anchoredPosition;
                               pos.x = offset;
                               pos.y = 0;
                               itemRect.anchoredPosition = pos;
                               offset += width;
                           });
        }
    }
}