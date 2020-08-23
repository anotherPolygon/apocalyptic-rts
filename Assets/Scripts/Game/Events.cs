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

    public event Action<GameObject> OnSingleSelection;
    public void SingleSelection(GameObject target)
    {
        if (OnSingleSelection != null)
            OnSingleSelection(target);
    }

    public event Action<Bounds> OnMultipleSelection;
    public void MultipleSelection(Bounds box)
    {
        if (OnMultipleSelection != null)
        {
            OnMultipleSelection(box);
        }
    }

    public event Action<RaycastHit> OnRequestAction;
    public void RequestAction(RaycastHit hit)
    {
        if (OnRequestAction != null)
        {
            OnRequestAction(hit);
        }
    }

    public event Action<Resource> onGatheredResource;
    public void GatheredResource(Resource resource)
    {
        if (onGatheredResource != null)
        {
            onGatheredResource(resource);
        }
    }
}
