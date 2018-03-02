using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public GameObject home;
    public GameObject controlpage;

	// Use this for initialization
	void Start () {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void showControls()
    {
        home.SetActive(false);
        controlpage.SetActive(true);
    }

    public void backtoHome()
    {
        home.SetActive(true);
        controlpage.SetActive(false);
    }
}
