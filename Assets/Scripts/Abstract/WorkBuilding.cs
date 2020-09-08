using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBuilding : Building
{
    public Dictionary<int, Settler> workers = new Dictionary<int, Settler>();
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
        if (this.isInConstruction)
        {
            base.InteractWithSettler(settler);
        }
        else
        {
            workers.Add(settler.id, settler);
            settler.StartWorking(this);
            PrintDictionaryOnChange(workers);
        }
    }

    internal void FireWorker(Settler settler)
    {
        workers.Remove(settler.id);
        PrintDictionaryOnChange(workers);
    }

    internal void PrintDictionaryOnChange(Dictionary<int, Settler> dict)
    {
        string mess = "Workers in " + this.name;
        foreach (var item in dict)
        {
            mess = mess + " " + item.Key;
        }

        Debug.Log(mess);
    }

}


