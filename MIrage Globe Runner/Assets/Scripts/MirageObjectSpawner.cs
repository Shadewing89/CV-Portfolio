using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageObjectSpawner : MonoBehaviour
{
    //public List<GameObject> objectPrefabs = new List<GameObject>();
    public GameObject prefabParent;
    public GameObject spawnLocation;
    public GameObject rotatingGlobe;
    public GameObject spawnedPrf;
    public float waitSeconds = 0.5f;
    //private int randomPrefabNumber;

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
        if (other.tag == "LeftSpawnArea")
        {
            Debug.Log(other.gameObject.name + " is being destroyed.");
            Destroy(other.gameObject);
            StartCoroutine("ZoneEnter");
            //something is crashing and the spawn stays on in a loop
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JustSpawned")
        {
            other.tag = "LeftSpawnArea";
            //Debug.Log("Something left spawnzone.");
            StartCoroutine("ZoneExit");
        }
    }
    public void SpawnGameObjectPrefab()
    {
        //randomPrefabNumber = Random.Range(0, objectPrefabs.Count);
        spawnedPrf = Instantiate(prefabParent, spawnLocation.transform.position, Quaternion.Euler(180,0,0));
        spawnedPrf.transform.parent = rotatingGlobe.transform;

        //Debug.Log("Something was spawned!");
    }
    public IEnumerator ZoneEnter()
    {
        yield return new WaitForSeconds(waitSeconds);
        SpawnGameObjectPrefab();
    }
    public IEnumerator ZoneExit()
    {
        yield return new WaitForSeconds(waitSeconds);
    }
}
