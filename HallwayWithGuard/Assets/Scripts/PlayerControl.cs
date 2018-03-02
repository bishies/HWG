using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    CharacterController charCtrl;
    GameObject glowstick;
    public GameObject glowstickPrefab;
    public float force;
    private int hasGS;
    private bool coinget;
    Rigidbody rb;
    public Text gsCounter;
    public Text Coingot;
    public AudioClip coinnoise;
    AudioSource caudio;

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
        coinget = false;
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
            //Debug.Log("CAN INSTANTIATE");
            if (Input.GetMouseButtonDown(0))
            {
                hasGS--;
                glowstick = Instantiate(glowstickPrefab, transform.position + new Vector3(0f, 1f, 0f), transform.rotation) as GameObject;
                //glowstick.transform.position = transform.position + new Vector3(0f,1f,0f);
                //Debug.Log("INSTANTIATED");
                Rigidbody rb = glowstick.GetComponent<Rigidbody>();
                //rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
                rb.velocity = transform.forward * force;
                rb.AddTorque(0f, 0f, 20f, ForceMode.Impulse);
            }

        }

        //gsCounter.text = "Glowsticks left: " + hasGS;

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "FIN")
            Debug.Log("YOU HERE");
        if (collision.gameObject.tag == "FIN" &&  coinget)
        {
            Coingot.text = "You win!";
            Invoke("restartGame", 3f);
        }
        if(collision.gameObject.tag == "Coin")
        {
            Debug.Log("COIN GOT");
            caudio.clip = coinnoise;
            caudio.PlayOneShot(caudio.clip);
            coinget = true;
            Coingot.text = "You got the coin! Now go back.";
            Destroy(collision.gameObject);
        }
    }

    void restartGame()
    {
        SceneManager.LoadScene(0);
    }
}
