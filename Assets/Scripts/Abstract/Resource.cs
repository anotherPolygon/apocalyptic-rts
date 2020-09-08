using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Entity
{
    public ResourceManager RM;
    private List<Storage> storagePlaces;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        // Defining the entity as Resource
        this.isResource = true;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    override internal void InteractWithSettler(Settler settler)
    {
        Storage closestStorge = GetClosestDropoffPoint(settler.isPlayer);
        settler.InitiateGatheringProcess(this, closestStorge);
        Debug.Log(this.name + " Interacting with " + settler.name);
    }

    public Storage GetClosestDropoffPoint(bool isPlayer)
    {
        storagePlaces = Game.Manager.RM.tag2Storage[this.tag];
        Storage closestStorge = null;

        float minDist = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Storage s in storagePlaces)
        {
            if (s.isPlayer)
            {
                float dist = Vector3.Distance(s.transform.position, currentPosition);
                if (dist < minDist)
                {
                    closestStorge = s;
                    minDist = dist;
                }
            }
        }

        //common.Utils.FindClosestEntityType(gameObject, storagePlaces);
       // adress if player requsted and if  storage owned by player --> probably iside the if

        return closestStorge;
    }

}
