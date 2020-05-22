using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    public bool IsOwenedByPlayer = true;

    public List<GameObject> AssignedWorkers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAssignedWorkerToBuildingTrigger += onAssignedWorkerToBuilding;
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void onAssignedWorkerToBuilding(GameObject bulidingGameObject, GameObject worker)
    {
        if(gameObject == bulidingGameObject && IsOwenedByPlayer)
        {
            if(AssignedWorkers.Contains(worker) == false)
                AssignedWorkers.Add(worker);
        }
        
    }


}
