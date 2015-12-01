using UnityEngine;
using System.Collections;

public class Heartbeat : MonoBehaviour {

	public LSLControllerHeartbeat controller;
	public AudioSource  audioSource;

	private bool lastHeartbeat = false;
	
	// Use this for initialization
	void Start () {
		
	}
	 
	// Update is called once per frame
	void Update () {

        if (controller.Heartbeat == true) //&& lastHeartbeat == false)
        {
            //lastHeartbeat = controller.Heartbeat;

            controller.Heartbeat = false;

            audioSource.Play();
        }
        else
        {
            //lastHeartbeat = controller.Heartbeat;
        }
    }
}
