using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitsActions : MonoBehaviour
{
    private Game Game;
    private dynamic identityScript;

    // Start is called before the first frame update
    void Start()
    {
        Game = GameObject.Find("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void MoveToPostion(NavMeshAgent agent, Vector3 position, float positionSpace = 0f)
    {
        if(agent != null)
            agent.destination = position + new Vector3(positionSpace, 0, positionSpace);
    }


    public void askAssignToBuilding(GameObject bulidingGameObjec, GameObject worker)
    {
        GameEvents.current.askAssignToBuilding(bulidingGameObjec, worker);

    }

}
