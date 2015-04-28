#pragma strict


var Player : GameObject;
var Floor : GameObject;
var enemyPrefab : GameObject;
var blood : GameObject;

function Start () {
}

function Update () {
	var distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x,2) + Mathf.Pow(transform.position.z - Player.transform.position.z,2));
	if (distance > 1.5) {
		transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime);
		transform.position.y -= transform.position.y - 3.0;
	}
	if (distance < 1.6) {
		Instantiate(blood, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
	}
	transform.LookAt(Player.transform.position);
}