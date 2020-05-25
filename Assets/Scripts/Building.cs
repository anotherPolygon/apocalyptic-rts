using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
  

    public List<GameObject> AssignedWorkers = new List<GameObject>();
    public int maxWorkers = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.askAssignToBuildingTrigger += AskAssignToBuildingCallback;
        this.isBuilding = true;
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void AskAssignToBuildingCallback(GameObject bulidingGameObject, GameObject worker)
    {
        if(gameObject == bulidingGameObject && isOwnedByPlayer)
        {
            if(AssignedWorkers.Contains(worker) == false)
                AssignedWorkers.Add(worker);
        }
        
    }

    public void AssignWorkers()
    {
       // implement
    }


}
