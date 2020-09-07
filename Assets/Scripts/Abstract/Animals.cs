using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animals : MonoBehaviour
{
    protected float hp;
    protected int healthy;//the status off the animal 0 die,1 sick,2 injured, 3 normal
    protected float speed;
    protected float TripTime;
    protected float ProgressDistance;
    [SerializeField] protected float food;//100 satiated, 50 hunger, 10 starvation
    protected Fish[] FishThatCanBeEaten;
    protected string[] FloatingFood;
    protected NavMeshAgent navMeshAgent;
    protected void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
