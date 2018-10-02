using UnityEngine;

public class groundPlacement : MonoBehaviour
{

	[SerializeField]
	private GameObject placeableObjectPrefab;
	private GameObject currentPlaceableObject;
	private float mouseWheelRotation;

	private void Update()
	{
		if (currentPlaceableObject != null)
		{
			MoveCurrentObjectToMouse();
			RotateFromMouseWheel();
			ReleaseIfClicked();
		}
	}

	private void RotateFromMouseWheel()
	{
		mouseWheelRotation += Input.mouseScrollDelta.y;
		currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
	}

	private void MoveCurrentObjectToMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			currentPlaceableObject.transform.position = hitInfo.point;
			currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
	}

	private void ReleaseIfClicked()
	{
		if (Input.GetMouseButtonDown(0))
		{
			currentPlaceableObject = null;
		}
	}

	public void HandleNewObjectHotkey()
	{
		if (currentPlaceableObject != null)
		{
			Destroy(currentPlaceableObject);
		}
		else
		{
			currentPlaceableObject = Instantiate(placeableObjectPrefab);
		}
	}
}
