#pragma strict


var Player : GameObject;
var Floor : GameObject;
var enemyPrefab : GameObject;
var blood : GameObject;

function Start () {
}

function Update () {
	transform.position.y = Floor.transform.position.y + (enemyPrefab.transform.localScale.y / 2);
	var distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x,2) + Mathf.Pow(transform.position.z - Player.transform.position.z,2));
	if (distance > 1.5) {
		transform.position.x = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime).x;
		transform.position.z = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime).z;
	}
	if (distance < 1.6) {
		Instantiate(blood, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
	}
	transform.LookAt(Player.transform.position);
}