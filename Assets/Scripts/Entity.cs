using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    public GameObject player;
    private MouseInputManager mouseInput;
    private NavMeshAgent agent;
    

    public bool isSelected = false;
    public int health = 100;
    public int maxHealth = 100;
    public Transform HBtrasform;

    // The following enum "InteractionOptions" holds all the action an entity may RECIEVE from antoher entity
    //      these option will be associated with a method that exists at the recieving entity (this method will
    //      get information from the acting entity, for example how severe is its attack power)
    public enum InteractionOptions
    {
        getAttacked,
        getRepaired,
        getHealed,
        getUsed,
        getKiddnaped,
        getWorked,
        getGuarded,
    }

    // The following enum "ActionOptions" holds all the action an entity may apply on another entity
    public enum ActionOptions
    {
        Attack,
        Repair,
        Heal,
        Use,
        Kiddnap,
        work,
        Guard,
    }


    // Start is called before the first frame update
    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        mouseInput = player.GetComponent<MouseInputManager>();

        HBtrasform = transform.Find("HB");

        GameEvents.current.onApplyMainObjectMethodTrigger += onApplyMainObjectMethod;
        GameEvents.current.onEntitySelectionTigger += onEntitySeletcion;
        GameEvents.current.onUnitMultiSelectTrigger += onUnitMultiSelect;

    }
    
     private void onApplyMainObjectMethod(List<Entity> selectedEntetiesList, GameObject targetObject, Vector3 point)
     {
        if (isSelected)
        { 
            int i = 0;
            foreach(Entity entity in selectedEntetiesList)
            {
                if(entity.agent != null)
                {
                    entity.agent.destination = point + new Vector3(0, 0, i);
                    i += 2;
                }
                    
            }
                //agent.destination = point;
        }

            
        //if (selectedObject == gameObject)
        //{
        //    Debug.Log("" + gameObject + " doing " + point);
        //    agent.destination = point;
        //}
    }

    private void onEntitySeletcion(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
        {
            isSelected = true;
            ShowThatEntityIsSelected(isSelected);
            onSelectedTrigger(gameObject);

        }
        else
        {
            isSelected = false;
            ShowThatEntityIsSelected(isSelected);
        }

    }

    private void onUnitMultiSelect(Bounds selectionBoxBounds)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        screenPos.z = 0;
        if (selectionBoxBounds.Contains(screenPos))
        {
            isSelected = true;
            onSelectedTrigger(gameObject);
            ShowThatEntityIsSelected(isSelected);
        }
        // Tryin to deselects when box leaves
        //else if (isSelected)
        //    isSelected = false;
        //    ShowThatEntityIsSelected(isSelected);


    }

    private void onSelectedTrigger(GameObject gameObjectInstance)
    {
        GameEvents.current.selectedTrigger(gameObjectInstance);
    }

        private void OnDestroy()
    {
        GameEvents.current.onEntitySelectionTigger -= onEntitySeletcion;
        GameEvents.current.onUnitMultiSelectTrigger -= onUnitMultiSelect;
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
}
