using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField] Color ogColor;
    [SerializeField] Color newColor;

    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnMouseOver()
    {
        meshRenderer.material.color = newColor;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = ogColor;
    }
}
