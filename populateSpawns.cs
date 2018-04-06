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
    [SerializeField]
    private float spawnTerrainXMin;
    [SerializeField]
    private float spawnTerrainXMax;
    [SerializeField]
    private float spawnTerrainZMin;
    [SerializeField]
    private float spawnTerrainZMax;
    [SerializeField]
    private int totalSpawnMinimum = 12;

    private float spawnArea = 100f;
    public static int totalSpawns;

    // Update is called once per frame
    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
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
