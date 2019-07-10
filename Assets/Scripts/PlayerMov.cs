using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject[] gameObjects;
    public float force;
    private NavMeshAgent agent;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        camera = GameObject.FindObjectOfType<Camera>();

        //Debug.Log(intfunction(gameObjects[0], gameObjects[3]));
        //Debug.Log(intfunction(gameObjects[1], gameObjects[2]));
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.right * -force);
        }
    }

    void MouseMovement()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    //private void disfunct()
    //{
       
    //}

    //private float intfunction(GameObject x, GameObject y)
    //{
    //    float distance;
    //    distance = Vector3.Distance(x.transform.position, y.transform.position);
    //    return distance;
    //}
}
