using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
	public class MenuItem : MonoBehaviour
	{

		public void Set(string title, UnityAction clickAction)
		{
			GetComponentInChildren<Text>().text = title;
			GetComponentInChildren<Button>().onClick.AddListener(clickAction);
		}
		
		
	}
}
