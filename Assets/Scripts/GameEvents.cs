using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    //public event Action selectedObjectRightClick;
    //public void onSelectedObjectRightClick()
    //{
    //    if selectedObjectRightClick
    //}

    public event Action<GameObject> onEntitySelectionTigger;
    public void entitySelectionTigger(GameObject gameObjectInstance)
    {
        if (onEntitySelectionTigger != null)
        {
            onEntitySelectionTigger(gameObjectInstance);
        }
    }

    public event Action<Bounds> onUnitMultiSelectTrigger;
    public void unitMultiSelectTrigger(Bounds selectionBoxBounds)
    {
        if (onUnitMultiSelectTrigger != null)
        {
            onUnitMultiSelectTrigger(selectionBoxBounds);
        }
    }

    public event Action<GameObject> onSelectedTrigger;
    public void selectedTrigger(GameObject gameObjectInstance)
    {
        if (onSelectedTrigger != null)
        {
            onSelectedTrigger(gameObjectInstance);
        }
    }

    public event Action<List<Entity>, GameObject, Vector3> onApplyMainObjectMethodTrigger;
    public void applyMainObjectMethod(List<Entity> selectedEntetiesList, GameObject targetObject, Vector3 point)
    {
        if (onApplyMainObjectMethodTrigger  != null)
        {
            onApplyMainObjectMethodTrigger(selectedEntetiesList, targetObject, point);
        }
    }


}
