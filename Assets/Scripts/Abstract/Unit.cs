using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    private GameObject HealthBar;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        HealthBar = transform.Find("HealthBar").gameObject;

        GameEvents.current.entitySelectionTrigger += EntitySelectionCallback;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public void Select()
    {
        isSelected = true;
        HealthBar.SetActive(true);
    }

    public void Diselect()
    {
        isSelected = false;
        HealthBar.SetActive(false);
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
