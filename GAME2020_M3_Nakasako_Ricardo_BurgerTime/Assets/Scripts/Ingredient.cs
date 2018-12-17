using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType {
	TopBun,
	Lettuce,
	Patty,
	BottomBun
}

public class Ingredient : MonoBehaviour {

	float x = 0;
	public bool isFalling = false;
	public bool goesOn = false;
	public IngredientType myType;
	float moveSpeed = 0.8f;
	PlayerData playerData;
	bool firstFall = true;
	AudioSource aud;
	[SerializeField] AudioClip[] sounds;

	void Start(){
		aud = GetComponent<AudioSource>();
		GameObject controller = GameObject.FindGameObjectWithTag("Control");
		playerData = controller.GetComponent<PlayerData>();
	}

	void Update(){
		if(isFalling){
			float vInitial = 0.8f;
			float vAdd = 0.4f * (Mathf.Abs(transform.position.y - 0.23f)/6.08f);
			transform.Translate (0, - moveSpeed * (vInitial + vAdd) * Time.fixedDeltaTime,0);
		}

	}

	public void setFall(){
		isFalling = true;
		aud.clip = sounds[1];
		aud.Play();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Enemy" && isFalling){
			//Debug.Log("[" + name + "]: Set GoesOn=true");
			goesOn = true;
			// Squish!
			aud.clip = sounds[0];
			aud.Play();
		}
		if (other.tag == "Player") {
			x= other.transform.position.x;
		}
		if(other.tag == "Platform"){
			if(firstFall) firstFall = false;
			else playerData.BurgerPlatformScore();

			if(goesOn){
				//Debug.Log("[" + name + "]: Set GoesOn=false - goes to the next platform");
				goesOn = false;
			}
			else{
				//Debug.Log("[" + name + "]: Set Falling=false - Stops immediately");
				isFalling = false;
			}
		}
		if(other.tag == "Tray"){
			//Debug.Log("[" + name + "]: Set Falling=false - Stops immediately in the Tray and becomes part of it");
			isFalling = false;
			gameObject.tag = "Tray";
			playerData.BurgerPlatformScore();
			WinCheck();
		}
		if(other.tag == "Ingredient" ){
			//Debug.Log("[" + name + "]: Hits another ingredient");
			Ingredient nextOne = other.gameObject.GetComponent<Ingredient>();
			if(ValidateCollisionFromTop(nextOne.myType)){
				//Debug.Log("[" + name + "]: Hits a lower ingredient");
				/* Counts points only once */
				playerData.BurgerOtherBurgerScore();
				nextOne.setFall();
				nextOne.goesOn = goesOn;
			}
		}
	}

	void WinCheck(){
		GameObject leftover = GameObject.FindGameObjectWithTag("Ingredient");
		if(leftover == null)
		{
			ThisPlayer.Paused = true;
			playerData.YoureAWinnerBaby();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			if (Mathf.Abs(other.transform.position.x - x) > 1.3){
				setFall();
			}
		}
	}

	bool ValidateCollisionFromTop(IngredientType otherType){
		return (myType == IngredientType.TopBun && otherType == IngredientType.Lettuce )
		    || (myType == IngredientType.Lettuce && otherType == IngredientType.Patty)
		    || (myType == IngredientType.Patty && otherType == IngredientType.BottomBun);
	}
}
