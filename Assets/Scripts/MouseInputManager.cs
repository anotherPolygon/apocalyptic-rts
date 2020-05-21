using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // to identify if GUI was pressed


public class MouseInputManager : MonoBehaviour
{
    private GameObject rayCastHitObject;
    public GameObject recievedSelectedObject;

    public List<Entity> selectedEntities = new List<Entity>();
    private Entity[] entites;

    public Image selectionBox;
    private RectTransform rt;
    private Vector3 startScreenPos;
    public Canvas canvas;
    private bool isSelecting;


    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onSelectedTrigger += onSelected;
        recievedSelectedObject = null;

        if (selectionBox != null)
        {
            //We need to reset anchors and pivot to ensure proper positioning
            rt = selectionBox.GetComponent<RectTransform>();
            rt.pivot = Vector2.one * .5f;
            rt.anchorMin = Vector2.one * .5f;
            rt.anchorMax = Vector2.one * .5f;
            selectionBox.gameObject.SetActive(false);

        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
            RightClick();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClick();
        
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionBox == null)
                return;
            //Storing these variables for the selectionBox
            startScreenPos = Input.mousePosition;
            isSelecting = true;

            // Reseting the list
            selectedEntities = new List<Entity>();
        }
        //If we never set the selectionBox variable in the inspector, we are simply not able to drag the selectionBox to easily select multiple objects. 'Regular' selection should still work
        if (selectionBox == null)
            return;

        //We finished our selection box when the key is released
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;

            // Storing a list of selected entities
            entites = UnityEngine.Object.FindObjectsOfType<Entity>();
            foreach(Entity entity in entites)
            {
                if (entity.isSelected)
                    selectedEntities.Add(entity);
            }

        }
        selectionBox.gameObject.SetActive(isSelecting);
        if (isSelecting)
        {
            Bounds b = new Bounds();
            //The center of the bounds is inbetween startpos and current pos
            b.center = Vector3.Lerp(startScreenPos, Input.mousePosition, 0.5f);
            //We make the size absolute (negative bounds don't contain anything)
            b.size = new Vector3(Mathf.Abs(startScreenPos.x - Input.mousePosition.x),
                Mathf.Abs(startScreenPos.y - Input.mousePosition.y),
                0);

            //To display our selectionbox image in the same place as our bounds
            rt.position = b.center;
            rt.sizeDelta = canvas.transform.InverseTransformVector(b.size);

            // Trigering the multi selcetion event
            onUnitMultiSelectTrigger(b);

        }
        }



    private void RightClick()
    {
        // Gettinh the ray cast collision info
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Identify if ray hit GUI
            if (EventSystem.current.IsPointerOverGameObject(-1) == false)
            {
                rayCastHitObject = hit.collider.gameObject;
            }
            onApplyMainObjectMethodTrigger(selectedEntities, rayCastHitObject, hit.point);
        }
                
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
                rayCastHitObject = hit.collider.gameObject;
                onEntitySelectionTrigger(rayCastHitObject);
                if(recievedSelectedObject != null)
                    selectedEntities.Add(recievedSelectedObject.GetComponent<Entity>());
                Debug.Log(rayCastHitObject);
            }
            else
            {
                Debug.Log("GUI - Ray case hit GUI");
            }

        }
        else
        {
            rayCastHitObject = null;
        }
    }
    private void onEntitySelectionTrigger(GameObject gameObjectInstance)
    {
        GameEvents.current.entitySelectionTigger(gameObjectInstance);
    }
    private void onUnitMultiSelectTrigger(Bounds selectionBoxBounds)
    {
        GameEvents.current.unitMultiSelectTrigger(selectionBoxBounds);
    }

    private void onApplyMainObjectMethodTrigger(List<Entity> selectedEntetiesList, GameObject targetObject, Vector3 point)
    {
        GameEvents.current.applyMainObjectMethod(selectedEntetiesList, targetObject, point);
    }


    private void onSelected(GameObject SelectedObject)
    {
        recievedSelectedObject = SelectedObject;

    }

}

