using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


[System.Serializable]
public static class TopTen {
	const int SCORE_TABLE = 10;
	public const int SCORE_MIN = -1;
	public static GameScore[] scores;
	static TopTen(){
		scores = new GameScore[SCORE_TABLE];
		for(int i = 0; i < SCORE_TABLE; i++){
			scores[i] = new GameScore();
		}
	}
	public static void AddHiScore(string newInitials, int newScore){
		int slot = 0;
		while(scores[slot].score > newScore && slot < SCORE_TABLE)
			slot++;
		if(slot == SCORE_TABLE) return;
		if(scores[slot].score == SCORE_MIN){
			scores[slot].initials = newInitials;
			scores[slot].score = newScore;
		}
		else{
			// move down the road
			for(int i = SCORE_TABLE-1; i>slot; i--){
				scores[i].initials = scores[i-1].initials;
				scores[i].score = scores[i-1].score;
			}
			// sets the score straight
			scores[slot].initials = newInitials;
			scores[slot].score = newScore;
			Debug.Log(slot);
		}

	}
	public static bool ShowsHiScoreScreen(int newScore){
		return newScore > scores[SCORE_TABLE-1].score;
	}
}
[System.Serializable]
public class GameScore
{
	public string initials = string.Empty;
	public int score = TopTen.SCORE_MIN;
}
