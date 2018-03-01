using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    CharacterController charCtrl;

    public enum RotationAxis
	{
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxis axes = RotationAxis.MouseX;

	public float minV = -45.0f;
	public float maxV = 45.0f;

	public float Horizontal = 10.0f;
	public float Vertical = 10.0f;

	public float rotationX = 0;

    public GameObject glowstickPrefab;
    public float force;
    private bool hasGS;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        hasGS = true;
    }
    

    void Update () {
        if (Input.GetMouseButtonDown(0) && hasGS)
        {
            hasGS = false;
            GameObject glowstick = Instantiate(glowstickPrefab, transform.position, transform.rotation);
            Rigidbody rb = glowstick.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
            rb.AddForce(transform.up * 5f, ForceMode.VelocityChange);
            rb.AddTorque(0f, 0f, 20f, ForceMode.Impulse);
        }

		if (axes == RotationAxis.MouseX) {
			transform.Rotate (0, Input.GetAxis ("Mouse X") * Horizontal, 0);
		} else if (axes == RotationAxis.MouseY) {
			rotationX -= Input.GetAxis ("Mouse Y") * Vertical;
			rotationX = Mathf.Clamp (rotationX, minV, maxV);

			float rotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3 (rotationX, rotationY, 0);
		}

        RaycastHit hit;

        Vector3 p1 = transform.position + charCtrl.center;
        float distanceToGlowStick = 0;

        if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, 10))
        {
            distanceToGlowStick = hit.distance;
            if(hit.transform.tag == "glowstick")
            {

            }
        }
        
    }
}
