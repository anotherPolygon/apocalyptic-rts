using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Animals
{
    float distance;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        base.ProgressDistance = 5f;
        base.TripTime = 10f;
        food = 44;
        RecursionTransformChange();
        //LookForFood();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
    /// <summary>
    /// call this function every TripTime and proceed steps by ProgressDistance & Random way
    /// </summary>
    private void RecursionTransformChange()
    {
        if(food>50)
        {
            base.navMeshAgent.SetDestination(RandomNewPlaceByProgressDistance());
            Invoke("RecursionTransformChange", base.TripTime);
        }
        else
        {
            LookForFood();
        }

    }
    /// <summary>
    /// Make a Random transform position 
    /// use ProgressDistance for Range Param
    /// use Random for cohice Direction
    /// </summary>
    /// <returns>the new random place</returns>
    private Vector3 RandomNewPlaceByProgressDistance()
    {

        return new Vector3(transform.position.x + Random.Range(-ProgressDistance, ProgressDistance), transform.position.y + Random.Range(-ProgressDistance, ProgressDistance), transform.position.z + Random.Range(-ProgressDistance, ProgressDistance));
    }
    /// <summary>
    /// 
    /// </summary>
    private void LookForFood()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("cola"));
        if (GameObject.FindGameObjectWithTag("FloatingBread"))
        {
            Debug.Log("exist");
            navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("FloatingBread").transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, GameObject.FindGameObjectWithTag("FloatingBread").transform.rotation, Time.deltaTime * 1000f);
        }
        else
        {
            base.navMeshAgent.SetDestination(RandomNewPlaceByProgressDistance());
            Invoke("RecursionTransformChange", base.TripTime);
        }
        //use a switch 
        //base.navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("FloatingBread").transform.position + new Vector3(Random.Range(-2,2), Random.Range(-2, 2), Random.Range(-2, 2)));
    }
    /// <summary>
    /// call this function when Reaching the destination
    /// </summary>
    private void Eat()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.tag == "FloatingBread")
        {
            
            //transform.LookAt(other.transform);
            Debug.Log("Eating");
            //play animation eating
            Destroy(other.gameObject);
            hp = 100;
            food = 100;
            RecursionTransformChange();//back to normal
        }
    }
}
