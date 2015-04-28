#pragma strict

var explosion: GameObject;

function OnTriggerEnter (theCollision : Collider) {
	if(theCollision.tag == "Player") {
		Destroy(gameObject);
		Instantiate(explosion, theCollision.transform.position, theCollision.transform.rotation);
	}
}