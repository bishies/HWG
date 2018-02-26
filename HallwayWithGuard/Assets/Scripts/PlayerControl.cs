using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


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



	void Update () {
		if (axes == RotationAxis.MouseX) {
			transform.Rotate (0, Input.GetAxis ("Mouse X") * Horizontal, 0);
		} else if (axes == RotationAxis.MouseY) {
			rotationX -= Input.GetAxis ("Mouse Y") * Vertical;
			rotationX = Mathf.Clamp (rotationX, minV, maxV);

			float rotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3 (rotationX, rotationY, 0);
		}

	}
}
