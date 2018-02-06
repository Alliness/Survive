using UnityEngine.Events;

namespace Game.Scripts.DTO
{
    public class PopupItemDTO
    {
        public string title { get; set; }

        public string imagePath { get; set; }

        public UnityAction onClickAction { get; set; }
    }
}