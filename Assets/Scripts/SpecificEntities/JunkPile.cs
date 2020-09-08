using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkPile : Storage
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        this.tag = Constants.junkResourceName;
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();
        
    }
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
