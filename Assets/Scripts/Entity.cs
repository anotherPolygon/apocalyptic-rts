using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    public GameObject player;
    private MouseInputManager mouseInput;

    public bool isSelected = false;
    public int health = 100;
    public int maxHealth = 100;
    public Transform HBtrasform;

    // Identity definition -  will be deprecated by specific entity:
    public bool isUnit = false;
    public bool isBuilding = false;
    public bool isResource = false;

    public string entityName; // to be defined via editor

    // To be defined in inhertence on Start():
    public bool isOwnedByPlayer = true;
    public bool isEnemy = false;

    // into Unit Script:
    public UnitsActions UActions;// A script the contains Units actions
    GameObject currentAssignedBuilding; // current building that
    public NavMeshAgent agent;

    public enum UnitsMethods 
    {
        Work,
        Gather, 
        Guard,
        Repair,
        Steal,
        Attack,
        Trade, 
    }

    public UnitsMethods currentMethod;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        mouseInput = player.GetComponent<MouseInputManager>();

        HBtrasform = transform.Find("HB");

        // Selection events
        GameEvents.current.entitySelectionTrigger += EntitySelectionCallback;
        GameEvents.current.multiSelectionTrigger += MultiSelcetionCallback;
        // Method events
        GameEvents.current.executeMainMethodTrigger += ExecuteMainMethodCallback;
        GameEvents.current.buildingAssignmentConfirmedTrigger += assignmentStartCallbck;

        // Setting a defauls method
        currentMethod = UnitsMethods.Work;

        UActions = new UnitsActions();
        currentAssignedBuilding = null;
    }
    
     private void ExecuteMainMethodCallback(List<GameObject> selectedGameObjectsList, GameObject targetObject, Vector3 point)
     {
        if (isSelected)
        {
            // targetObject is an terrain:
            if (targetObject.name == "terrain")
            {
                // calculating the space needed in multiple selection
                float positionSpace = selectedGameObjectsList.IndexOf(gameObject);
                // initating movement
                UActions.MoveToPostion(agent, point, positionSpace);
                GameEvents.current.assignmentEnd(currentAssignedBuilding, gameObject);
                currentAssignedBuilding = null;
            }

            // targetObject is an entity:
            else
            {
                // Grabbing the targetObject Entity cast script
                Entity gameObjectClass = targetObject.GetComponent<Entity>();

                currentMethod = UnitsMethods.Work;
                // Player's side
                if (gameObjectClass.isOwnedByPlayer)
                {
                    if (gameObjectClass.isBuilding)
                    {
                        // Work, Repair or Guard
                        if (currentMethod == UnitsMethods.Guard)
                        {
                            // Guard Own Building
                        }
                        else if (currentMethod == UnitsMethods.Repair)
                        {
                            // Repair Own Building
                        }
                        else
                        {
                            // Work Own Building
                            Debug.Log("" + gameObject + " asks to assign to " + targetObject);
                            UActions.askAssignToBuilding(targetObject, gameObject);
                            
                        }
                    }
                    else if (gameObjectClass.isUnit)
                    {

                    }

                    
                    
                }
                // Not Owned By Player
                else
                {
                    // Resource
                    if (gameObjectClass.isResource)
                    {

                    }
                    // Enemy
                    else if (gameObjectClass.isEnemy)
                    {

                    }
                    // A neutral NPC
                    else
                    {

                    }

                }




            }
            
        }
    }

    private void EntitySelectionCallback(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
        {
            isSelected = true;
            ShowThatEntityIsSelected(isSelected);
            ReportSelected(gameObject);

        }
        else
        {
            isSelected = false;
            ShowThatEntityIsSelected(isSelected);
        }

    }

    private void MultiSelcetionCallback(Bounds selectionBoxBounds)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        screenPos.z = 0;
        if (selectionBoxBounds.Contains(screenPos))
        {
            isSelected = true;
            ReportSelected(gameObject);
            ShowThatEntityIsSelected(isSelected);
        }
        // Tryin to deselects when box leaves
        //else if (isSelected)
        //    isSelected = false;
        //    ShowThatEntityIsSelected(isSelected);


    }

    private void ReportSelected(GameObject gameObjectInstance)
    {
        GameEvents.current.reportSelected(gameObjectInstance);
    }

        private void OnDestroy()
    {
        GameEvents.current.entitySelectionTrigger -= EntitySelectionCallback;
        GameEvents.current.multiSelectionTrigger -= MultiSelcetionCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // A method the indeactes the an entity is currently selected
    public void ShowThatEntityIsSelected(bool selectionBool)
    {
        if(HBtrasform != null)
            HBtrasform.gameObject.SetActive(selectionBool);
    }

    public void assignmentStartCallbck(GameObject bulidingGameObject, GameObject worker)
    {
        GameEvents.current.assignmentEnd(currentAssignedBuilding, worker);
        currentAssignedBuilding = bulidingGameObject;
    }
}
