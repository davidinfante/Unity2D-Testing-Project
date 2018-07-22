using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	public float speed = 0.1f;
	public bool launched;
	public bool stillLaunched;
	public bool directionRight;

	public AudioClip shootSound;
	private AudioSource source;

	public GameObject samus;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		directionRight = samus.GetComponent<SamusBehaviour>().getIsFacingRight();
		source = GetComponent<AudioSource>();

		launched = false;
		stillLaunched = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {

		this.OnKeyDown();

		if (launched) {
			Vector3 temp;
			if (directionRight) temp = new Vector3(this.transform.position.x+speed,this.transform.position.y,this.transform.position.z);
			else temp = new Vector3(this.transform.position.x-speed,this.transform.position.y,this.transform.position.z);
			this.transform.position = temp;
			if (this.transform.position.x >= 10 || this.transform.position.x <= -10) {
				launched = false;
				stillLaunched = false;
			}
		}

	}

	private void OnKeyDown () {
		//Tecla E - Disparar
		if (Input.GetKeyDown(KeyCode.E) && !stillLaunched && samus.GetComponent<SamusBehaviour>().getActualAmmo() > 0) {

			samus.GetComponent<SamusBehaviour>().setActualAmmo(samus.GetComponent<SamusBehaviour>().getActualAmmo()-1);

			launched = true;
			stillLaunched = true;
			Vector3 temp;
			if (samus.GetComponent<SamusBehaviour>().getIsFacingRight()) {
				temp = new Vector3(samus.transform.position.x+1.1f,samus.transform.position.y+0.1f,samus.transform.position.z);
				directionRight = true;
				sr.flipX = false;
			}
			else {
				temp = new Vector3(samus.transform.position.x-1.1f,samus.transform.position.y+0.1f,samus.transform.position.z);
				directionRight = false;
				sr.flipX = true;
			}
			this.transform.position = temp;

			source.PlayOneShot(shootSound, 0.2f);
		}
		
		//Tecla R - Recargar
		else if (Input.GetKeyDown(KeyCode.R)) {
			//Llamar a Reload en 1 segundo
			Invoke("Reload", 1);
		}
	}

	private void OnKeyUp () {
		if (Input.GetKeyUp(KeyCode.E)) launched = false;
	}

	// Colisiones
	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Ridley") {
			launched = false;
			stillLaunched = false;
			Vector3 temp = new Vector3(-8f,-5.5f,0);
			this.transform.position = temp;
		}
	}

	public void Reload () {
		samus.GetComponent<SamusBehaviour>().setActualAmmo(samus.GetComponent<SamusBehaviour>().getMaxAmmo());
	}

}
