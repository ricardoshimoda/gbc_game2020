﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {

	public void QuitApp(){
		Application.Quit();
		Debug.Log("Qutting the app");
	}
}
