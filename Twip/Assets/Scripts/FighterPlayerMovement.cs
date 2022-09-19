using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterPlayerMovement : MonoBehaviour
{
    // brackeys tutorial https://www.youtube.com/watch?v=dwcT-Dch0bA
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }
    private void FixedUpdate()
    {
        controller.Move(horizMove * Time.fixedDeltaTime, false, false);
    }
}
