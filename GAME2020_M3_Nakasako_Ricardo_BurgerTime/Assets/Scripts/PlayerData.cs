using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {
	[SerializeField] Text scoreDisplay;
	[SerializeField] Text hiScoreDisplay;
	[SerializeField] Text peppersDisplay;
	[SerializeField] Image[] peterLives;
	[SerializeField] Transform spawnPeter;
	[SerializeField] GameObject peterPepperPrefab;
	[SerializeField] AudioClip[] sounds;
	[SerializeField] GameObject finalScorePanel;
	[SerializeField] Text[] finalScoreDisplay;
	[SerializeField] GameObject hiScoreInput;
	[SerializeField] AudioSource backgroundMusic;
	int hitEnemyPoints = 100;
	int hitEnemyInstances = 0; 
	int burgerPlatformPoints = 50;
	int burgerPlatformInstances = 0;
	int burgerOtherBurgerPoints = 100;
	int burgerOtherBurgerInstances = 0;
	int pepperNotUsed = 50;
	int levelClear = 1000;
	int score = 0;
	int lives = 3;
	int peppers = 5;
	AudioSource aud;

	void Start(){
		aud = GetComponent<AudioSource>();
		GameObject PeterPepper = GameObject.FindGameObjectWithTag("Player");
		if(PeterPepper == null)
		{
			PeterPepper = Instantiate (peterPepperPrefab, spawnPeter.position, spawnPeter.rotation);
		}
		ThisPlayer.Paused = false;
		/* Ignore collisions between enenmies */
		Physics2D.IgnoreLayerCollision(8, 8);
		Physics2D.IgnoreLayerCollision(8, 10);
		/* Ignore collisions between disabled enemies */
		Physics2D.IgnoreLayerCollision(10, 10);
		/* Ignore collisions between player and disabled enemies */
		Physics2D.IgnoreLayerCollision(9, 10);
		if(TopTen.scores[0].score > -1) hiScoreDisplay.text = TopTen.scores[0].initials + " - " + TopTen.scores[0].score.ToString("N0");
	}

	void InstantiatePlayer(){
		Instantiate (peterPepperPrefab, spawnPeter.position, spawnPeter.rotation);
	}
	public bool UsePepper(){
		if(peppers > 0){
			--peppers;
			Debug.Log("Pepperit!" + peppers.ToString());
			peppersDisplay.text = peppers.ToString();
			return true;
		}
		return false;
	}

	public bool AnotherRound(){
		if(lives > 1){
			lives--;
			peterLives[lives-1].gameObject.SetActive(false);
			return true;
		}
		/* This is, like, the end of the game */
		return false;
	}

	public void EnemyHitScore(){
		score += hitEnemyPoints;
		hitEnemyInstances++;
		scoreDisplay.text = score.ToString();
		aud.clip = sounds[3];
		aud.Play();
	}
	public void BurgerPlatformScore(){
		score += burgerPlatformPoints;
		burgerPlatformInstances++;
		scoreDisplay.text = score.ToString();
		aud.clip = sounds[3];
		aud.Play();
	}
	public void BurgerOtherBurgerScore(){
		score += burgerOtherBurgerPoints;
		burgerOtherBurgerInstances++;
		scoreDisplay.text = score.ToString();
		aud.clip = sounds[3];
		aud.Play();
	}
	public void PlayerDeath(){
		aud.clip = sounds[0];
		aud.Play();
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < enemyList.Length; i++) Destroy(enemyList[i]);
		GameObject PeterPepper = GameObject.FindGameObjectWithTag("Player");
		Destroy(PeterPepper);
		if(AnotherRound()){
			InstantiatePlayer();
			ThisPlayer.Paused = false;
		}
		else{
			// This is the end, hold your breath and count to ten
			StartCoroutine("WaitForFalling");
		}
	}
	public void YoureAWinnerBaby(){
		backgroundMusic.Pause();
		aud.clip = sounds[2];
		aud.Play();
		Debug.Log("Before Level Clear: " + score);
		score += levelClear;
		Debug.Log("After Level Clear: " + score);
		score = score + (pepperNotUsed * peppers);
		Debug.Log("After Pepper Not Used: " + score);
		ShowPoints(true);
	}

	IEnumerator WaitForFalling(){
        yield return new WaitForSeconds(2);
		bool stillFalling = false;
		GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
		for(int i = 0; i < ingredients.Length; i++){
			Ingredient ing = ingredients[i].GetComponent<Ingredient>();
			if(ing.isFalling){
				stillFalling = true;
				break;
			}
		}
		if(stillFalling){
			StartCoroutine("WaitForFalling");
		}
		else{
			StartCoroutine("DeathSounds");
		}
	}
	IEnumerator DeathSounds()
    {
		backgroundMusic.Pause();
		aud.clip = sounds[1];
		aud.Play();
        yield return new WaitForSeconds(2);
		ShowPoints(false);
    }

	void ShowPoints(bool winner){
		finalScoreDisplay[0].text = hitEnemyInstances.ToString("N0");
		finalScoreDisplay[1].text = burgerPlatformInstances.ToString("N0");
		finalScoreDisplay[2].text = burgerOtherBurgerInstances.ToString("N0");
		finalScoreDisplay[4].text = (hitEnemyInstances * hitEnemyPoints).ToString("N0");
		finalScoreDisplay[5].text = (burgerPlatformInstances * burgerPlatformPoints).ToString("N0");
		finalScoreDisplay[6].text = (burgerOtherBurgerInstances * burgerOtherBurgerPoints).ToString("N0");

		if(winner){
			finalScoreDisplay[3].text = peppers.ToString("N0");
			finalScoreDisplay[7].text = (peppers*pepperNotUsed).ToString("N0");
			finalScoreDisplay[8].text = levelClear.ToString("N0");
		}
		else{
			finalScoreDisplay[3].text = "0";
			finalScoreDisplay[7].text = "0";
			finalScoreDisplay[8].text = "0";
		}
		finalScoreDisplay[9].text = score.ToString("N0");
		finalScorePanel.SetActive(true);
		if(TopTen.ShowsHiScoreScreen(score)){
			StartCoroutine("GetInitials");
		}
		else{
			StartCoroutine("GoMainMenu");
		}

	}
	IEnumerator GetInitials(){
        yield return new WaitForSeconds(5);
		finalScorePanel.SetActive(false);		
		HiScoreInitials hiScore = hiScoreInput.GetComponent<HiScoreInitials>();
		hiScore.finalScore = score;
		hiScoreInput.SetActive(true);
	}
	IEnumerator GoMainMenu()
    {
        yield return new WaitForSeconds(5);
		SceneManager.LoadScene("MainMenu");
    }

}
