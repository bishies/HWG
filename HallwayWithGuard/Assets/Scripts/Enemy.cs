using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    //public Camera nmeCam;
    //public float lineofSight = 50f;
    public float radius = 10f;
    public GameObject target;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= radius)
        {
            //agent.destination = target.transform.position;
            agent.SetDestination(target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                faceTarget();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    void faceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotate, Time.deltaTime * 5f);
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
