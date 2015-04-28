#pragma strict

var Enemy: Transform;
private var Timer: float;

function Awake() {
	Timer = Time.time + 5;
}

function Update() {
	if (Timer < Time.time) {
		Instantiate(Enemy, transform.position, transform.rotation);
		Timer = Time.time + 5;
	}
}