using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.GUI
{
    public class MenuController : MonoBehaviour
    {
        public GameObject buttonPrefab;

        public List<GameObject> buttons;

        void Start()
        {
            buttons = new List<GameObject>();
        }

        public void AddButton(string title, UnityAction action)
        {
            var newButton = Instantiate(buttonPrefab);
            newButton.GetComponent<MenuItem>().Set(title, action);
            newButton.transform.SetParent(transform, false);
            buttons.Add(newButton);
        }

        public void Clear()
        {
            buttons.ForEach(Destroy);
        }
    }
}