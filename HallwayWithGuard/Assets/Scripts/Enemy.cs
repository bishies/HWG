using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    //public Camera nmeCam;
    //public float lineofSight = 50f;
    public float radius = 10f;
    public float glowRadius = 15f;
    public float speed;
    public float startWaitTime;

    private float waitTime;
    private int randomLocation;
    private bool distracted = true;
    private bool patrol = true;

    public GameObject target;
    public GameObject glowStick;
    NavMeshAgent agent;
    public Transform[] Location;




    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
        randomLocation = Random.Range(0, Location.Length);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distracted)
        {
            float distanceToGlow = Vector3.Distance(glowStick.transform.position, transform.position);
        
            if (distanceToGlow <= glowRadius && distracted)
            {
                agent.SetDestination(glowStick.transform.position);
                if (distance <= agent.stoppingDistance)
                {
                     faceTarget(glowStick);
                }
            }
            else if (distance <= radius)
            {
                Debug.Log("chasing player...");
                //agent.destination = target.transform.position;
                agent.SetDestination(target.transform.position);
                if (distance <= agent.stoppingDistance)
                {
                    faceTarget(target);
                }
            }
            else
            {
                Debug.Log("patrolling...2");
                patrolling();
            }
        }
        else if (distance <= radius)
        {
            Debug.Log("chasing player...");
            //agent.destination = target.transform.position;
            agent.SetDestination(target.transform.position);
            if (distance <= agent.stoppingDistance)
            {
                faceTarget(target);
            }
        }
        else
        {
            Debug.Log("patrolling...");
            patrolling();
        }
    }

    void patrolling()
    {
        agent.SetDestination(Location[randomLocation].transform.position);

        if (patrol)
        {
            patrol = false;
            Invoke("changeLoc", 5f);
        }
        
        /*transform.position = Vector3.MoveTowards(transform.position, Location[randomLocation].position, speed * Time.deltaTime);
        */
        if (Vector3.Distance(transform.position, Location[randomLocation].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomLocation = Random.Range(0, Location.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void changeLoc()
    {
        //Debug.Log("RUNNING");
        //int originalposition = randomLocation;
        randomLocation = Random.Range(0, Location.Length);
        patrol = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    void faceTarget(GameObject target2)
    {
        Vector3 direction = (target2.transform.position - transform.position).normalized;
        Quaternion lookRotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotate, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "glowstick")
        {
            Invoke("distraction", 2f);
            Destroy(other.gameObject, 2f);
        }
    }

    void distraction()
    {
        Debug.Log("DISTRACTION");
        distracted = false;
    }

    /*
    void checkRaycast()
    {
        if (Physics.Raycast(nmeCam.transform.position, nmeCam.transform.forward, out hit, lineofSight))
        {
            Debug.Log(hit.transform.tag);
        }
    }
    */
}
