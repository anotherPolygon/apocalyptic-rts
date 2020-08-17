using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Settler : Animated
{
    public GameObject healthBar;
    private Color originalColor;
    public WorkBuilding workPlace;
    public Constants.SettlerRoles currentRole;
    public Constants.ResourceGatheringState currentGatherState;

    public GameObject junkPiece;
    // Resource Gathering:
    public Resource targetResource;
    public Storage assignedResourceStoragePlace;
    float distToTargetResource;
    float minDistanceToTargetResource = 4.5f;
    public bool isGathering = false;

    // Animation parameters
    Animator animator;
    private bool isRunining;
    private bool isDeliveringJunk;
    private bool isCollectingJunk;
    //    #Other boolians may be connected to animaiton from settler or unit properties
    
    protected new void Start()
    {
        base.Start();
        
        InitizalizeState();
        InitizalizeHealthBar();
        InitizalizeEventListeners();
        InitializeAnimatorParameters();

        junkPiece = GetComponentInChildren<JunkPiece>().gameObject;
    }

    // Initializations
    private void InitizalizeState()
    {
        this.isSelected = false;
        this.originalColor = this.unityObjects.childs[Constants.settlerMeshName].renderer.material.color;
        this.currentRole = Constants.SettlerRoles.Worker;  // Defining default role as a worker
        this.currentGatherState = Constants.ResourceGatheringState.NotGathering;
    }

    private void InitializeAnimatorParameters()
    {
        animator = this.unityObjects.childs[Constants.settlerMeshName].animator;
        isRunining = false;
        isCollectingJunk = false;
        isDeliveringJunk = false;
    }

    private void HandleAnimatorParameters()
    {
        animator.SetBool("isRuning", isRunining);
        animator.SetBool("isShooting", isShooting);
        animator.SetBool("isCollecting", isCollectingJunk);
        animator.SetBool("isDelivering", isDeliveringJunk);
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
    
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        HandleState();
    }
    
    // State and Animation Handlers

    private void HandleState()
    {
        HandleWorkStuff();
        HandleCombate();
        HandleGatheringState();
        HandleAnimatorParameters();
        HandleAnimation();            
    }

    private void HandleAnimation()
    {
        

        if (isInCombat)
        {
            //animator.Play("Cowboy_2_Shoot");
        }
        
        else if (CheckNavMeshArrived(this.unityObjects.navMeshAgent))
        {
            // animator.Play("Idle");
            //animator.Play("Cowboy_2_Idle");
            isRunining = false;
        }
        else
        {
            // animator.Play("Run");
            //animator.Play("Cowboy_2_Run");
            isRunining = true;
        }
    }

    internal void QuitEveryThing()
    {
        QuitWork();
        QuitCombat();
        QuitGathering();
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
            // Here adress an if not - manually roled changed - change role automatically
            ChangeRole(_otherEntity);
            InteractWithOtherEntity(_otherEntity);
        }

    }
    
    // Interatcions determinants
    private void ChangeRole(Entity otherEntity)
    {
        if (!otherEntity.isPlayer)
        {
            if (otherEntity.isResource)
                this.currentRole = Constants.SettlerRoles.Gatherer;
            else // Entitiy is Enemy
                this.currentRole = Constants.SettlerRoles.Soldier;

        }
        // owened:
        else if (otherEntity.isBuilding) // WorkBuilding
            this.currentRole = Constants.SettlerRoles.Worker;
    }

    private void InteractWithOtherEntity(Entity otherEntity)
    {
        if (this.currentRole == Constants.SettlerRoles.Soldier)
            otherEntity.GetAttacked(this);
        // this time its wiered because in the follwing no matter what - we reach "InteractWithSettler"
        else if (this.currentRole == Constants.SettlerRoles.Worker)
            otherEntity.InteractWithSettler(this);
        else if (this.currentRole == Constants.SettlerRoles.Gatherer)
            otherEntity.InteractWithSettler(this);
    }

    // Resource Gathering Methods

    private void HandleGatheringState()
    {
        if (isGathering)
        {
            checkIfArrivedToTargetResource();
            StartGathering();
            //StartDelivering();
            StartDroppingResource();
            // maybe another function that starts it all over

            // isTowardsGatheing to flase
            // is Gathering to true

            //else if (this.currentGatherState == Constants.ResourceGatheringState.TowardsResource)
        }
    }
    private void checkIfArrivedToTargetResource()
    {
        if (this.currentGatherState == Constants.ResourceGatheringState.TowardsResource)
        {
            distToTargetResource = Vector3.Distance(transform.position, targetResource.transform.position);
            if (minDistanceToTargetResource >= distToTargetResource)
                this.currentGatherState = Constants.ResourceGatheringState.CollectResource;
        }    
    }

    private void StartGathering()
    {
        // animation
        // stoping motion
        // calling delayed action of + 1 piece
        if (this.currentGatherState == Constants.ResourceGatheringState.CollectResource)
        {
            this.unityObjects.navMeshAgent.SetDestination(transform.position); // in order to stop the setller!
            isCollectingJunk = true;
            Game.Manager.Timer.RegisterTimedAction(this.name + "Deliver Junk", StartDelivering, 100, 10, true);
            this.currentGatherState = Constants.ResourceGatheringState.WaitForColloecrion;
        }

    }
    private void StartDelivering()
    {
        Debug.Log("here");
        this.currentGatherState = Constants.ResourceGatheringState.DeliverResource;
        junkPiece.SetActive(true);
        isCollectingJunk = false;
        isDeliveringJunk = true;
        MoveTo(assignedResourceStoragePlace.gameObject.transform.position);

    }
    private void StartDroppingResource()
    {
        if (this.currentGatherState == Constants.ResourceGatheringState.DeliverResource)
        {
            float distToTargetStorage = Vector3.Distance(transform.position, assignedResourceStoragePlace.transform.position);
            if (2.5f >= distToTargetStorage)
            {
                this.unityObjects.navMeshAgent.SetDestination(transform.position);
                isDeliveringJunk = false;
                junkPiece.SetActive(false);
            }
        }
    }

    internal void InitiateGatheringProcess(Resource gatheredResource, Storage closestStorage)
    {
        QuitEveryThing();
        isGathering = true;
        this.currentGatherState = Constants.ResourceGatheringState.TowardsResource;
        assignedResourceStoragePlace = closestStorage;
        targetResource = gatheredResource;
        MoveTo(targetResource.transform.position);
        Debug.Log(this.name + " started gathering " + gatheredResource + " --> delivered to " + closestStorage.name);
    }

    private void QuitGathering()
    {
        isGathering = true;
        this.currentGatherState = Constants.ResourceGatheringState.NotGathering;
        junkPiece.SetActive(false);

        targetResource = null;
        // What to do if i now start gathering X and quit gathering Y - drop the gathered cube??
    }

    // Combat Methods
    private void Attack()
    {
        animator.Play("Shoot");
    }

    private void HandleCombate()
    {
        if (this.isInCombat)
        {
            this.transform.LookAt(targetOfAttack.transform.position);
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
        this.isShooting = true;
        Game.Manager.Timer.RegisterTimedAction(this.name + "Attack", Attack, 45, 20);
    }

    internal void QuitCombat()
    {
        Game.Manager.Timer.RemoveTimedAction(this.name + "Attack"); // Needs to be fixxed
        this.isInCombat = false;
        this.isShooting = false;
        if(this.targetOfAttack != null)
        {
            this.targetOfAttack.WithdrawAttacker(this);
            this.targetOfAttack = null;
        }
        

    }
    
    // Work Methods
    private void QuitWork()
    {
        if (workPlace != null)
            workPlace.FireWorker(this);
        workPlace = null;
    }

    private void HandleWorkStuff()
    {
        //if (workPlace is null)
        //    //Debug.Log("Not Working");
        //    //ChangeMeshColor(originalColor);
        //else
        //    //Debug.Log("Working"); 
        //    //ChangeMeshColor(workPlace.unityObjects.renderer.material.color);
    }

    internal void StartWorking(WorkBuilding building)
    {
        QuitEveryThing();
        workPlace = building;
        MoveTo(building.unityObjects.transform.position);
        
        //Vector3 cent = building.unityObjects.childs["Cube"].gameObject.GetComponent<Renderer>().bounds.center;
        //float boundX = building.unityObjects.childs["Cube"].gameObject.GetComponent<Renderer>().bounds.size.x;
        //MoveTo(cent + new Vector3(boundX,0,0));
        //Debug.Log(building.unityObjects.childs["Cube"].gameObject.GetComponent<Renderer>().bounds.center);
        //Debug.Log(building.unityObjects.childs["EntryPoint"].transform.position);
        //Debug.Log(building.unityObjects.transform.position);
        //NavMeshObstacle obstacle = building.GetComponent<NavMeshObstacle>();
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
        //this.unityObjects.childs[Constants.settlerMeshName].animator.SetLookAtPosition(point);
        // Line above raises an issue: "Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK
        //                              UnityEngine.Animator:SetLookAtPosition(Vector3)"
        this.unityObjects.navMeshAgent.destination = point;
        
    }

    // Check if we've reached the destination
    private bool CheckNavMeshArrived(NavMeshAgent mNavMeshAgent)
    {
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
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
