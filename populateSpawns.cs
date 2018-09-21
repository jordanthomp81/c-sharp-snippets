using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateSpawns : MonoBehaviour
{

	[SerializeField]
	private GameObject spawnPrefab;
	private MeshCollider meshCollider;

	// Gather the bounding box vector positions needed to generate the random item locations
	// Serialize the x,y, and z axis values to use for random number generation
	private float spawnTerrainXMin;
	private float spawnTerrainXMax;
	private float spawnTerrainZMin;
	private float spawnTerrainZMax;
	[SerializeField]
	private int totalSpawnMinimum = 12;

	public static int totalSpawns;

	// Update is called once per frame
	void Awake()
	{
		meshCollider = GetComponent<MeshCollider>();
		spawnTerrainXMin = meshCollider.transform.position.x;
		spawnTerrainXMax = spawnTerrainXMin + 25;
		spawnTerrainZMin = meshCollider.transform.position.z;
		spawnTerrainZMax = spawnTerrainZMin + 25;
		totalSpawns = 0;

		while (totalSpawns < totalSpawnMinimum)
		{
			float maxHeight = 20f;
			float positionX = Random.Range(spawnTerrainXMin, spawnTerrainXMax);
			float positionZ = Random.Range(spawnTerrainZMin, spawnTerrainZMax);

			Ray ray = new Ray(new Vector3(positionX, maxHeight, positionZ), Vector3.down);
			RaycastHit hit;
			if (meshCollider.Raycast(ray, out hit, maxHeight))
			{
				Vector3 spawnPosition = new Vector3(positionX, hit.point.y, positionZ);
				Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
			}
			totalSpawns++;
		}
	}
}
