using System.Collections;
using TMPro;
using UnityEngine;

public class LocationHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI latitudeText;
    [SerializeField]
    private TextMeshProUGUI longitudeText;

    private readonly float initialDelay = 5f;
    private bool isLocationAvailable;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        yield return new WaitForSeconds(initialDelay);
        
        Input.location.Start();

        while(Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1f);
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Failed to determine device location!");
            yield break;
        }

        isLocationAvailable = true;
    }

    void Update()
    {
        if(isLocationAvailable)
        {
            latitudeText.text = "LAT: " + Input.location.lastData.latitude;
            longitudeText.text = "LON: " + Input.location.lastData.longitude;
        }
    }
}
