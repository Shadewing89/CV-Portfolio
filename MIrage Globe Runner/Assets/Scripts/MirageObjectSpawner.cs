using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs = new List<GameObject>();
    public GameObject spawnLocation;
    public GameObject rotatingGlobe;
    public GameObject spawnedPrf;
    private int randomPrefabNumber;

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
        //Debug.Log(other.transform.tag + " is sitting in the spawnzone.");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Something entered spawnzone.");
        if (other.tag != "SphereGlobe" && other.tag != "JustSpawned")
        {
            Debug.Log(other.gameObject.name + " is being destroyed.");
            Destroy(other.gameObject);
            SpawnGameObjectPrefab();
            //something is crashing and the spawn stays on in a loop
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.tag = "LeftSpawnArea";
        //Debug.Log("Something left spawnzone.");
    }
    public void SpawnGameObjectPrefab()
    {
        randomPrefabNumber = Random.Range(0, objectPrefabs.Count);
        spawnedPrf = Instantiate(objectPrefabs[randomPrefabNumber], spawnLocation.transform.position, Quaternion.Euler(180,0,0));
        spawnedPrf.transform.parent = rotatingGlobe.transform;

        //Debug.Log("Something was spawned!");
    }
}
