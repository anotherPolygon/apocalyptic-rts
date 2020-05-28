using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingJunkPile : Building
{
    public ResourceManager RM;

    // Start is called before the first frame update
    void Start()
    {
        // Referencing the ResourceManager script
        RM = GameObject.Find("Player").GetComponent<ResourceManager>();

        // registering a junk resource
        RM.JunkPilesContainers.Add(gameObject);

        // Dosent come via inhertince -- BECAUSE THE CURRENT START DEPRACATES THE ORIGINAL
        GameEvents.current.askAssignToBuildingTrigger += AskAssignToBuildingCallback;
        GameEvents.current.assignmentEndTrigger += EndAssignmentCallback;
        isBuilding = true;

    }

    private void OnDestroy()
    {
        // Removing the junk pile container from the list
        RM.JunkPilesContainers.Remove(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
