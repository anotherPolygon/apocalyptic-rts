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

}
