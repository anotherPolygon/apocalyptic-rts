using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // to identify if GUI was pressed


public class MouseInputManager : MonoBehaviour
{
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClick();
        else if (Input.GetMouseButtonDown(1))
            RightClick();
    }

    private void RightClick()
    {
       //
    }
    private void LeftClick()
    {
        
        // Gettinh the ray cast collision info
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Identify if ray hit GUI
            if (EventSystem.current.IsPointerOverGameObject(-1) == false)
            {
                selectedObject = hit.collider.gameObject;
                onEntitySelectionTrigger(selectedObject);
                Debug.Log(selectedObject);
            }
            else
            {
                Debug.Log("GUI - Ray case hit GUI");
            }

        }
        else
        {
            selectedObject = null;
        }
    }
    private void onEntitySelectionTrigger(GameObject gameObjectInstance)
    {
        GameEvents.current.entitySelectionTigger(gameObjectInstance);
    }
    
}

