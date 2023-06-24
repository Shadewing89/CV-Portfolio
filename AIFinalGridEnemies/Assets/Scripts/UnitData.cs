using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will use a few public ints and bools to set movement range, attack range, and turn team
public class UnitData : MonoBehaviour
{
    public int attackRange;
    public bool onlyFurthestAttackRange;
    public int movementRange;
    public int teamIndexNumber;
    public List<Material> teamColors;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        TeamColourSet(teamIndexNumber);
    }
    public void TeamColourSet(int teamIndex)
    {
        meshRenderer.material = teamColors[teamIndex];
    }
    
    void Update()
    {
        
    }
}
