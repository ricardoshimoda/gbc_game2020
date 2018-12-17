using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Leaderboard : MonoBehaviour {

	[SerializeField] Text[] InitialsAndScores;
	void Start () {
		for(int i = 0; i < 10; i++){
			if(TopTen.scores[i].score < 0){
				InitialsAndScores[2*i].text = "-";
				InitialsAndScores[2*i+1].text = "-";
			}
			else{
				InitialsAndScores[2*i].text = TopTen.scores[i].initials;
				InitialsAndScores[2*i+1].text = TopTen.scores[i].score.ToString("N0");
			}
		}
	}
	
}
