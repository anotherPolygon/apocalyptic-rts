using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        //GameEvents.current.entitySelectionTrigger += EntitySelectionCallback;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public void Select()
    {
        isSelected = true;
        //healthBar.SetActive(true);
    }

    public void Diselect()
    {
        isSelected = false;
        //healthBar.SetActive(false);
    }

    private void EntitySelectionCallback(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
        {
            Select();
            ReportSelected(gameObject);
            Game.Manager.DebugConsole.Log("Selected!", this.id);
            
        }
        else
        {
            Diselect();
            Game.Manager.DebugConsole.Log("Diselected!", this.id);
        }

    }

    private void ReportSelected(GameObject gameObjectInstance)
    {
        GameEvents.current.reportSelected(gameObjectInstance);
    }
}
