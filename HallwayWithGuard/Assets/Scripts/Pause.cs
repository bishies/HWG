using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	private bool paused =false;

	void Update () {
		pauseGame ();


	  

	}

	void pauseGame(){
		if (Input.GetKeyDown ("p"))
		{	
			Debug.Log ("pressed");
			if (!paused) {
				paused = true;
				Time.timeScale = .0001f;
			} else {
				paused = false;
				Time.timeScale = 1.0f;
			}
		}
	}
}