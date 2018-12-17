using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PeterSounds{
	PepperThrow=0,
	Dying=1
}
public class ThisPlayer : MonoBehaviour {

	[SerializeField] float moveSpeed;
	[SerializeField] Transform groundCheck;
	[SerializeField] Transform pepperPointRight;
	[SerializeField] Transform pepperPointLeft;
	[SerializeField] GameObject pepperThrow;
	[SerializeField] AudioClip[] peterSounds;

	Rigidbody2D rb;
    bool onLadder = false;
	bool onGround = true;
	public static bool Paused= false;
	float axisH, axisV;
	float xLadder = 0;
	PlayerData playerData;
	bool throwingPepper = false; 
	AudioSource aud;
	GameObject pauseMenu;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		aud = GetComponent<AudioSource>();
		GameObject gameUI = GameObject.FindGameObjectWithTag("Control");
		playerData = gameUI.GetComponent<PlayerData>();
		for(int i = 0; i < gameUI.transform.GetChildCount(); i++){
			if("PauseMenu" == gameUI.transform.GetChild(i).name){
				pauseMenu = gameUI.transform.GetChild(i).gameObject;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.P)){
			Paused = true;
			pauseMenu.SetActive(true);
		}
		if(!Paused){
			ThrowPepper();
			DoMoveChecks();
			FixedUpdate();
		}
	}

	void ThrowPepper(){
		if( (!throwingPepper) && (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)) && (playerData.UsePepper())){
			aud.clip = peterSounds[(int)PeterSounds.PepperThrow];
			aud.Play();
			throwingPepper = true;
			var axisH = Input.GetAxis ("Horizontal");
			if(axisH < 0){
				Instantiate (pepperThrow, pepperPointRight.position, pepperPointRight.rotation);
			}
			else{
				Instantiate (pepperThrow, pepperPointLeft.position, pepperPointLeft.rotation);
			}
			Invoke("ReturnPepperControl", 2);
		}
	}

	void ReturnPepperControl(){
		throwingPepper = false;
	}

	void DoMoveChecks (){
		axisH = Input.GetAxis ("Horizontal");
		axisV = Input.GetAxis ("Vertical");
		if (onLadder) {
			rb.gravityScale = 0;
			rb.drag = 15;
		} else {
			rb.gravityScale = 3;
			rb.drag = 5;
			/* changes the player orientation - where he is looking at */
			if (axisH < 0) transform.localScale = new Vector3 (1, 1, 1);
			else if (axisH > 0) transform.localScale = new Vector3 (-1, 1, 1);
		}
	}

	void FixedUpdate(){
		if(onLadder){
			/* moves on vertical */
			transform.Translate (0, axisV * moveSpeed * Time.fixedDeltaTime,0);
		}
		if(onGround){
			/* moves on horizontal */
			transform.Translate (axisH * moveSpeed * Time.fixedDeltaTime, 0,0);
		}
	}

	void SetLadder(bool situation){
		onLadder = situation;
	}

	public void SetGround(bool situation){
		onGround = situation;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ladder") {
			SetLadder (true);
			xLadder = other.gameObject.transform.position.x;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Ladder") {
			SetLadder (false);
		}
		else if(other.tag == "Platform"){
			transform.Translate(xLadder - 0.24f - transform.position.x , 0, 0);
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag=="Enemy"){
			Paused = true;
			this.playerData.PlayerDeath();
		}
	}
}
 