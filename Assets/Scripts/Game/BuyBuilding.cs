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
            foreach (var item in Game.Manager.BuildingBoundsDict)
            {
                if(_CheckIntersects(buildingBounds, item.Value))
                {
                    overlappingFeedbackMaterial.SetColor("_Color", Color.red);
                    intersectionsCounter += 1;
                }

            }

            if (intersectionsCounter==0)
            {
                overlappingFeedbackMaterial.SetColor("_Color", Color.green);
            }
                
        }
    }

    // Relaese the building from mouse
    private void PlaceBuilding()
    {
        Events.current.OnLeftClick -= PlaceBuilding;
        currentPlaceableObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("Hi");
        Debug.Log(_GetBuildinBounds());
        Debug.Log(Game.Manager.BuildingBoundsDict["Building-" + currentPlaceableObject.gameObject.GetInstanceID().ToString()]);
        Game.Manager.BuildingBoundsDict.Remove("Building-" + currentPlaceableObject.gameObject.GetInstanceID().ToString());
        Game.Manager.BuildingBoundsDict.Add("Building-" + currentPlaceableObject.gameObject.GetInstanceID().ToString(), _GetBuildinBounds());
        currentPlaceableObject = null;

        //Game.Manager.playerBuildingBounds.RemoveAt(Game.Manager.playerBuildingBounds.Count - 1);
        //Game.Manager.playerBuildingBounds.Add(buildingBounds);

    }

    private void HandleNewBuilding()
    {
        if (currentPlaceableObject == null)
        {
            currentPlaceableObject = Instantiate(placeObjectPrefab);
            currentPlaceableObject.layer = LayerMask.NameToLayer("Ignore Raycast"); 
            Events.current.OnLeftClick += PlaceBuilding;
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
        currentPlaceableObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // a patch i added -> maybe not needed
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.transform.lossyScale.y, hitInfo.point.z);
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
    bool _CheckIntersects(Bounds thisBounds, Bounds otherBounds)
    {
        bool overlapping = otherBounds.Intersects(thisBounds);
        return overlapping;
    }

    private Bounds _GetBuildinBounds()
    {
        buildingBounds = currentPlaceableObject.GetComponent<Collider>().bounds;
        common.objects.UnityObjects g;
        if (currentPlaceableObject.GetComponent<Building>().unityObjects.childs.TryGetValue("OverlapFeedback", out g))
            buildingBounds = currentPlaceableObject.GetComponent<Building>().unityObjects.childs["OverlapFeedback"].gameObject.GetComponent<Collider>().bounds;

        return buildingBounds;
    }
}
