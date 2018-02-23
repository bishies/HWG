using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    public Camera nmeCam;
    public float lineofSight = 50f;
    public float radius = 10f;

    Transform target;
    NavMeshAgent agent;

    private RaycastHit hit;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= radius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                faceTarget();
            }
        }
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotate, Time.deltaTime * 5f);
    }

    void checkRaycast()
    {
        if (Physics.Raycast(nmeCam.transform.position, nmeCam.transform.forward, out hit, lineofSight))
        {
            Debug.Log(hit.transform.tag);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
