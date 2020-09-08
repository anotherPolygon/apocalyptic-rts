using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshArrivalReporter : MonoBehaviour
{
    public Dictionary<NavMeshAgent, AgentDesitnationDisatnce> agent2destination = new Dictionary<NavMeshAgent, AgentDesitnationDisatnce>();
    public List<NavMeshAgent> agentKeys = new List<NavMeshAgent>();
    private NavMeshAgent currentAgent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //IterateAgents();
        for (int i = 0; i < agentKeys.Count; i++)
        {
            currentAgent = agentKeys[i];
            if (agent2destination[currentAgent].CheckNavMeshArrived(currentAgent) || agent2destination[currentAgent].IsInDistance(currentAgent.transform.position))
            {
                RemoveAgent(currentAgent);
                Events.current.arivedToDestination(currentAgent);
            }
        }
    }

    void IterateAgents()
    {
        
    }
    public void RegisterAgent(NavMeshAgent agent, Vector3 destination, float distance)
    {
        AgentDesitnationDisatnce agentDD = new AgentDesitnationDisatnce(destination, distance);
        RemoveAgent(agent);
        agent2destination.Add(agent, agentDD);
        agentKeys.Add(agent);
        Debug.Log(agent + " Added");
        Debug.Log(agentKeys);

    }

    public void RemoveAgent(NavMeshAgent agent)
    {
        AgentDesitnationDisatnce agentDD;
        if (agent2destination.TryGetValue(agent, out agentDD))
        {
            agent2destination.Remove(agent);
            agentKeys.Remove(agent);
            Debug.Log(agent + " Removed");
            Debug.Log(agentKeys);
        }
    }


    public class AgentDesitnationDisatnce
    {
        public Vector3 Destination { get; set; }
        public float Distance { get; set; }

        public AgentDesitnationDisatnce(Vector3 destination, float distance)
        {
            Destination = destination;
            Distance = distance;
        }

        public bool IsInDistance(Vector3 currentPosition)
        {
            return Vector3.Distance(currentPosition, this.Destination) <= this.Distance;
        }

        public bool CheckNavMeshArrived(NavMeshAgent mNavMeshAgent)
        {
            if (!mNavMeshAgent.pathPending)
            {
                if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
                {
                    if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
