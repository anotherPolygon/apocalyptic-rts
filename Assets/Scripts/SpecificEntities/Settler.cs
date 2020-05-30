using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : Animated
{
    public GameObject healthBar;
    private Color originalColor;
    private WorkBuilding workPlace;
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
        Game.Manager.DebugConsole.Log(workPlace, "workPlace");
        HandleState();
    }

    private void HandleState()
    {
        HandleWorkStuff();
    }

    private void HandleWorkStuff()
    {
        if (workPlace is null)
            ChangeMeshColor(originalColor);
        else
            ChangeMeshColor(workPlace.UnityObjects.renderer.material.color);
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
                HandleMoveCommand(hit.point);
        }
        else
        {
            Game.Manager.DebugConsole.Log(otherEntity.UnityObjects.gameObject.name, "clicked on");
            InteractWithOtherEntity(otherEntity);
        }

    }

    private void HandleMoveCommand(Vector3 point)
    {
        QuitWork();
        MoveTo(point);
    }

    private void QuitWork()
    {
        if (workPlace != null)
            workPlace.FireWorker(this);
        workPlace = null;
    }

    private void MoveTo(Vector3 point)
    {
        this.UnityObjects.navMeshAgent.destination = point;
    }

    private void InteractWithOtherEntity(Entity otherEntity)
    {
        otherEntity.PromptForInteraction(this);
    }

    internal void StartWorking(WorkBuilding building)
    {
        QuitWork();
        workPlace = building;
        MoveTo(building.UnityObjects.transform.position);
    }

    private void AssignToWork(WorkBuilding building)
    {
        ChangeMeshColor(building.UnityObjects.renderer.material.color);
    }

    private void ChangeMeshColor(Color color)
    {
        Color _current;
        Color _new;
        _current = this.UnityObjects.childs[Constants.settlerMeshName].renderer.material.color;
        _new = Color.Lerp(_current, color, 0.1f);
        this.UnityObjects.childs[Constants.settlerMeshName].renderer.material.color = _new;
    }
}
