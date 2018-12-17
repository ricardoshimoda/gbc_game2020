using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

	float x = 0;
	public bool isFalling = false;
	float moveSpeed = 0.8f;

	void Update(){
		if(isFalling){
			transform.Translate (0, - moveSpeed * Time.fixedDeltaTime,0);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			x= other.transform.position.x;
		}
		if(other.tag == "Platform"){
			isFalling = false;
		}
		if(other.tag == "Tray"){
			isFalling = false;
			gameObject.tag = "Tray";
		}
		if(other.tag == "Ingredient" ){
			Ingredient nextOne = other.gameObject.GetComponent<Ingredient>();
			nextOne.isFalling=true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			if (Mathf.Abs(other.transform.position.x - x) > 1.3){
				isFalling = true;
			}
		}
	}
}
