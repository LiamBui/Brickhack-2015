using UnityEngine;
using System.Collections;

public class enemy_movement : MonoBehaviour {
	public GameObject Player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, 1.0f*Time.deltaTime);
	}
}
