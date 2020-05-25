using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<GameObject> entitySelectionTrigger;
    public void entitySelection(GameObject gameObjectInstance)
    {
        if (entitySelectionTrigger != null)
        {
            entitySelectionTrigger(gameObjectInstance);
        }
    }

    public event Action<Bounds> multiSelectionTrigger;
    public void multiSelection(Bounds selectionBoxBounds)
    {
        if (multiSelectionTrigger != null)
        {
            multiSelectionTrigger(selectionBoxBounds);
        }
    }

    public event Action<GameObject> reportSelectedTrigger;
    public void reportSelected(GameObject gameObjectInstance)
    {
        if (reportSelectedTrigger != null)
        {
            reportSelectedTrigger(gameObjectInstance);
        }
    }

    public event Action<List<GameObject>, GameObject, Vector3> executeMainMethodTrigger;
    public void executeMainMethod(List<GameObject> selectedEntetiesList, GameObject targetObject, Vector3 point)
    {
        if (executeMainMethodTrigger  != null)
        {
            executeMainMethodTrigger(selectedEntetiesList, targetObject, point);
        }
    }

    // Building Assingment
    //  Unit side - Ask
    public event Action<GameObject, GameObject> askAssignToBuildingTrigger;
    public void askAssignToBuilding(GameObject bulidingGameObjec, GameObject worker)
    {
        if (askAssignToBuildingTrigger != null)
        {
            askAssignToBuildingTrigger(bulidingGameObjec, worker);
        }
    }

    //Unit side - Start
    public event Action<GameObject, GameObject> assignmentStartTrigger;
    public void assignmentStart(GameObject bulidingGameObjec, GameObject worker)
    {
        if (assignmentStartTrigger != null)
        {
            assignmentStartTrigger(bulidingGameObjec, worker);
        }
    }

    //Unit side - End
    public event Action<GameObject, GameObject> assignmentEndTrigger;
    public void assignmentEnd(GameObject bulidingGameObjec, GameObject worker)
    {
        if (assignmentEndTrigger != null)
        {
            assignmentEndTrigger(bulidingGameObjec, worker);
        }
    }
    
    //Building side - Confirmed
    public event Action<GameObject, GameObject> buildingAssignmentConfirmedTrigger;
    public void buildingAssignmentConfirmed(GameObject worker, GameObject bulidingGameObjec)
    {
        if (buildingAssignmentConfirmedTrigger != null)
        {
            buildingAssignmentConfirmedTrigger(bulidingGameObjec, worker);
        }
    }

    //Building side - Denied
    public event Action<GameObject, GameObject> buildingAssignmentDeniedTrigger;
    public void buildingAssignmentDenied(GameObject worker, GameObject bulidingGameObjec)
    {
        if (buildingAssignmentDeniedTrigger != null)
        {
            buildingAssignmentDeniedTrigger(bulidingGameObjec, worker);
        }
    }


}
