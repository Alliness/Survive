using Game.Scripts;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{

	public Text layerText;
	public Text raycastText;


	private void Start()
	{
		EventController.instance.GameObjectHoverSubscribles += raycastChange;
		EventController.instance.LayerChangeSubscribles += changeLayer;
	}

	void changeLayer(Enums.GameLayer layer)
	{
		layerText.text = "Current Layer: " + layer;
	}

	void raycastChange(GameObject go)
	{
		raycastText.text = "Current RayCast: " + go.name;
	}
}
