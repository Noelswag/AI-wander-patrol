using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    NavMeshAgent agent;


    public Transform[] Waypoints;
    private int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.transform.position += new Vector3(Random.Range(30,-30), 0 , Random.Range(30, -30) );
        HeadToClosestWaypoint();

    }


    void HeadToClosestWaypoint()
    {
        float smallestDistance = Vector3.Distance(agent.transform.position, Waypoints[0].position);


        for (int i = 0; i < Waypoints.Length; i++)
        {
            float distance = Vector3.Distance(agent.transform.position, Waypoints[i].position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                index = i;
            }
        }



        Seek(Waypoints[index].position);
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Move to the next waypoint
            index++;
            if (index >= Waypoints.Length)
            {
                index = 0; // Loop back to the first waypoint
            }
            Seek(Waypoints[index].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
}
