using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    //GameObject plane;

    public Bounds buildingBounds;


    protected new void Start()
    {
        base.Start();
        this.isBuilding = true;
        buildingBounds = gameObject.GetComponent<Collider>().bounds;
        //buildingBounds = gameObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].gameObject.GetComponent<Collider>().bounds;
        Game.Manager.playerBuildingBounds.Add(buildingBounds);
        
        
        // A failed try to create a plane under each building
        //GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //plane.transform.SetParent(transform);
        //plane.transform.localScale = GetComponent<Collider>().bounds.extents;
        //plane.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

    }

    protected new void Update()
    {
        base.Update();
        
    }

}
