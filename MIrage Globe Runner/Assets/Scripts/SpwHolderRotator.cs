using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwHolderRotator : MonoBehaviour
{
    //https://answers.unity.com/questions/1569460/auto-rotate-object-smoothly-back-and-forth.html
    public float speed = 1f;
    public float rotAngleY = 70f;
    // Update is called once per frame
    void Update()
    {
        float rY = Mathf.SmoothStep(0, rotAngleY, Mathf.PingPong(Time.time * speed, 1f));
        transform.rotation = Quaternion.Euler(0, rY, 0);
    }
}
