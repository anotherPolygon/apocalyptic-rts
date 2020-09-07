using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class BuyBuilding : MonoBehaviour
{
    private Button buttonTry;

    public GameObject placeObjectPrefab; // added from the editor - the building to place

    private GameObject currentPlaceableObject; // refernce to "placeObjectPrefab"

    private float rotationDirection;
    private float PlaceableOjectRotationSpeed = 10f;

    private Bounds buildingBounds;
    private Material overlappingFeedbackMaterial;
    private Building buildingToBuy;

    public int price;
    public string resouceToCharge;
    // Start is called before the first frame update
    void Start()
    {
        buttonTry = GetComponent<Button>();
        buttonTry.onClick.AddListener(HandleNewBuilding);
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
    }

    private void AddEventListeners()
    {
        Events.current.OnLeftClick += PlaceBuilding;
        Events.current.OnRightClick += CancelBuyBuilding;
    }

    private void RemoveEventListeners()
    {
        Events.current.OnLeftClick -= PlaceBuilding;
        Events.current.OnRightClick -= CancelBuyBuilding;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            _provideFeedback();
        }
    }

    private void _provideFeedback()
    {
        //buildingBounds = currentPlaceableObject.GetComponent<Collider>().bounds;
        overlappingFeedbackMaterial = currentPlaceableObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].renderer.material;

        if (buildingToBuy.canBeConstructed)
            overlappingFeedbackMaterial.SetColor("_Color", new Color(0, 183, 43, 0.25f));
        else
        {
            overlappingFeedbackMaterial.SetColor("_Color", Color.red);
        }
    }

    private void CancelBuyBuilding()
    {
        RemoveEventListeners();
        Game.Manager.BuildingBoundsDict.Remove("Building-" + currentPlaceableObject.gameObject.GetInstanceID().ToString());
        Destroy(currentPlaceableObject);
    }

    // Relaese the building from mouse
    private void PlaceBuilding()
    {
        if(Game.Manager.RM.verifAndApplyCost(price, resouceToCharge))
        {
            RemoveEventListeners();
            currentPlaceableObject.layer = LayerMask.NameToLayer("Default");
            currentPlaceableObject.GetComponent<Building>().InitializeConstructionState();
            currentPlaceableObject = null;
        }
    }

    private void HandleNewBuilding()
    {
        if (currentPlaceableObject == null)
        {
            currentPlaceableObject = Instantiate(placeObjectPrefab);
            currentPlaceableObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            buildingToBuy = currentPlaceableObject.GetComponent<Building>();
            currentPlaceableObject.GetComponent<NavMeshObstacle>().enabled = false; // disabeling navmes obstacel
            AddEventListeners();

        }
        else
        {
            Destroy(currentPlaceableObject);
        }
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.transform.lossyScale.y, hitInfo.point.z);
            //currentPlaceableObject.transform.position = hitInfo.point; // deags the prefab to mouse
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal); // verify that rotation is upwards even if its on a slope
                                                                                                                // maybe in some objects I would want this rotation
        }
    }
    private void RotateFromMouseWheel()
    {
        if (Input.GetKey(KeyCode.Q))
            rotationDirection -= 5;
        if (Input.GetKey(KeyCode.E))
            rotationDirection += 5;
        if (Input.GetKeyDown(KeyCode.R))
            rotationDirection += 90;

        currentPlaceableObject.transform.Rotate(Vector3.up, rotationDirection);
        rotationDirection = 0;
    }

    // A way to check for collusion with bounds of otther buildings - in process
    bool _CheckIntersects(Bounds thisBounds, Bounds otherBounds)
    {
        bool overlapping = thisBounds.Intersects(otherBounds);
        return overlapping;

    }

    
}
