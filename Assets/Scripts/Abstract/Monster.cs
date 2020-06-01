using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Animated
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    internal override void InteractWithSettler(Settler settler)
    {
        this.attackers.Add(settler.id, settler);
        settler.StartAttack(this);
    }
}
