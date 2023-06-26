using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAreaFollow : MonoBehaviour
{
    bool playerTurn;
    public GameObject walkAreaCollider;
    public GameObject trackedPlayer;
    void Start()
    {
        playerTurn = false;
    }


    void Update()
    {
        if (playerTurn)
        {
            walkAreaCollider.SetActive(true);
        }
        else
        {
            walkAreaCollider.SetActive(false);
        }
    }
    private void OnEnable()
    {
        TurnOrder.OnTurnChange += TurnIsChanging;
    }
    private void OnDisable()
    {
        TurnOrder.OnTurnChange -= TurnIsChanging;
    }
    void TurnIsChanging()
    {
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            gameObject.transform.position = trackedPlayer.transform.position;
        }
    }
}
