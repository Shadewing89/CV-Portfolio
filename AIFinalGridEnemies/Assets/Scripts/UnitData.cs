using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This will use a few public ints and bools to set movement range, attack range, and turn team
//Also enemy AI will be calculated here
public class UnitData : MonoBehaviour
{
    public int attackRange;
    public bool onlyFurthestAttackRange;
    public float movementRange;
    public int teamIndexNumber;
    private int turn;
    private bool aiTurnUsed;
    [SerializeField] List<GameObject> playerUnitsList;
    [SerializeField] List<GameObject> agentsList;
    public List<Material> teamColors;
    public List<GameObject> swordAttackPoints;
    public List<GameObject> spearAttackPoints;
    private MeshRenderer meshRenderer;
    public GameObject attackRangeIndicator;
    public GameObject movementTarget;
    private GameObject closestTarget;

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
    private GameObject FindClosestTarget(List<GameObject> pointList)
    {
        //https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        GameObject tMin = null;
        GameObject cTarget = null;
        float minDist = Mathf.Infinity;
        float minDist2 = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in pointList)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        switch (attackRange) //we search attackrange positions from the target based on the weapon
        {
            case 1:
                foreach (GameObject c in tMin.GetComponent<UnitData>().swordAttackPoints)
                {
                    float dist2 = Vector3.Distance(c.transform.position, currentPos);
                    if (dist2 < minDist2)
                    {
                        cTarget = c;
                        minDist2 = dist2;
                    }
                }
                break;
            case 2: //in the future, add if for more weapons and freeranges
                foreach (GameObject c in tMin.GetComponent<UnitData>().spearAttackPoints)
                {
                    float dist2 = Vector3.Distance(c.transform.position, currentPos);
                    if (dist2 < minDist2)
                    {
                        cTarget = c;
                        minDist2 = dist2;
                    }
                }
                break;
        }
        return cTarget;
    }
    private void AIBehaviour()
    {
        if (teamIndexNumber == 1 && aiTurnUsed == false) //only agents set to ai-team and only once
        {
            movementTarget.SetActive(true);
            aiTurnUsed = true;
            closestTarget = FindClosestTarget(playerUnitsList);
            //https://forum.unity.com/threads/find-a-point-on-a-line-between-two-vector3.140700/ next we calculate if the walkdistance allows us to walk to target
            //or if it needs to be adjusted
            //Vector3 reachablePosition = Vector3.Lerp(gameObject.transform.position, closestTarget.transform.position, movementRange / (gameObject.transform.position - closestTarget.transform.position).Length());
            float dist = Vector3.Distance(closestTarget.transform.position, transform.position);
            if (dist > movementRange)
            {
                Vector3 reachablePosition = movementRange * Vector3.Normalize(closestTarget.transform.position - gameObject.transform.position) + gameObject.transform.position;
                movementTarget.transform.position = reachablePosition;
            }
            else
            {
                movementTarget.transform.position = closestTarget.transform.position;
            }
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
        if (turn == 1 && teamIndexNumber == 1)//end of ai turn
        {
            movementTarget.SetActive(false);
        }
        turn++;
    }
}
