using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated : Unit
{
    public float ProgressDistance;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        ProgressDistance = 5f;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
