using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private GameObject raycastCollidedObject;

    public GameObject selectionBox;
    public GameObject selectionBoxCollider;
    public Image selectionBoxImage;
    public RectTransform selectionBoxTransform;
    public Vector3 startScreenPos;
    public Canvas selectionBoxCanvas;
    public Bounds selectionBoxBounds = new Bounds();

    readonly List<int> buttonIds = new List<int>(new int[] {
        Constants.mouseLeftButtonId,
        Constants.mouseRightButtonId,
        Constants.mouseMiddleButtonId
    });

    Vector3 currentMousePosition;

    private common.MouseButton[] buttons = new common.MouseButton[] { null, null, null };
    private common.MouseButton leftButton;
    private common.MouseButton rightButton;
    private common.MouseButton middleButton;

    void Start()
    {
        selectionBox = transform.Find(Constants.selectionBoxGameObjectName).gameObject;
        selectionBoxCanvas = selectionBox.GetComponent<Canvas>();

        selectionBoxImage = selectionBox.transform.Find("selectionBoxImage").GetComponent<Image>();
        selectionBoxTransform = selectionBoxImage.GetComponent<RectTransform>();

        selectionBoxCollider = transform.Find(Constants.selectionBoxColliderGameObjectName).gameObject;

        InitializeMouseButtons();
        InitializeSelectionBox();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        HandleInput();
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
            buttons[id] = new common.MouseButton(id);

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

        selectionBoxBounds.center = selectionBoxTransform.transform.position;
        selectionBoxBounds.size = new Vector3(width, height, 0);

        //selectionBoxTransform.sizeDelta = new Vector2(width, height);
        //selectionBoxTransform.anchoredPosition = leftButton.lastClickPosition + new Vector3(width / 2, height / 2, 0);
        selectionBoxTransform.position = Vector3.Lerp(leftButton.lastClickPosition, currentMousePosition, 0.5f);
        selectionBoxTransform.sizeDelta = selectionBoxCanvas.transform.InverseTransformVector(selectionBoxBounds.size);
    }

    private void ApplySelectionBox()
    {
        selectionBox.SetActive(false);
        onApplyMultiSelection();
    }

    //private void UpdateSelectionBoxCollider()
    //{
    //    float scaleX;
    //    float scaleY;
    //    float scaleZ;
    //
    //    selectionBoxCollider.transform.position = Camera.main.transform.position;
    //    selectionBoxCollider.transform.rotation = Camera.main.transform.rotation;
    //
    //    Game.Manager.DebugConsole.Log(selectionBoxTransform.sizeDelta, "sizeDelta");
    //    Game.Manager.DebugConsole.Log(Screen.width, "Screen.width");
    //    scaleX = selectionBoxTransform.sizeDelta.x;
    //    scaleY = selectionBoxTransform.sizeDelta.y;
    //    scaleZ = 10;
    //    selectionBoxCollider.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    //}

    private Vector3 CalculateSelectionBoxSize()
    {
        Vector3 size;
        size.x = Mathf.Abs(leftButton.lastClickPosition.x - currentMousePosition.x);
        size.y = Mathf.Abs(leftButton.lastClickPosition.y - currentMousePosition.y);
        size.z = 0;

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

    private void onApplyMultiSelection()
    {
        GameEvents.current.multiSelection(selectionBoxBounds);
    }
}

