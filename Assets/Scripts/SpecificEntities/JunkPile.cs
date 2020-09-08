using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkPile : Storage
{
    // Start is called before the first frame update
    protected new void Start()
    {
            
        base.Start();
        //// Referencing the ResourceManager script
        //RM = GameObject.Find("Player").GetComponent<ResourceManager>();
        //
        //// registering a junk resource
        //RM.JunkPilesContainers.Add(gameObject);
        //
        //// Dosent come via inhertince -- BECAUSE THE CURRENT START DEPRACATES THE ORIGINAL
        //GameEvents.current.askAssignToBuildingTrigger += AskAssignToBuildingCallback;
        //GameEvents.current.assignmentEndTrigger += EndAssignmentCallback;
        //isBuilding = true;

    }

    protected new void OnDestroy()
    {
        base.OnDestroy();
        // Removing the junk pile container from the list
        //RM.JunkPilesContainers.Remove(gameObject);
    }
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
