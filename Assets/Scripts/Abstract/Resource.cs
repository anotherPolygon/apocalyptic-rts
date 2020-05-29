using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Entity
{
    public ResourceManager RM;
    public List<GameObject> AssociatedDropOffBuildig;

    // Start is called before the first frame update
    protected new void Start()
    {
        // Defining the entity as Resource
        this.isResource = true;
        
        // gettin a reffernce to the resource manager
        RM = GameObject.Find("Player").GetComponent<ResourceManager>();

        // linking betwee resource(junk) to the list that holds *player's* JunkPilesContainers
        // Pay attention to how can i know the containers are owned by player??
        AssociatedDropOffBuildig = RM.JunkPilesContainers;
    }

   

    // Update is called once per frame
    protected new void Update()
    {
        
    }
}
