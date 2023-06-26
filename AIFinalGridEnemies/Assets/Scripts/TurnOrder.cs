using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder : MonoBehaviour
{
    [SerializeField] List<GameObject> playerUnitsList;
    [SerializeField] List<GameObject> agentsList;
    public delegate void TurnChange();
    public static event TurnChange OnTurnChange;


    void Start()
    {
        ListAgents();
    }
    private void ListAgents()
    {
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            if (go.tag == "Player" && go.transform.parent == null)
            {
                switch (go.GetComponent<UnitData>().teamIndexNumber)
                {
                    case 0:
                        playerUnitsList.Add(go);
                        break;
                    case 1:
                        agentsList.Add(go);
                        break;
                }
            }
        }
    }
    public void ChangeTurn()
    {
        if (OnTurnChange != null) //if event is not null, call event to inform agents of turn change
        {
            OnTurnChange();
        }
    }
}
