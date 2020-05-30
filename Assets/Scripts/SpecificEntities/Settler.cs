using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : Animated
{
    public GameObject healthBar;
    // Start is called before the first frame update

    protected new void Start()
    {
        healthBar = transform.Find(Constants.healthBarGameObjectName).gameObject;
        Events.current.onSingleSelection += HandleSelection;

        healthBar.SetActive(false);
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public void HandleSelection(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
            ApplySelection();
    }

    public void ApplySelection()
    {
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterSelection(this);
    }

    public void ClearSelection()
    {
        this.healthBar.SetActive(false);
    }
}
