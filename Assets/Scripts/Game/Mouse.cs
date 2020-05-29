using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private GameObject raycastCollidedObject;

    public GameObject selectionBox;
    public Image selectionBoxImage;
    public RectTransform selectionBoxTransform;
    public Vector3 startScreenPos;
    public Canvas selectionBoxCanvas;
    private bool isSelecting;

    List<int> buttonIds = new List<int>(new int[] {
        Constants.mouseLeftButtonId,
        Constants.mouseRightButtonId,
        Constants.mouseMiddleButtonId
    });

    Vector3 currentMousePosition;

    //private bool[] isClicked = new bool[] { false, false, false };
    //private bool[] hasClickJustStarted = new bool[] { false, false, false };
    //private bool[] hasClickJustEnded = new bool[] { false, false, false };
    //private bool[] isHeld = new bool[] { false, false, false };
    //private float[] clickDuration = new float[] { 0f, 0f, 0f };
    //private Vector3[] clickPosition = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero };

    private common.MouseButton[] buttons = new common.MouseButton[] { null, null, null };
    private common.MouseButton leftButton;
    private common.MouseButton rightButton;
    private common.MouseButton middleButton;

    //private bool isLeftClicked;
    //private bool isRightClicked;
    //private bool isMiddleClicked;
    //
    //private float lastLeftClick;
    //private float lastRightClick;
    //private float lastMiddleClick;
    //
    //private bool isLeftHold;
    //private bool isRightHold;
    //private bool isMiddleHold;

    // Start is called before the first frame update
    void Start()
    {
        selectionBox = transform.Find(Constants.selectionBoxGameObjectName).gameObject;
        selectionBoxCanvas = selectionBox.GetComponent<Canvas>();


        selectionBoxImage = selectionBox.transform.Find("selectionBoxImage").GetComponent<Image>();
        selectionBoxTransform = selectionBoxImage.GetComponent<RectTransform>();

        InitializeMouseButtons();
        InitializeSelectionBox();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Game.Manager.DebugConsole.Log(currentMousePosition, "current Mouse");
        GetInput();
        HandleInput();
    }

    private void GetInput()
    {
        bool _isClicked;
        common.MouseButton button;

        currentMousePosition = Input.mousePosition;
        
        foreach (int _id in buttonIds) {

            button = buttons[_id];
            _isClicked = Input.GetMouseButton(_id);

            button.Update(_isClicked);
        }
    }

    private void HandleInput()
    {
        HandleLeftClick();
        HandleRightClick();
        HandleMiddleClick();
    }

    private void HandleLeftClick()
    {
        if (leftButton.hasClickJustStarted)
            LeftClick();

        if (leftButton.isHeld)
            UpdateSelectionBox();

        if (leftButton.hasClickJustEnded)
            ApplySelectionBox();
    }

    private void InitializeMouseButtons()
    {
        foreach (int id in buttonIds)
            buttons[id] = new common.MouseButton();

        leftButton = buttons[Constants.mouseLeftButtonId];
        rightButton = buttons[Constants.mouseRightButtonId];
        middleButton = buttons[Constants.mouseMiddleButtonId];
    }

    private void InitializeSelectionBox()
    {
        selectionBoxTransform.pivot = Vector2.one * Constants.selectionBoxPivot;
        selectionBoxTransform.anchorMin = Vector2.one * Constants.selectionBoxMinimumAnchor;
        selectionBoxTransform.anchorMin = Vector2.one * Constants.selectionBoxMaximumAnchor;
        selectionBox.SetActive(false);
    }

    private void LeftClick()
    {

    }

    private void UpdateSelectionBox()
    {
        float width;
        float height;

        selectionBox.SetActive(true);
        width = Mathf.Abs(currentMousePosition.x - leftButton.lastClickPosition.x);
        height = Mathf.Abs(currentMousePosition.y - leftButton.lastClickPosition.y);

        selectionBoxTransform.sizeDelta = new Vector2(width, height);
        selectionBoxTransform.anchoredPosition = leftButton.lastClickPosition + new Vector3(width / 2, height / 2, 0);
        selectionBoxTransform.position = Vector3.Lerp(leftButton.lastClickPosition, currentMousePosition, 0.5f);
        
    }

    private void ApplySelectionBox()
    {
        selectionBox.SetActive(false);
        // apply selection box on all colliding objects
    }

    private Vector3 CalculateSelectionBoxSize()
    {
        Vector3 size;
        //size = (leftButton.lastClickPosition - CurrentMousePosition());
        size.x = Mathf.Abs(leftButton.lastClickPosition.x - currentMousePosition.x);
        size.y = Mathf.Abs(leftButton.lastClickPosition.y - currentMousePosition.y);
        size.z = 0;// Mathf.Abs(leftButton.lastClickPosition.z - Input.mousePosition.z);
        //size.y = Mathf.Abs(size.y);
        //size.z = Mathf.Abs(size.z);

        return size;
    }

    private void HandleRightClick()
    {

    }

    private void HandleMiddleClick()
    {

    }

    private common.MouseButton GetButton(int id)
    {
        return buttons[id];
    }

    private Vector3 CurrentMousePosition()
    {
        return Input.mousePosition;
    }
}
