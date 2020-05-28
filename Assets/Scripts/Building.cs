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
        GameEvents.current.assignmentEndTrigger += EndAssignmentCallback;
        isBuilding = true;
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void AskAssignToBuildingCallback(
        GameObject bulidingGameObject, GameObject worker)
    {
        // Identify if this bulding is the target building
        if (gameObject == bulidingGameObject)
        {
            if (AssignedWorkers.Contains(worker) == false)
            {
                if(AssignedWorkers.Count < maxWorkers)
                {
                // Accept
                GameEvents.current.buildingAssignmentConfirmed(bulidingGameObject, worker);
                AssignWorker(worker);
                Debug.Log("Building accepts " + worker.name);
                }
                else
                {
                //Refuse
                GameEvents.current.buildingAssignmentDenied(worker, bulidingGameObject);
                Debug.Log("Building is full, thanks");
                }
            }

        }
    }

    public void EndAssignmentCallback(GameObject bulidingGameObject, GameObject worker)
    {
        if (gameObject == bulidingGameObject)
        {
            RemoveWorker(worker);
        }
            
    }

    public void AssignWorker(GameObject worker)
    {
        AssignedWorkers.Add(worker);
    }

    public void RemoveWorker(GameObject worker)
    {
        AssignedWorkers.Remove(worker);
    }



}
