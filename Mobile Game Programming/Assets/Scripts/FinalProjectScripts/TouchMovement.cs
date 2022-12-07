using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public GameObject carrotToFollow;
    public float speed = 20.0f;
    public Rigidbody playerRb;
    private void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update method is called every frame, if the MonoBehaviour is enabled.
    private void Update()
    {
        
        // Make sure there are currently touches on the screen (at least one).
        if (Input.touchCount > 0 && Input.touchCount < 3)
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
                    LerpTowardsForward();
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("TouchPhase.Moved: " + i);
                    LerpTowardsForward();
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    Debug.Log("TouchPhase.Stationary: " + i);
                    LerpTowardsForward();
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
    public void LerpTowardsForward() //Well copy CameraFollow logic to make the player move towards the carrotToFollow prefab-object
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        //position.y = Mathf.Lerp(this.transform.position.y, carrotToFollow.transform.position.y, interpolation);
        //position.x = Mathf.Lerp(this.transform.position.x, carrotToFollow.transform.position.x, interpolation);
        playerRb.MovePosition(Vector3.Lerp(position, carrotToFollow.transform.position, interpolation));
        //Player ignored walls, figured it out with these:
        //https://answers.unity.com/questions/1493163/using-rigidbodymoveposition-with-a-local-position.html
        //https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html

        this.transform.position = position;
    }
}
