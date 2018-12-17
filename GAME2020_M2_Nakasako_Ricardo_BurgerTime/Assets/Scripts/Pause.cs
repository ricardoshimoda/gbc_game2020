using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;

	[SerializeField] GameObject playerGO;

	public void TogglePause(){
		pauseMenu.SetActive(false);
		ThisPlayer player = playerGO.GetComponent<ThisPlayer>();
		player.paused = false;
	}
}
