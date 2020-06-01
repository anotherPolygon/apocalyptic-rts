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
        HandleState();
    }

    private void HandleState()
    {
        inAdditiveSelection = Input.GetKey(KeyCode.LeftShift);
    }

    private void AddToSelection(Settler settler)
    {
        selectedSettlers.Add(settler.unityObjects.gameObject.GetInstanceID(), settler);
    }

    public void RegisterSingleSelection(Settler settler, bool isFromMultipleSelection)
    {
        if (!inAdditiveSelection & !isFromMultipleSelection)
            DeselectAll();
        AddToSelection(settler);
    }

    public void RegisterMultipleSelection(Settler settler)
    {
        AddToSelection(settler);
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

    public override string ToString()
    {
        string result = "";
        result += "selectedSettlers: (";
        result += selectedSettlers.Count.ToString();
        result += ") - ";
        result += selectedSettlers.Keys.ToString();
        return result;
    }

}
