using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : Resource
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        this.name = Constants.junkResourceName; // its no the name of the game object - this.gameObject.name is the name shown in the edito
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
