using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideUI : MonoBehaviour {

	[SerializeField]
	private Canvas cityCenterUI;

	void OnMouseDown()
	{
		cityCenterUI.GetComponent<Canvas>().enabled = false;
	}
}
