using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TwoDirection{
	Empty,
	Right,
	Left,
	Up,
	Down
}

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed;
	[SerializeField] float secondsParalyzed;

	TwoDirection currentDirection = TwoDirection.Empty;

	bool onLadder;
	Rigidbody2D rb;

	PlayerData playerData;
	AudioSource aud;

	public bool underPepperInfluence = false;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D> ();
		GameObject controller = GameObject.FindGameObjectWithTag("Control");
		playerData = controller.GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!ThisPlayer.Paused && !underPepperInfluence){
			DoMoveChecks();
			ChangeDirection();
		}
	}

	TwoDirection GetOposite(TwoDirection currentDirection){
		if(currentDirection == TwoDirection.Up){
			return TwoDirection.Down;
		}
		else if(currentDirection == TwoDirection.Down){
			return TwoDirection.Up;
		}
		else if(currentDirection == TwoDirection.Left){
			return TwoDirection.Right;
		}
		return TwoDirection.Left;
	}

	void ChangeDirection(){
		switch(currentDirection)
		{
			case TwoDirection.Right:
				transform.Translate (moveSpeed * Time.fixedDeltaTime, 0,0);
				break;
			case TwoDirection.Left:
				transform.Translate (-moveSpeed * Time.fixedDeltaTime, 0,0);
				break;
			case TwoDirection.Up:
				transform.Translate (0, moveSpeed * Time.fixedDeltaTime, 0,0);
				break;
			case TwoDirection.Down:
				transform.Translate (0,-moveSpeed * Time.fixedDeltaTime, 0,0);
				break;
				
		}
	}

	void DoMoveChecks (){
		if (onLadder) {
			rb.gravityScale = 0;
			rb.drag = 15;
		} else {
			rb.gravityScale = 3;
			rb.drag = 5;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ladder") {
			onLadder = true;
			return;
		}
		if(other.tag == "Ingredient"){
			Ingredient nhamNham = other.gameObject.GetComponent<Ingredient>();
			if(nhamNham != null && nhamNham.isFalling){
				playerData.EnemyHitScore();
				Destroy(this.gameObject);
			}
			return;
		}
		if(other.tag == "Crossroad" || other.tag == "Boundaries" || other.tag == "Platform")
			return;
		Debug.Log(other.tag);
		if(other.tag == "Pepper"){
			Debug.Log("Frozen by pepper");
			// you're frozen - when your heart is not open
			underPepperInfluence = true;
			gameObject.layer = 10;
			aud.Play();
			Invoke("ReturnToNormal", secondsParalyzed);
		}
	}

	void ReturnToNormal(){
		underPepperInfluence = false;
		gameObject.layer = 8;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Ladder") {
			onLadder = false;
		}
	}

	public void SnapXChangeDirection(float newX, TwoDirection newDirection){
		if(newX > -10){
			float yBump = 0.0f;
			if(currentDirection == TwoDirection.Up) yBump = 0.07f;
			if(currentDirection == TwoDirection.Down) yBump = -0.07f;
			transform.Translate(newX - transform.position.x , yBump, 0);
		}
		currentDirection = newDirection;
	}

	void OnCollisionEnter2D(Collision2D other){
		return;
	}

	public TwoDirection GetCurrentDirection(){return currentDirection;}

}
