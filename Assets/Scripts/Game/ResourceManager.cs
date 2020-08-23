using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    public List<GameObject> JunkPilesContainers = new List<GameObject>();
    public Dictionary<string, List<Storage>> tag2Storage = new Dictionary<string, List<Storage>>();

    int junk = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Game.Manager.uIManager.junkCount = 5;
        Events.current.onGatheredResource += RaiseResource; // move to local initialzie event listeners
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RaiseResource(Resource resource)
    {
        junk += 1;
        Game.Manager.uIManager.updateResourceText(resource.tag, junk);
    }

}

