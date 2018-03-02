using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class Player_Movement : MonoBehaviour {

	public float speed = 4.0f;
	public float gravity = -9.8f;

    public AudioClip[] footsteps;
    AudioSource caudio;
    private bool step = true;

    private CharacterController _charCont;


	void Start () {
		_charCont = GetComponent<CharacterController> ();
        caudio = GetComponent<AudioSource>();
        caudio.volume = 0.01f;
    }


	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed);

        if (Input.anyKey && step && !Input.GetMouseButton(0))
        {
            int n = Random.Range(1, footsteps.Length);
            caudio.clip = footsteps[n];
            caudio.PlayOneShot(caudio.clip);
            footsteps[n] = footsteps[0];
            footsteps[0] = caudio.clip;
            StartCoroutine(WaitForFootSteps(.45f));
        }

        movement *= Time.deltaTime;		
		movement = transform.TransformDirection(movement);
		_charCont.Move (movement);
	}

    IEnumerator WaitForFootSteps(float stepsLength)
    {
        step = false; yield return new WaitForSeconds(stepsLength); step = true;
    }
}
