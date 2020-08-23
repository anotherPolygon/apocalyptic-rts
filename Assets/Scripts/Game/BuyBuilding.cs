using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBuilding : MonoBehaviour
{
    private Button buttonTry;

    [SerializeField]
    private GameObject placeObjectPrefab; // added from the editor - the building to place

    private GameObject currentPlaceableObject; // refernce to "placeObjectPrefab"

    private float mouseWheelRotation;
    private float PlaceableOjectRotationSpeed = 10f;

    private Bounds buildingBounds;
    private Material overlappingFeedbackMaterial;

    // Start is called before the first frame update
    void Start()
    {
        buttonTry = GetComponent<Button>();
        buttonTry.onClick.AddListener(HandleNewBuilding);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();

            buildingBounds = currentPlaceableObject.GetComponent<Collider>().bounds;
            overlappingFeedbackMaterial = currentPlaceableObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].renderer.material;

            int intersectionsCounter = 0;
            foreach (Bounds b in Game.Manager.playerBuildingBounds)
            {
                if(CheckIntersects(buildingBounds, b))
                {
                    overlappingFeedbackMaterial.SetColor("_Color", Color.red);
                    intersectionsCounter += 1;
                }

            if (intersectionsCounter==0)
            {
                overlappingFeedbackMaterial.SetColor("_Color", Color.green);
            }
                
            }
        }
    }

    // Relaese the building from mouse
    private void RelaeaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject.layer = LayerMask.NameToLayer("Default");
            currentPlaceableObject = null;
        }
    }

    private void HandleNewBuilding()
    {
        if (currentPlaceableObject == null)
        {
            currentPlaceableObject = Instantiate(placeObjectPrefab);
            currentPlaceableObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
            //localScale.y / 2f
            //currentPlaceableObject.transform.position = hitInfo.point; // deags the prefab to mouse
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal); // verify that rotation is upwards even if its on a slope
                                                                                                                // maybe in some objects I would want this rotation
        }
    }
    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * PlaceableOjectRotationSpeed);

    }

    // A way to check for collusion with bounds of otther buildings - in process
    bool CheckIntersects(Bounds thisBounds, Bounds otherBounds)
    {
        bool overlapping = otherBounds.Intersects(thisBounds);
        return overlapping;
    }
}
