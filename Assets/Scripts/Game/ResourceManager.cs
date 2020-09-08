using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    public Dictionary<string, List<Storage>> tag2Storage;
    private Dictionary<string, int> resourceName2units = new Dictionary<string, int>();

    int junk = 0;

    private void Awake()
    {
        tag2Storage = new Dictionary<string, List<Storage>>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Events.current.onGatheredResource += RaiseResource; // move to local initialzie event listeners
        resourceName2units.Add(Constants.junkResourceName, junk);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void RaiseResource(string resourceName)
    {
        Debug.Log("b");
        resourceName2units[resourceName] += 1;
        Game.Manager.uIManager.updateResourceText(resourceName, resourceName2units[resourceName]);
    }

    private void SubstractResource(string resourceName, int ammount)
    {
        resourceName2units[resourceName] -= ammount;
        Game.Manager.uIManager.updateResourceText(resourceName, resourceName2units[resourceName]);
    }

    public bool verifAndApplyCost(int price, string resouceToCharge)
    {
        if (price <= resourceName2units[resouceToCharge])
        {
            SubstractResource(resouceToCharge, price);
            return true;
        }
        else
            return false;

    }
}

