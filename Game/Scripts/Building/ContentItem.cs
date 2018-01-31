using Game.Scripts.DTO;
using Game.Scripts.Managers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Building
{
    public class ContentItem : MonoBehaviour
    {
        public Text title;

        public int maket;
        private bool isSelected;
        private Room _room;

        private void Start()
        {
            HideTooltip();
        }


        public void Set(Room buildInfo)
        {
            _room = buildInfo;
            maket = buildInfo.size;
            title.text = _room.GetRoomType() + "_" + _room.GetRoomSize();
        }


        [UsedImplicitly]
        public void Build()
        {
            isSelected = true;
            BuildManager.instance.selectSize(this);
        }

        [UsedImplicitly]
        public void ShowTooltip()
        {
            GUIManager.instance.contentTooltip.SetActive(true);
            GUIManager.instance.contentTooltip.GetComponent<BuildToolTipController>().Display(title.text, _room.description);
        }

        public void HideTooltip()
        {
            if (!isSelected)
                GUIManager.instance.contentTooltip.SetActive(false);
        }


        public Room getRoom()
        {
            return _room;
        }

        public int GetMaketId()
        {
            return maket;
        }
    }
}