using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	public float speed;
	private float waitTime;
	public float startWaitTime;

	public Transform[] Location;
	private int randomLocation;


	void Start () {
		waitTime = startWaitTime;
		randomLocation = Random.Range(0,Location.Length);
	}
	
	
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, Location [randomLocation].position, speed * Time.deltaTime);
        
		if (Vector3.Distance (transform.position, Location [randomLocation].position) < 0.2f) {
			if (waitTime <= 0) {
				randomLocation = Random.Range (0, Location.Length);
				waitTime = startWaitTime;
			} else {
				waitTime -= Time.deltaTime;
			} 
		}
	}
}
