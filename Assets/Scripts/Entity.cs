using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject player;
    private MouseInputManager mouseInput;

    public bool isSelected = false;
    public int health = 100;
    public int maxHealth = 100;

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

        player = GameObject.Find("Player");
        mouseInput  = player.GetComponent<MouseInputManager>();

        GameEvents.current.onEntitySelectionTigger += onEntitySeletcion;
        GameEvents.current.onUnitMultiSelectTrigger += onUnitMultiSelect;

    }

    private void onEntitySeletcion(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
        {
            isSelected = true;
            onSelectedTrigger(gameObject);

        }
        else
        {
            isSelected = false;
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
        }

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
        ShowThatEntityIsSelected();
    }

    // A method the indeactes the an entity is currently selected
    public void ShowThatEntityIsSelected()
    {
       // throw new NotImplementedException();
    }
}
