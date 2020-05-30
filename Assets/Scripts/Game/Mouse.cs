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

    void FixedUpdate()
    {
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

    private void HandleLeftClick()
    {
        if (leftButton.hasClickJustStarted)
            LeftClick();

        if (leftButton.isHeld)
            UpdateSelectionBox();

        if (leftButton.hasClickJustEnded)
            ApplySelectionBox();
    }

    private void LeftClick()
    {
        RaycastHit hit;

        hit = SendRaycase();
        if (hit.collider != null)
            Events.current.SingleSelection(hit.collider.gameObject);
        else
            Game.Manager.State.DeselectAll();
    }

    private RaycastHit SendRaycase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }

    private void SelectGameObject(GameObject target)
    {
        Events.current.SingleSelection(target);
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

        selectionBoxTransform.position = Vector3.Lerp(leftButton.lastClickPosition, currentMousePosition, 0.5f);
        selectionBoxTransform.sizeDelta = selectionBoxCanvas.transform.InverseTransformVector(selectionBoxBounds.size);
    }

    private void ApplySelectionBox()
    {
        selectionBox.SetActive(false);
        onApplyMultiSelection();
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

