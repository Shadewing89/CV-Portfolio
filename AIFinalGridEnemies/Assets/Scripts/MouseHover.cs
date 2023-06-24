using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField] Color ogColor;
    [SerializeField] Color newColor;
    [SerializeField] Color walkableColor;

    MeshRenderer meshRenderer;
    bool withinWalkableCol;
    bool isHovering;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        withinWalkableCol = false;
    }
    private void OnMouseOver()
    {
        isHovering = true;
        if (withinWalkableCol)
        {
            meshRenderer.material.color = newColor;
        }
    }
    private void OnMouseExit()
    {
        isHovering = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            withinWalkableCol = false;
        }
        else if (other.tag == "WalkPath")
        {
            withinWalkableCol = true;
        }
    }
    private void Update()
    {
        if(!isHovering)
        {
            if (withinWalkableCol)
            {
                meshRenderer.material.color = walkableColor;
            }
            else
            {
                meshRenderer.material.color = ogColor;
            }
        }
    }
}
