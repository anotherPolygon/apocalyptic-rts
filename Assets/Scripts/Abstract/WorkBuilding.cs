using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBuilding : Building
{
    Dictionary<int, Settler> workers = new Dictionary<int, Settler>();
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

    override internal void InteractWithSettler(Settler settler)
    {
        workers.Add(settler.id, settler);
        settler.StartWorking(this);
    }

    internal void FireWorker(Settler settler)
    {
        workers.Remove(settler.id);
    }
}
