using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool isUnit = false;
    public bool isAnimated = false;
    public bool isMonster = false;
    public bool isFeralWolf = false;
    public bool isSettler = false;
    public bool isBuilding = false;
    public bool isResource = false;

    public bool isPlayer = false;
    public bool isSelected = false;


    // Start is called before the first frame update
    protected void Start()
    {
        Game.Instance.DebugConsole.Log("starting", "Entity");
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }


}
