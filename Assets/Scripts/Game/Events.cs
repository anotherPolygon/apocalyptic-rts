using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events current;

    public void Awake()
    {
        current = this;
    }

    public event Action<GameObject> onSingleSelection;
    public void SingleSelection(GameObject target)
    {
        if (onSingleSelection != null)
            onSingleSelection(target);
    }
}
