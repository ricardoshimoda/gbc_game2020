using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

	[SerializeField] ThisPlayer currentPlayer;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Platform") {
			currentPlayer.SetGround (true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Platform") {
			currentPlayer.SetGround (false);
		}
	}

}
