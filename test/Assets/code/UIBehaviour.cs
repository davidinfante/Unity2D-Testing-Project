using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

	public float samusHealth;
	public float ridleyHealth;

	public GameObject samus;
	public GameObject ridley;

	public Image samusHealthBar;
	public Image samusHealthBarIcon;
	public Image ridleyHealthBar;
	public Image ridleyHealthBarIcon;

	public Text ammo;
	public Text gameEnd;

	// Use this for initialization
	void Start () {
		ammo.text = samus.GetComponent<SamusBehaviour>().getActualAmmo().ToString() + "/" + samus.GetComponent<SamusBehaviour>().getMaxAmmo().ToString();
		gameEnd.enabled = false;

		samusHealth = samus.GetComponent<SamusBehaviour>().getHealth();
		ridleyHealth = ridley.GetComponent<RidleyBehaviour>().getHealth();
	}
	
	// Update is called once per frame
	void Update () {
		this.UpdateAmmo();
		this.UpdateGameEnd();
	}

	void FixedUpdate () {
		this.UpdateSamusHealthBar();
		this.UpdateRidleyHealthBar();
	}

	public void UpdateAmmo () {
		ammo.text = samus.GetComponent<SamusBehaviour>().getActualAmmo().ToString() + "/" + samus.GetComponent<SamusBehaviour>().getMaxAmmo().ToString();
	}

	public void UpdateGameEnd () {
		if (ridleyHealth == 0) {
			gameEnd.text = "YOU WIN";
			gameEnd.enabled = true;
			samus.GetComponent<SamusBehaviour>().setEnableControls(false);
		}
		else if (samusHealth == 0) {
			gameEnd.text = "YOU LOSE";
			gameEnd.enabled = true;
			samus.GetComponent<SamusBehaviour>().setEnableControls(false);
		}
	}

	public void UpdateSamusHealthBar () {
		samusHealth = samus.GetComponent<SamusBehaviour>().getHealth();

		if (samusHealth >= 0) samusHealthBar.transform.localScale = new Vector3(samusHealth/100, 1, 1);
		if (samusHealth <= 0) samusHealthBarIcon.enabled = false;
	}

	public void UpdateRidleyHealthBar () {
		ridleyHealth = ridley.GetComponent<RidleyBehaviour>().getHealth();

		if (ridleyHealth >= 0) ridleyHealthBar.transform.localScale = new Vector3(ridleyHealth/100, 1, 1);
		if (ridleyHealth <= 0) ridleyHealthBarIcon.enabled = false;
	}
}
