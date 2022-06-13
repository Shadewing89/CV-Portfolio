using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentWhoSpawnsPrefabs : MonoBehaviour
{
    public List<GameObject> objectPrefabs = new List<GameObject>();
    public GameObject spawnLocation;
    public GameObject rotatingGlobePoint;
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
        //https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
        // Determine which direction to rotate towards
        Vector3 targetDirection = rotatingGlobePoint.transform.position;

        // The step size is equal to speed times frame time.
        //float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, -500f, 0.0f);

        // Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object



        randomPrefabNumber = Random.Range(0, objectPrefabs.Count);
        spawnedPrf = Instantiate(objectPrefabs[randomPrefabNumber], transform.position, Quaternion.Euler(-90, 0, 0));
        //spawnedPrf.transform.rotation = Quaternion.LookRotation(newDirection);
        spawnedPrf.transform.parent = transform;
        transform.LookAt(targetDirection);

        //Debug.Log("Something was spawned!");
    }
}
