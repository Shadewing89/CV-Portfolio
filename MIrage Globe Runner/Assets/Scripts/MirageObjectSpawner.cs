using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageObjectSpawner : MonoBehaviour
{
    //public List<GameObject> objectPrefabs = new List<GameObject>();
    public GameObject prefabParent;
    public List<GameObject> spawnLocations = new List<GameObject>();
    public GameObject rotatingGlobe;
    public GameObject spawnedPrf;
    public int allowedSpawnsLimit = 100;
    public List<GameObject> spawnedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnGameObjectPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (spawnedObjects.Count >= allowedSpawnsLimit)
        {
            Debug.Log("Limit reached, destroying.");
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                Destroy(spawnedObjects[i].gameObject);
                spawnedObjects.Remove(spawnedObjects[i]);
            }
            SpawnGameObjectPrefab();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftSpawnArea" || other.tag == "JustSpawned")
        {
            spawnedObjects.Add(other.gameObject);

        }
        //Debug.Log("Something entered spawnzone.");
        if (other.tag == "LeftSpawnArea")
        {
            Debug.Log(other.gameObject.name + " is being destroyed.");
            spawnedObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
            SpawnGameObjectPrefab();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JustSpawned")
        {
            spawnedObjects.Remove(other.gameObject);
            other.tag = "LeftSpawnArea";
            //Debug.Log("Something left spawnzone.");
        }
    }
    public void SpawnGameObjectPrefab()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            spawnedPrf = Instantiate(prefabParent, spawnLocations[i].transform.position, Quaternion.Euler(0,0,0));
            spawnedPrf.transform.parent = rotatingGlobe.transform;
        }
                   
        //Debug.Log("Something was spawned!");
    }

}
