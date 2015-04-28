#pragma strict

var projectile : Rigidbody;
var speed = 10;

function Update() {
	if(Input.GetKey("space")) {
		var clone = Instantiate(projectile, transform.position, transform.rotation);
		clone.velocity = transform.TransformDirection(Vector3(0, 0, speed));
		
		Destroy(clone.gameObject, 5);
	}
}