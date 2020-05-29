using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private GameObject raycastCollidedObject;

    private GameObject selectionBox;
    private Image selectionBoxImage;
    private RectTransform selectionBoxTransform;
    private Vector3 startScreenPos;
    public Canvas selectionBoxCanvas;
    private bool isSelecting;

    private bool isLeftClicked;
    private bool isRightClicked;

    // Start is called before the first frame update
    void Start()
    {
        selectionBox = transform.Find(Constants.selectionBoxGameObjectName).gameObject;
        selectionBoxImage = selectionBox.GetComponent<Image>();
        selectionBoxTransform = selectionBox.GetComponent<RectTransform>();
        SetInitialSelectionBoxTransform();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleInput();
    }

    private void HandleInput()
    {
        if (isLeftClicked)
            LeftClick();
        else if (isRightClicked)
            RightClick();
    }

    private void GetInput()
    {
        isLeftClicked = Input.GetMouseButtonDown(Constants.mouseLeftClickId);
        isRightClicked = Input.GetMouseButtonDown(Constants.mouseRightClickId);
    }

    private void SetInitialSelectionBoxTransform()
    {

    }

    private void LeftClick()
    {
        Game.Manager.DebugConsole.Log("LeftClicked!", "Mouse");
    }

    private void RightClick()
    {
        Game.Manager.DebugConsole.Log("RightClicked!", "Mouse");
    }
}
