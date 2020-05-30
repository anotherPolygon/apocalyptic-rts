using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : Animated
{
    public GameObject healthBar;
    // Start is called before the first frame update

    protected new void Start()
    {
        base.Start();

        InitizalizeHealthBar();
        InitizalizeEventListeners();
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

    public void HandleSingleSelection(GameObject selectedObject)
    {
        if (selectedObject == gameObject)
            ApplySingleSelection();
    }

    public void ApplySingleSelection()
    {
        this.healthBar.SetActive(true);
        Game.Manager.State.RegisterSingleSelection(this);
    }

    public void HandleMultipleSelection(Bounds box)
    {
        // this is not ideal. Mouse object should identify objects in the box
        // and call SingleSelection for each selected object individually.
        // but for now this will have to do.
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

    public void ClearSelection()
    {
        this.healthBar.SetActive(false);
    }
}
