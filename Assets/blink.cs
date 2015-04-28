using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {

	private const float beatsperminute = 122.0f;

	//number of seconds to count down from to 0
	//1 / (x beats / 1 minute)(1 minute / 60 secs)(1 flash / 2 beats)
	private const float countdown = 1.0f / (beatsperminute / (60.0f * 2.0f));
	private float timer = countdown;

	private Light myLight;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			blinkLight();
			timer = countdown;
		}
	}
	void blinkLight(){
		//myLight.enabled = true;
		//myLight.enabled = false;
		myLight.enabled = !(myLight.enabled);
	}
}
