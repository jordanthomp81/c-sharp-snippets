using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingClick : MonoBehaviour {

	[SerializeField]
	private GameObject citizenPrefab;
	private GameObject building;

	// Use this for initialization
	void OnMouseDown()
	{
		gameManager.buildingSelected = true;

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				building = GameObject.Find(hit.transform.gameObject.name);
				gameManager.currSelectedBuilding = building;
				GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
				if(GetComponent<Outline>().enabled == false)
				{
					gameManager.buildingSelected = false;
				}
			}
		}
	}
}
