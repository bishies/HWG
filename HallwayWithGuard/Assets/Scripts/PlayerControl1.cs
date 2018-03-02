using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    CharacterController charCtrl;
    GameObject glowstick;
    public GameObject glowstickPrefab;
    public float force;
    private int hasGS;
    Rigidbody rb;

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


    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        hasGS = 2;
    }
    

    void Update () {
        

		if (axes == RotationAxis.MouseX) {
			transform.Rotate (0, Input.GetAxis ("Mouse X") * Horizontal, 0);
		} else if (axes == RotationAxis.MouseY) {
			rotationX -= Input.GetAxis ("Mouse Y") * Vertical;
			rotationX = Mathf.Clamp (rotationX, minV, maxV);

			float rotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3 (rotationX, rotationY, 0);
		}
        
        if (hasGS>0)
        {
            //glowstick.transform.position = transform.position;
            Debug.Log("CAN INSTANTIATE");
            if (Input.GetMouseButtonDown(0))
            {
                hasGS--;
                glowstick = Instantiate(glowstickPrefab) as GameObject;
                glowstick.transform.position = transform.position + new Vector3(0f,3f,0f);
                Debug.Log("INSTANTIATED");
                Rigidbody rb = glowstick.GetComponent<Rigidbody>();
                //rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
                rb.velocity = transform.forward * 20f;
                rb.AddTorque(0f, 0f, 20f, ForceMode.Impulse);
            }

        }

    }
}
