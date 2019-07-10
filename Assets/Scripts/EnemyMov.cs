using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    public Transform[] Waypoints;
    public Transform desiredWaypoint;
    private NavMeshAgent ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        AImovement();
    }

    public void AImovement()
    {
        if (desiredWaypoint == null)
        {
            desiredWaypoint = Waypoints[Random.Range(0, Waypoints.Length - 1)];
        }
        if (desiredWaypoint.position.x == transform.position.x && desiredWaypoint.position.z == transform.position.z)
        {
            desiredWaypoint = null;
        }
        if(desiredWaypoint != null)
        {
            ai.SetDestination(desiredWaypoint.position);
        }

    }
}
