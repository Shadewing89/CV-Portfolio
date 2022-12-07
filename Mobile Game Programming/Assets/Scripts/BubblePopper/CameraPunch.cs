using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraPunch : MonoBehaviour
{
    public static CameraPunch Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void PunchCamera(Vector3 punchDirection, int ballAmount)
    {
        if(ballAmount > 4)
        {
            transform.DOPunchPosition(punchDirection.normalized * 0.3f, 0.7f, 0);
        }
    }
}
