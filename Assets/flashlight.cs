using UnityEngine;
using System.Collections;

public class flashlight : MonoBehaviour {
	private Vector3 v3Offset;
	private GameObject goFollow;
	private float speed = 6.0f;

	private HandController controller;

	public Camera CameraFacing;

	// Use this for initialization
	void Start () {
		controller = transform.parent.GetComponent<HandController> ();
		goFollow = GameObject.Find ("OVRPlayerController");
		v3Offset = transform.position - goFollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		HandModel[] hands = controller.GetAllPhysicsHands ();
		if (hands.Length > 0) {
			HandModel hand = hands[0];
			FingerModel pointer = hand.fingers[1];
			Vector3 direction = pointer.GetRay ().direction;
			Vector3 origin = pointer.GetRay ().origin;


			//NOTE: this is buggy
			//adjust light source position to finger position
			transform.position = goFollow.transform.position;//+ (0.9f * pointer.GetTipPosition ());
			//change rotation by both camera and finger
			Quaternion q = pointer.GetBoneRotation (1);
			transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion(q.x * 1.2f, q.y * 1.2f, q.z * 1.2f, q.w * 1.2f), speed * Time.deltaTime);

			transform.rotation = Quaternion.Slerp (transform.rotation, CameraFacing.transform.rotation, speed * Time.deltaTime);
			//transform.rotation = Quaternion.Slerp (transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
			//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (goFollow.transform.localRotation.eulerAngles), speed*Time.deltaTime);
		}
	}
}
