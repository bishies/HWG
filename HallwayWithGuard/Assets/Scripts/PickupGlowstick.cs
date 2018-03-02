using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGlowstick : MonoBehaviour {

    public GameObject glowstick;
    public GameObject glowstickPrefab;
    public float force;
    private int hasGS;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        hasGS = 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasGS > 0 && glowstick == null)
        {
            //glowstick.transform.position = transform.position;
            Debug.Log("CAN INSTANTIATE ANOTHER");
            if (Input.GetMouseButtonDown(0))
            {
                hasGS--;
                glowstick = Instantiate(glowstickPrefab, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
                Debug.Log("INSTANTIATED");
                Rigidbody rb = glowstick.GetComponent<Rigidbody>();
                //rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
                rb.velocity = transform.position + transform.localEulerAngles * .01f;
                //rb.AddTorque(0f, 0f, 20f, ForceMode.Impulse);
            }
        }
    }
    void enableCollider()
    {
        glowstick.GetComponent<CapsuleCollider>().enabled = true;
    }
}
