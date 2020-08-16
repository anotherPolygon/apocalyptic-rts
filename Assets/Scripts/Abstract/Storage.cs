using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Building
{
    public bool isStorage;

    protected new void Start()
    {
        base.Start();
        this.isStorage = true;

        // Registrating this storage is a place that hold the resource of this tag
        // builds on the tags of storgae and resource to have the same name
        Debug.Log("Added this tag-> " + this.tag);
        List<Storage> storagePlaces;
        if (!Game.Manager.RM.tag2Storage.TryGetValue(this.tag, out storagePlaces))
            Game.Manager.RM.tag2Storage.Add(this.tag, storagePlaces = new List<Storage>());
        else
            Game.Manager.RM.tag2Storage[this.tag].Add(this);
        storagePlaces.Add(this);
        //Game.Manager.RM.tag2Storage.Add(this.tag,this); // CHECK C# NEW AND OLD KEYS IN THIS METHOD
        Debug.Log(Game.Manager.RM);
        Game.Manager.DebugConsole.Log(Game.Manager.RM.tag2Storage, "Storage");
    }

    protected new void Update()
    {
        base.Update();
    }
}
