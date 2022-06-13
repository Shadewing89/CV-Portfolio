using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnlyMirageObjectSpawner : MonoBehaviour
{
    //public List<GameObject> objectPrefabs = new List<GameObject>();
    //public GameObject prefabParent;
    //public List<GameObject> spawnLocations = new List<GameObject>();
    //public GameObject rotatingGlobe;
    //public GameObject spawnedPrf;
    //public float waitSecondsSO = 1f;
    //public int canSpawnQuestion;
    //public int canSpawnLimit;
    public GameObject bigSpawnZone;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log(other.gameObject.name + " entered visual distance. Spawning another.");
            //Destroy(other.gameObject);

            bigSpawnZone.GetComponent<MirageObjectSpawner>().SpawnGameObjectPrefab();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JustSpawned")
        {
            other.tag = "LeftSpawnArea";
            //Debug.Log("Something left spawnzone.");
        }
    }



}
