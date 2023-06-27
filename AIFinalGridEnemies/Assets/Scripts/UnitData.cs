using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will use a few public ints and bools to set movement range, attack range, and turn team
//Also enemy AI will be calculated here
public class UnitData : MonoBehaviour
{
    public int attackRange;
    public bool onlyFurthestAttackRange;
    public int movementRange;
    public int teamIndexNumber;
    private int turn;
    private bool aiTurnUsed;
    [SerializeField] List<GameObject> playerUnitsList;
    [SerializeField] List<GameObject> agentsList;
    public List<Material> teamColors;
    private MeshRenderer meshRenderer;
    public GameObject attackRangeIndicator;
    public GameObject movementTarget;

    void Start()
    {
        turn = 1;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        TeamColourSet(teamIndexNumber);
        aiTurnUsed = false;
        ListAgents();
    }
    public void TeamColourSet(int teamIndex)
    {
        meshRenderer.material = teamColors[teamIndex];
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

    void Update()
    {
        if (turn > 1)
        {
            turn = 0;
        }

        if (turn == teamIndexNumber)
        {
            attackRangeIndicator.SetActive(true);
        }
        else
        {
            attackRangeIndicator.SetActive(false);
        }

        switch (turn)
        {
            case 0:
                //control on player
                break;
            case 1:
                //control on ai, move closer to closest player -attackrange offset  
                AIBehaviour();
                break;
        }
    }
    private GameObject FindClosestTarget(List<GameObject> agentsList)
    {
        //https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in agentsList)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    private void AIBehaviour()
    {
        if (teamIndexNumber == 1 && aiTurnUsed == false) //only agents set to ai-team and only once
        {
            aiTurnUsed = true;
            GameObject closestTarget = FindClosestTarget(agentsList);

        }
    }
    private void OnEnable()
    {
        TurnOrder.OnTurnChange += IncrementTurn;
    }
    private void OnDisable()
    {
        TurnOrder.OnTurnChange -= IncrementTurn;
    }
    private void IncrementTurn() //will need an added check of listing the agents again if more get spawned
    {
        if (turn == 0) //at the end of the player turn, activate ai bool with setting to false
        {
            aiTurnUsed = false;
        }
        turn++;
    }
}
