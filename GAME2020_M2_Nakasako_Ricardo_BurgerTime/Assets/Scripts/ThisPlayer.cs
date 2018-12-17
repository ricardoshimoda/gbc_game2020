using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisPlayer : MonoBehaviour {

	[SerializeField] float moveSpeed;
	[SerializeField] Transform groundCheck;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] Transform pepperPointRight;
	[SerializeField] Transform pepperPointLeft;
	[SerializeField] GameObject pepperThrow;

	Rigidbody2D rb;
    bool onLadder = false;
	bool onGround = true;
	public bool paused= false;
	float axisH, axisV;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.P)){
			paused = true;
			pauseMenu.SetActive(true);
		}
		if(!paused){
			ThrowPepper();
			DoMoveChecks();
			FixedUpdate();
		}
	}

	void ThrowPepper(){
		if(Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)){
			Debug.Log("Pepper it!");
			var axisH = Input.GetAxis ("Horizontal");
			if(axisH < 0){
				Instantiate (pepperThrow, pepperPointLeft.position, pepperPointLeft.rotation);
			}
			else{
				Instantiate (pepperThrow, pepperPointRight.position, pepperPointRight.rotation);
			}
		}
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
		//anim.SetBool ("Ladder", onLadder);
	}

	public void SetGround(bool situation){
		onGround = situation;
		//anim.SetBool ("Ladder", onLadder);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ladder") {
			SetLadder (true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Ladder") {
			SetLadder (false);
		}
	}


}
 