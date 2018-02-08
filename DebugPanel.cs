using Game.Scripts;
using Game.Scripts.Building.Rooms;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{

	public Text layerText;
	public Text raycastText;
	public Text activeRoom;

	private void Start()
	{
		EventController.instance.GameObjectHoverSubscribles += raycastChange;
		EventController.instance.LayerChangeSubscribles += changeLayer;
		EventController.instance.OnActiveRoomChangeSubscribles += changeActiveRoom;
	}

	private void changeActiveRoom(RoomController room)
	{
		activeRoom.text = "Active Room: " + room.name;
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
