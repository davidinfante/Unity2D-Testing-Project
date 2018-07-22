using UnityEngine;
using System.Collections;

public class RidleyBehaviour : MonoBehaviour {

	public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (health <= 0) transform.rotation = Quaternion.AngleAxis(180f, Vector3.forward);
	}

	// Colisiones
	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Bullet") {
			health -= 10;
		}
	}

	public float getHealth () {
		return health;
	}
}
