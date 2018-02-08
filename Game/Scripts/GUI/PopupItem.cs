using Game.Scripts.DTO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class PopupItem : MonoBehaviour
    {
        public Text text;
        public Image image;
        private PopupItemDTO _dto;

        public void Set(PopupItemDTO dto)
        {
            _dto = dto;
            text.text = dto.title;
            image.material = Resources.Load<Material>(dto.imagePath);
            
            GetComponentInChildren<Button>().onClick.AddListener(dto.onClickAction);
        }
    }
}