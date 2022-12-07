using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotation : MonoBehaviour
{
    private Gyroscope gyroscope;
    private bool isGyroAvailable;

    private static readonly float gravityMultiplier = -9.81f;
    private static readonly Vector3 defaultGravity = new Vector3(0, gravityMultiplier, 0f);

    void Start()
    {
        isGyroAvailable = SystemInfo.supportsGyroscope;

        if (isGyroAvailable)
        {
            Input.gyro.enabled = true;
            gyroscope = Input.gyro;
        }

        Physics.gravity = defaultGravity;
    }

    void Update()
    {
        if (isGyroAvailable)
        {
            Vector3 gyroRotation = new Vector3(gyroscope.gravity.x * -gravityMultiplier, gyroscope.gravity.y * -gravityMultiplier, 0f);
            Physics.gravity = gyroRotation;
            var lookTarget = transform.position + gyroRotation;
            transform.LookAt(lookTarget);
        }
    }
}
