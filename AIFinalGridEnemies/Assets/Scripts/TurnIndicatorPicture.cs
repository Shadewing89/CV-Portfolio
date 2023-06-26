using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicatorPicture : MonoBehaviour
{
    public Image teamColour;
    private bool changingTeamToRed;
    
    void Start()
    {
        changingTeamToRed = true;
        ChangeTeamColour();
    }

    private void OnEnable()
    {
        TurnOrder.OnTurnChange += ChangeTeamColour;
    }
    private void OnDisable()
    {
        TurnOrder.OnTurnChange -= ChangeTeamColour;
    }
    private void ChangeTeamColour()
    {
        if (!changingTeamToRed)
        {
            teamColour.color = Color.blue;
        }
        else
        {
            teamColour.color = Color.red;
        }
        changingTeamToRed = !changingTeamToRed;
    } 
}
