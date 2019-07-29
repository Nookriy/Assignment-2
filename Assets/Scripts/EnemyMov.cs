using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum states { idle, patrol, chase, attack}
public class EnemyMov : MonoBehaviour
{
    public Transform[] Waypoints;
    public Transform desiredWaypoint;
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
    public int Damage;
    public int HP;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float radiusWaypointDetect;
    private BattleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        state = states.patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        bm = GameObject.FindObjectOfType<BattleManager>();
                
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattlemode)
        {
            //ai.Stop();
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
        // If distance between desiredwaypoint and enem is smaller or equal to X units, then 
        if (Vector3.Distance(desiredWaypoint.position, transform.position) <= radiusWaypointDetect) 

        {
            idleWaypoint = true;
            desiredWaypoint = null;
        }
        if(desiredWaypoint != null)
        {
           transform.position = Vector3.MoveTowards(transform.position, desiredWaypoint.position, speed * Time.deltaTime);
        }



    }

    void SwitchStatement()
    {
        
        switch (state)
        {
            case states.idle:
                Debug.Log("Idling");
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    idleWaypoint = false;
                    timer = idleTime;
                    state = states.patrol;
                }
                
                break;
            case states.patrol:
                Debug.Log("Patrolling");
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
                Debug.Log("Chasing");
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
                bm.initializeBattlemode(gameObject.GetComponent<EnemyMov>());
                break;
            default:
                state = states.patrol;
                break;
        }
    }



    void ChaseFunct()
    {
        // ai.SetDestination(player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Transform wayppoint in Waypoints)
        {
            Gizmos.DrawSphere(wayppoint.position, radiusWaypointDetect);
        }
    }
}

