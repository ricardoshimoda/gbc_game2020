using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiScoreInitials : MonoBehaviour {

	[SerializeField] Text[] initials;
	public int finalScore = 0;

	int FilledIn = 0;
	// Use this for initialization
	void Start () {
		
	}

	public void SaveHiScoreMainMenu(){
		string hiScoreInitials = initials[0].text + initials[1].text + initials[2].text;
		TopTen.AddHiScore(hiScoreInitials, finalScore);
		SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if(FilledIn < 3){
			if(Input.GetKeyDown(KeyCode.A)){
				initials[FilledIn].text = "A";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.B)){
				initials[FilledIn].text = "B";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.C)){
				initials[FilledIn].text = "C";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.D)){
				initials[FilledIn].text = "D";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.E)){
				initials[FilledIn].text = "E";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.F)){
				initials[FilledIn].text = "F";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.G)){
				initials[FilledIn].text = "G";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.H)){
				initials[FilledIn].text = "H";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.I)){
				initials[FilledIn].text = "I";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.J)){
				initials[FilledIn].text = "J";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.K)){
				initials[FilledIn].text = "K";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.L)){
				initials[FilledIn].text = "L";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.M)){
				initials[FilledIn].text = "M";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.N)){
				initials[FilledIn].text = "N";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.O)){
				initials[FilledIn].text = "O";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.P)){
				initials[FilledIn].text = "P";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Q)){
				initials[FilledIn].text = "Q";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.R)){
				initials[FilledIn].text = "R";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.S)){
				initials[FilledIn].text = "S";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.T)){
				initials[FilledIn].text = "T";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.U)){
				initials[FilledIn].text = "U";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.V)){
				initials[FilledIn].text = "V";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.X)){
				initials[FilledIn].text = "X";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.W)){
				initials[FilledIn].text = "W";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Y)){
				initials[FilledIn].text = "Y";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Z)){
				initials[FilledIn].text = "Z";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Underscore)){
				initials[FilledIn].text = "_";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				initials[FilledIn].text = " ";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.At)){
				initials[FilledIn].text = "@";
				FilledIn++;
			}
			if(Input.GetKeyDown(KeyCode.Backspace)){
				FilledIn--;
				initials[FilledIn].text = "_";
			}
		}
	}
}
