using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Touch input MonoBehaviour for handling multiple touch input events.
public class MultiTouchInput : MonoBehaviour
{
    // Reference to touch count Text component in the scene.
    [SerializeField] private TextMeshProUGUI touchCountText;
    public GameObject[] prefabsArray = new GameObject[4];
    private int whatHasSpawned = 0;

    // Update method is called every frame, if the MonoBehaviour is enabled.
    private void Update()
    {
        // Display the current touch count in the Text component.
        touchCountText.text = "Spawned count: " + whatHasSpawned;

        // Make sure there are currently touches on the screen (at least one).
        if (Input.touchCount > 0)
        {
            // Obtain the Touch in the zero index.
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                // Log the touch phase events.
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("TouchPhase.Began: " + i);
                    SpawnStuff(i, worldPosition);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("TouchPhase.Moved: " + i);
                    SpawnStuff(i, worldPosition);
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    Debug.Log("TouchPhase.Stationary: " + i);
                    SpawnStuff(i, worldPosition);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("TouchPhase.Ended: " + i);
                }
                else if (touch.phase == TouchPhase.Canceled)
                {
                    Debug.Log("TouchPhase.Canceled: " + i);
                }
            }
        }
    }
    public void SpawnStuff(int howManyTouches, Vector3 spawnLocation)
    {
        while (howManyTouches >= prefabsArray.Length) //we check whether or not the current touches on screen are more than the amount
                                                      //of prefabs prepared in advance and decrease the index to fit within the limit
                                                      //thus even with 9 inputs at once all touches will spawn a prefab.
        {
            howManyTouches -= prefabsArray.Length;
            Debug.Log("howManyTouches is at: " + howManyTouches);
        }
        Instantiate(prefabsArray[howManyTouches], spawnLocation, Quaternion.identity);
        whatHasSpawned += 1;
    }
}