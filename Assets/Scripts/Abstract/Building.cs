using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : Entity
{
    //GameObject plane;

    public Bounds buildingBounds;
    public bool isInConstruction = false;
    public int constructionProgress = 100;

    public bool canBeConstructed = true;

    protected new void Start()
    {
        base.Start();
        this.isBuilding = true;
        buildingBounds = gameObject.GetComponent<Collider>().bounds;
        //buildingBounds = gameObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].gameObject.GetComponent<Collider>().bounds;
        common.objects.UnityObjects g;
        if (this.unityObjects.childs.TryGetValue("OverlapFeedback", out g))
            buildingBounds = this.unityObjects.childs["OverlapFeedback"].gameObject.GetComponent<Collider>().bounds;
        Game.Manager.BuildingBoundsDict.Add("Building-"+gameObject.GetInstanceID().ToString(), buildingBounds);
        
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

    public void InitializeConstructionState()
    {
        isInConstruction = true;
        constructionProgress = 0;
        // Removing initally set from gui bounds and Adding the correct one back to game.
        Game.Manager.BuildingBoundsDict.Remove("Building-" + gameObject.GetInstanceID().ToString());
        Game.Manager.BuildingBoundsDict.Add("Building-" + gameObject.GetInstanceID().ToString(), _GetBuildinBounds(gameObject));
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void ConstructionFinished()
    {
        isInConstruction = false;
        constructionProgress = 100;
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<NavMeshObstacle>().enabled = true; // was disable in BuyBuilding

    }

    override internal void InteractWithSettler(Settler settler)
    {
        settler.StartConstruction(this);
    }


        private Bounds _GetBuildinBounds(GameObject buildingGameObject)
    {
        buildingBounds = buildingGameObject.GetComponent<Collider>().bounds;
        common.objects.UnityObjects g;
        if (buildingGameObject.GetComponent<Building>().unityObjects.childs.TryGetValue("OverlapFeedback", out g))
            buildingBounds = buildingGameObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].gameObject.GetComponent<Collider>().bounds;

        return buildingBounds;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag != Constants.terrainGameObjectTag)
        {
            Entity entityBuilding= Game.Manager.GameObject2Entity(collision.gameObject);
            Building otherBuilding = entityBuilding as Building;
            if(otherBuilding != null)
                otherBuilding.canBeConstructed = false;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != Constants.terrainGameObjectTag)
        {
            Entity entityBuilding = Game.Manager.GameObject2Entity(collision.gameObject);
            Building otherBuilding = entityBuilding as Building;
            if (otherBuilding != null)
                otherBuilding.canBeConstructed = true;
        }
    }
}
