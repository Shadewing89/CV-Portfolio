using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    private int turn;
    private int pScore;
    private int cScore;
    public TMP_Text playerScore;
    public TMP_Text comScore;
    void Start()
    {
        turn = 1;
        pScore = 0;
        cScore = 0;
    }
    
    void Update()
    {
        if (turn > 1)
        {
            turn = 0;
        }
        playerScore.SetText(pScore.ToString());
        comScore.SetText(cScore.ToString());
    }
    private void OnEnable()
    {
        TurnOrder.OnTurnChange += IncrementTurn;
        UnitData.OnPlayerPoint += CheckPlayerScore;
        UnitData.OnComPoint += CheckComScore;
    }
    private void OnDisable()
    {
        TurnOrder.OnTurnChange -= IncrementTurn;
        UnitData.OnPlayerPoint -= CheckPlayerScore;
        UnitData.OnComPoint -= CheckComScore;
    }
    
    private void IncrementTurn() //will need an added check of listing the agents again if more get spawned
    {
        
        if (turn == 0) //at the end of the player turn
        {
            
        }
        if (turn == 1)//end of ai turn
        {
            
        }
        turn++;
    }
    private void CheckPlayerScore()
    {
        pScore++;
    }
    private void CheckComScore()
    {
        cScore++;
    }
}
