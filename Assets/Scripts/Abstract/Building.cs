using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    protected new void Start()
    {
        base.Start();
        this.isBuilding = true;
    }

    protected new void Update()
    {
        base.Update();
    }
}
