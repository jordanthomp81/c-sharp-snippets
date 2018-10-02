using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public static bool buildingSelected = false;
	public static GameObject currSelectedBuilding;

	[SerializeField]
	private Canvas cityCenterUI;
	[SerializeField]
	private GameObject spawnPrefab;
	private GameObject currentPlaceableObject;
	private int spawnFlagCount = 0;
	
	// Update is called once per frame
	void Update () {
		if (buildingSelected == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					if(currSelectedBuilding.name != hitInfo.transform.name)
					{
						currSelectedBuilding.GetComponent<Outline>().enabled = !currSelectedBuilding.GetComponent<Outline>().enabled;
						if (currSelectedBuilding.GetComponent<Outline>().enabled == false)
						{
							// hide ui
							cityCenterUI.GetComponent<Canvas>().enabled = false;
							buildingSelected = false;
							currSelectedBuilding.GetComponent<buildingClick>().updateSpawnLocation();
							Destroy(GameObject.Find("shovel(Clone)"));
							Destroy(currentPlaceableObject);
						}
					}
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				Destroy(GameObject.Find("shovel(Clone)"));
				currentPlaceableObject = Instantiate(spawnPrefab);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					currentPlaceableObject.transform.position = hitInfo.point;
					currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
				}
			}
		}
	}
}
