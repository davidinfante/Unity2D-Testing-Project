using UnityEngine;
using System.Collections;

public class SamusBehaviour : MonoBehaviour {

	public float health = 100f;
	public float jumpSpeed = 140f;

	public bool isFacingRight;
	public bool movingLeft = false;
	public bool movingRight = false;
	public bool grounded = false;
	public bool jumping = false;
	public bool canJump = false;
	public bool doubleJump = false;

	public bool enableControls;

	public int actualAmmo = 10;
	public static int maxAmmo = 10;

	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		sr = gameObject.GetComponent<SpriteRenderer>();
		anim = gameObject.GetComponent<Animator>();

		isFacingRight = true;
		enableControls = true;
		actualAmmo = 10;
	}
	
	// Update is called once per frame
	void Update () {

		this.OnKeyDown();
		
		this.OnKeyUp();

		this.UpdateAnimation();
	}

	void FixedUpdate () {
		this.Movement();
		if (health <= 0 && isFacingRight) transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
		else if (health <= 0 && !isFacingRight) transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
	}

	// Presionar una tecla
	private void OnKeyDown () {
		if (enableControls) {
			if (Input.GetKeyDown(KeyCode.A)) movingLeft = true;
			else if (Input.GetKeyDown(KeyCode.D)) movingRight = true;
			if (Input.GetKeyDown(KeyCode.Space)) jumping = true;
		}
	}

	// Dejar de presionar una tecla
	private void OnKeyUp () {
		if (Input.GetKeyUp(KeyCode.A)) movingLeft = false;
		else if (Input.GetKeyUp(KeyCode.D)) movingRight = false;
		if (Input.GetKeyUp(KeyCode.Space)) jumping = false;
	}

	// Movimiento
	private void Movement () {
		//Moverse
		if (movingLeft) {
			if (isFacingRight) sr.flipX = true;
			isFacingRight = false;
			Vector3 temp = new Vector3(-0.05f,0,0);
			this.transform.position += temp;
		}
		else if (movingRight) {
			if (!isFacingRight) sr.flipX = false;
			isFacingRight = true;
			Vector3 temp = new Vector3(+0.05f,0,0);
			this.transform.position += temp;
		}

		// Saltar y doble salto
		if (jumping && canJump) {
			grounded = false;
			canJump = false;
			jumping = false;
			rb2d.AddForce(Vector2.up * jumpSpeed);
		}
		else if (jumping && doubleJump) {
			doubleJump = false;
			rb2d.velocity = Vector3.zero;
    		rb2d.angularVelocity = 0f;
			rb2d.AddForce(Vector2.up * jumpSpeed);
		}
	}

	public void UpdateAnimation () {
		if (movingLeft || movingRight) anim.SetBool("Running", true);
		else if (!movingLeft && !movingRight) anim.SetBool("Running", false);
	}

	// Colisiones
	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Ground") {
			grounded = true;
			canJump = true;
			doubleJump = true;
		}
		else if (col.gameObject.name == "Ridley") {
			health -= 10;
		}
	}

	/**
	** GETTERS
	*/

	public float getHealth () {
		return health;
	}

	public bool getIsFacingRight () {
		return isFacingRight;
	}

	public int getActualAmmo () {
		return actualAmmo;
	}

	public int getMaxAmmo () {
		return maxAmmo;
	}

	/**
	** SETTERS
	*/

	public void setActualAmmo (int newAmmo) {
		actualAmmo = newAmmo;
	}

	public void setEnableControls (bool enabled) {
		enableControls = enabled;
	}
}
