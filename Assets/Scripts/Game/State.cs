using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool inAdditiveSelection = false;
    public Dictionary<int, Settler> selectedSettlers = new Dictionary<int, Settler>();


    public void Update()
    {
        GetInput();
        HandleInput();
    }

    private void GetInput()
    {
        inAdditiveSelection = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleInput()
    {
        
    }

    public void RegisterSelection(Settler settler)
    {
        if (!inAdditiveSelection)
            DeselectAll();
        selectedSettlers.Add(settler.UnityObjects.gameObject.GetInstanceID(), settler);
    }

    public void DeselectAll()
    {
        Settler _settler;
        foreach(KeyValuePair<int, Settler> settler in selectedSettlers)
        {
            _settler = settler.Value;
            _settler.ClearSelection();
        }
        selectedSettlers.Clear();
    }
}
