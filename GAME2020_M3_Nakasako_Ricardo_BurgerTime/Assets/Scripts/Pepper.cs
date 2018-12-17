using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : MonoBehaviour {

	public float pepperInterval;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, pepperInterval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
