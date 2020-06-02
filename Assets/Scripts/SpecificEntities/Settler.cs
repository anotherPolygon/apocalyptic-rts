using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : Animated
{
    public GameObject healthBar;
    private Color originalColor;
    private WorkBuilding workPlace;

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
        this.originalColor = this.unityObjects.childs[Constants.settlerMeshName].renderer.material.color;
        // Defining default role as a worker
        this.currentRole = Game.SettlerRoles.Worker;
    }

    internal void QuitEveryThing()
    {
        QuitWork();
        QuitCombat();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        HandleState();
    }

    private void HandleState()
    {
        HandleWorkStuff();
        HandleCombate();
    }

    // Initializations
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

    // General Action 
    private void HandleGeneralAction(RaycastHit hit)
    {
        Entity _otherEntity;
        _otherEntity = Game.Manager.GameObject2Entity(hit.collider.gameObject);
        if (_otherEntity is null)
        {
            if (common.Utils.IsTerrain(hit.collider.gameObject))
                HandleMoveCommand(hit.point);
        }
        else
        {
            ChangeRole(_otherEntity);
            InteractWithOtherEntity(_otherEntity);
        }

    }
    
    // Interatcions determinants
    private void ChangeRole(Entity otherEntity)
    {
        if (!otherEntity.isPlayer) // Entitiy is Enemy
            this.currentRole = Game.SettlerRoles.Soldier;
        else // owened or terrain
            this.currentRole = Game.SettlerRoles.Worker;
    }

    private void InteractWithOtherEntity(Entity otherEntity)
    {
        //otherEntity.InteractWithSettler(this);
        otherEntity.InteractWithSettlerAlterntive(this);
    }

    // Combat Methods
    private void HandleCombate()
    {
        if (this.isInCombat)
        {
            ChangeMeshColor(this.targetOfAttack.unityObjects.childs[Constants.fearlWolfMeshName].renderer.material.color);
        }
            
        else
        {
            ChangeMeshColor(originalColor); 
        }
            
    }

    internal override void StartAttack(Entity otherEntityUnderAttck)
    {
        QuitEveryThing();
        this.targetOfAttack = otherEntityUnderAttck;
        this.isInCombat = true;
    }

    internal void QuitCombat()
    {
        this.isInCombat = false;
        if(this.targetOfAttack != null)
        {
            this.targetOfAttack.WithdrawAttacker(this);
            this.targetOfAttack = null;
        }
        
        
        
    }
    // Work Methods
    private void HandleWorkStuff()
    {
        if (workPlace is null)
            ChangeMeshColor(originalColor);
        else
            ChangeMeshColor(workPlace.unityObjects.renderer.material.color);
    }

    internal void StartWorking(WorkBuilding building)
    {
        QuitEveryThing();
        workPlace = building;
        MoveTo(building.unityObjects.transform.position);
    }
    private void QuitWork()
    {
        if (workPlace != null)
            workPlace.FireWorker(this);
        workPlace = null;
    }

    private void AssignToWork(WorkBuilding building)
    {
        ChangeMeshColor(building.unityObjects.renderer.material.color);
    }

    // Move Methods
    private void HandleMoveCommand(Vector3 point)
    {
        QuitEveryThing();
        MoveTo(point);
    }

    private void MoveTo(Vector3 point)
    {
        this.unityObjects.navMeshAgent.destination = point;
    }

    // Selection methods
    public void HandleSingleSelection(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
            ApplySelection();
    }

    public void ApplySelection(bool isFromMultipleSelection = false)
    {
        this.isSelected = true;
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterSingleSelection(this, isFromMultipleSelection);

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
            ApplySelection(true);
        }
    }

    private void ApplyMultipleSelection()
    {
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterMultipleSelection(this);
    }


    // Changeing Color
    private void ChangeMeshColor(Color color)
    {
        Color _current;
        Color _new;
        _current = this.unityObjects.childs[Constants.settlerMeshName].renderer.material.color;
        _new = Color.Lerp(_current, color, 0.1f);
        if (_new != _current)
            this.unityObjects.childs[Constants.settlerMeshName].renderer.material.color = _new;
    }
}
