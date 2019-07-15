using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum states { idle, patrol, chase, attack}
public class EnemyMov : MonoBehaviour
{
    public Transform[] Waypoints;
    public Transform desiredWaypoint;
    private NavMeshAgent ai;
    public states state;
    private bool idleWaypoint;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float idleTime;
    private GameObject player;
    [SerializeField]
    private float ChaseRange;
    [SerializeField]
    private float AttackRange;
    public bool isBattlemode;
    [SerializeField]
    private float Damage;
    [SerializeField]
    private float HP;

    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
        state = states.patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattlemode)
        {
            ai.Stop();
            return;
        }
        SwitchStatement();
    }

    public void AImovement()
    {
        if (desiredWaypoint == null)
        {
            desiredWaypoint = Waypoints[Random.Range(0, Waypoints.Length - 1)];
        }
        if (desiredWaypoint.position.x == transform.position.x && desiredWaypoint.position.z == transform.position.z)
        {
            idleWaypoint = true;
            desiredWaypoint = null;
        }
        if(desiredWaypoint != null)
        {
            ai.SetDestination(desiredWaypoint.position);
        }

    }

    void SwitchStatement()
    {
        
        switch (state)
        {
            case states.idle:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    idleWaypoint = false;
                    timer = idleTime;
                    state = states.patrol;
                }
                
                break;
            case states.patrol:
                AImovement();
                if (idleWaypoint)
                {
                    state = states.idle;
                }
                if (Vector3.Distance(transform.position, player.transform.position) <= ChaseRange)
                {
                    state = states.chase;
                }
                break;
            case states.chase:
                ChaseFunct();
                if (Vector3.Distance(transform.position, player.transform.position) >= ChaseRange)
                {
                    state = states.patrol;

                }
                if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange)
                {
                    state = states.attack;
                }

                break;
            case states.attack:
                //change to battlemode
                break;
            default:
                state = states.patrol;
                break;
        }
    }



    void ChaseFunct()
    {
        ai.SetDestination(player.transform.position);
    }
}
