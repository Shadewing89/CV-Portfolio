using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentWhoSpawnsPrefabs : MonoBehaviour
{
    public List<GameObject> objectPrefabs = new List<GameObject>();
    public GameObject spawnLocation;
    public GameObject rotatingGlobe;
    public GameObject spawnedPrf;
    private int randomPrefabNumber;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnGameObjectPrefab();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnGameObjectPrefab()
    {
        randomPrefabNumber = Random.Range(0, objectPrefabs.Count);
        spawnedPrf = Instantiate(objectPrefabs[randomPrefabNumber], transform.position, Quaternion.Euler(180, 0, 0));
        spawnedPrf.transform.parent = transform;

        //Debug.Log("Something was spawned!");
    }
}
