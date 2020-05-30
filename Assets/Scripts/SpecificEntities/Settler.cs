using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : Animated
{
    public GameObject healthBar;
    private Color originalColor;
    // Start is called before the first frame update

    protected new void Start()
    {
        base.Start();

        InitizalizeState();
        InitizalizeHealthBar();
        InitizalizeEventListeners();
    }

    private void InitizalizeState()
    {
        this.isSelected = false;
        this.originalColor = this.UnityObjects.childs[Constants.settlerMeshName].renderer.material.color;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    private void InitizalizeHealthBar()
    {
        healthBar = transform.Find(Constants.healthBarGameObjectName).gameObject;
        healthBar.SetActive(false);
    }
    private void InitizalizeEventListeners()
    {
        Events.current.OnSingleSelection += HandleSingleSelection;
        Events.current.OnMultipleSelection += HandleMultipleSelection;
    }

    private void RegisterForSelectedStateEvents()
    {
        Events.current.OnRequestAction += HandleGeneralAction;
    }

    private void UnregisterFromSelectedStateEvents()
    {
        Events.current.OnRequestAction -= HandleGeneralAction;
    }

    public void HandleSingleSelection(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
            ApplySingleSelection();
    }

    public void ApplySingleSelection()
    {
        this.isSelected = true;
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterSingleSelection(this);

        RegisterForSelectedStateEvents();
    }

    public void ClearSelection()
    {
        this.isSelected = false;
        this.healthBar.SetActive(false);

        UnregisterFromSelectedStateEvents();
    }

    public void HandleMultipleSelection(Bounds box)
    {
        // TODO: this is not ideal. Mouse object should identify objects in the box
        //       and call SingleSelection for each selected object individually.
        //       but for now this will have to do.
        Vector3 _screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        _screenPosition.z = 0;
        if (box.Contains(_screenPosition))
        {
            ApplyMultipleSelection();
        }
    }

    private void ApplyMultipleSelection()
    {
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterMultipleSelection(this);
    }

    private void HandleGeneralAction(RaycastHit hit)
    {
        Entity otherEntity;
        otherEntity = Game.Manager.GameObject2Entity(hit.collider.gameObject);

        if (otherEntity is null)
        {
            if (common.Utils.IsTerrain(hit.collider.gameObject))
                MoveTo(hit.point);
        }
        else
            InteractWithOtherEntity(otherEntity);

    }

    private void MoveTo(Vector3 point)
    {
        //UActions.MoveToPostion(agent, point, positionSpace);
        //agent.destination = position + new Vector3(positionSpace, 0, positionSpace);
        //
        this.UnityObjects.navMeshAgent.destination = point;
    }

    private void InteractWithOtherEntity(Entity otherEntity)
    {
        throw new NotImplementedException();
    }

}
