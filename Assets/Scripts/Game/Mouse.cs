using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Mouse : MonoBehaviour
{
    private GameObject raycastCollidedObject;

    public common.objects.UnityObjects selectionBox;

    public Bounds selectionBoxBounds = new Bounds();

    readonly List<int> buttonIds = new List<int>(new int[] {
        Constants.mouseLeftButtonId,
        Constants.mouseRightButtonId,
        Constants.mouseMiddleButtonId
    });

    Vector3 currentMousePosition;

    private common.objects.MouseButton[] buttons = new common.objects.MouseButton[] {
        new common.objects.MouseButton(Constants.mouseLeftButtonId),
        new common.objects.MouseButton(Constants.mouseRightButtonId),
        new common.objects.MouseButton(Constants.mouseMiddleButtonId),
    };

    private common.objects.MouseButton leftButton;
    private common.objects.MouseButton rightButton;
    private common.objects.MouseButton middleButton;

    bool isInitialized = false;

    void Start()
    {
        GameObject _selectionBoxGameObject;
        _selectionBoxGameObject = transform.Find(Constants.selectionBoxGameObjectName).gameObject;
        selectionBox = new common.objects.UnityObjects(_selectionBoxGameObject);
        
        InitializeSelectionBox();
        InitializeMouseButtons();

        isInitialized = true;
    }

    void FixedUpdate()
    {
        if (!isInitialized)
            return;

        GetInput();
        HandleInput();
    }

    private void GetInput()
    {
        bool _isClicked;
        common.objects.MouseButton button;

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
        leftButton = buttons[Constants.mouseLeftButtonId];
        rightButton = buttons[Constants.mouseRightButtonId];
        middleButton = buttons[Constants.mouseMiddleButtonId];
    }

    private void InitializeSelectionBox()
    {
        RectTransform _rectTransform;
        Debug.Log(common.Utils.GetUnityObjectChildsInfo(selectionBox));
        _rectTransform = selectionBox.childs[Constants.selectionBoxImageName].rectTransform;
        
        _rectTransform.pivot = Vector2.one * Constants.selectionBoxPivot;
        _rectTransform.anchorMin = Vector2.one * Constants.selectionBoxMinimumAnchor;
        _rectTransform.anchorMin = Vector2.one * Constants.selectionBoxMaximumAnchor;
        selectionBox.gameObject.SetActive(false);
    }

    private void HandleLeftClick()
    {
        if (leftButton.hasClickJustStarted)
            LeftClick();

        if (leftButton.isHeld)
            UpdateSelectionBox();

        if (leftButton.hasClickJustEnded & leftButton.wasHeld)
            ApplySelectionBox();
    }

    private void LeftClick()
    {
        RaycastHit hit;
        SendRaycast(out hit);

        if (hit.collider != null)
            if (hit.collider.gameObject.tag == Constants.terrainGameObjectTag)
                Game.Manager.State.DeselectAll();
            else
                Events.current.SingleSelection(hit.collider.gameObject);
    }

    private bool SendRaycast(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    private void SelectGameObject(GameObject target)
    {
        Events.current.SingleSelection(target);
    }

    private void UpdateSelectionBox()
    {
        RectTransform _rectTransform;
        float width;
        float height;

        _rectTransform = selectionBox.childs[Constants.selectionBoxImageName].rectTransform;

        selectionBox.gameObject.SetActive(true);
        width = Mathf.Abs(currentMousePosition.x - leftButton.lastClickPosition.x);
        height = Mathf.Abs(currentMousePosition.y - leftButton.lastClickPosition.y);

        selectionBoxBounds.center = _rectTransform.transform.position;
        selectionBoxBounds.size = new Vector3(width, height, 0);

        _rectTransform.position = Vector3.Lerp(leftButton.lastClickPosition, currentMousePosition, 0.5f);
        _rectTransform.sizeDelta = selectionBox.canvas.transform.InverseTransformVector(selectionBoxBounds.size);
    }

    private void ApplySelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        Events.current.MultipleSelection(selectionBoxBounds);
        InitializeSelectionBox();
    }

    private void HandleRightClick()
    {
        if (rightButton.hasClickJustStarted)
            RightClick();
    }

    private void RightClick()
    {
        RaycastHit _hit;
        SendRaycast(out _hit);

        if (_hit.collider != null)
            Events.current.RequestAction(_hit);
    }

    private void HandleMiddleClick()
    {

    }

    private common.objects.MouseButton GetButton(int id)
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

