using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField] Color ogColor;
    [SerializeField] Color newColor;
    [SerializeField] Color walkableColor;
    [SerializeField] Color attackAimColor;

    MeshRenderer meshRenderer;
    public bool withinWalkableCol;
    bool isHovering;
    bool isAiming;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        withinWalkableCol = false;
        isAiming = false;
        isHovering = false;
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
        else if (other.tag == "AttackRange")
        {
            isAiming = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isAiming = false;
        withinWalkableCol = false;
    }
    private void Update()
    {
        if(!isHovering)
        {
            if (isAiming)
            {
                meshRenderer.material.color = attackAimColor;
            }
            else if (withinWalkableCol)
            {
                meshRenderer.material.color = walkableColor;
            }
            else
            {
                meshRenderer.material.color = ogColor;
            }
        }
    }
    private void OnEnable()
    {
        TurnOrder.OnTurnChange += ResetWalkArea;
    }
    private void OnDisable()
    {
        TurnOrder.OnTurnChange -= ResetWalkArea;
    }
    void ResetWalkArea()
    {
        withinWalkableCol = false;
        isAiming = false;
        isHovering = false;
    }
}
