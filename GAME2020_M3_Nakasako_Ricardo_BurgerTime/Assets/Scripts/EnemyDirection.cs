using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour {
	[SerializeField] GameObject ThisGuyHere;

	private bool Opposite(TwoDirection first, TwoDirection second){
		return   (first == TwoDirection.Left && second == TwoDirection.Right)
		   || (first == TwoDirection.Right && second == TwoDirection.Left)
		   || (first == TwoDirection.Up && second == TwoDirection.Down)
		   || (first == TwoDirection.Down && second == TwoDirection.Up);
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "Crossroad") {

			// Define new direction, if necessary
			CrossroadInfo currentCrossroad = other.GetComponent<CrossroadInfo>();
			int nextDirectionSlot = (int)Mathf.Floor(Random.Range(0.0f,(float)currentCrossroad.possibleDirections.Length));
			TwoDirection nextDirection = currentCrossroad.possibleDirections[nextDirectionSlot];
 
			EnemyMovement currentMovement = ThisGuyHere.GetComponent<EnemyMovement>();
			if(Opposite(currentMovement.GetCurrentDirection(), nextDirection)){
				nextDirectionSlot = (nextDirectionSlot+1) % currentCrossroad.possibleDirections.Length;
			}
			currentMovement.SnapXChangeDirection(other.transform.position.x, currentCrossroad.possibleDirections[nextDirectionSlot]);
		}
	}



}
