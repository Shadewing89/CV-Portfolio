using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camGreatParentObject;
    public Transform camParentObject;
    public float moveSpeed;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;
    public float rotateSpeed;
    private float curZoom;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.position.y;
        curXRot = 45f;
    }

    void Update()
    {
        //Zooming with scroll wheel
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
        //Edited code, need to tweak how rotations and zoom affect the camera
        cam.transform.position = new Vector3(camGreatParentObject.position.x, camParentObject.position.y, camGreatParentObject.position.z) + (cam.transform.forward * -curZoom);
        //cam.transform.localPosition = Vector3.up * -curZoom;

        //Rotating camera with right mouse button
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        //Moving camera with WASD
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();
        Vector3 right = cam.transform.right.normalized;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        transform.position += dir;
    }
}
