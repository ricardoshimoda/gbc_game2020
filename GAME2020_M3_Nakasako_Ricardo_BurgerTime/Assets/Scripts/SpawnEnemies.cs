using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

	[SerializeField] GameObject enemy;
	[SerializeField] float spawnTimer;
	[SerializeField] int maxNumberEnemies;
	[SerializeField] GameObject[] spawnPositions;
	/* 0-> left upper corner */
	/* 1-> left down corner */
	/* 2-> right upper corner */
	/* 3-> right down corner */

	private static GameObject[] enemies;

	
	void Start(){
		enemies = new GameObject[maxNumberEnemies];
		InvokeRepeating ("SpawnEnemy", 0, spawnTimer);
	}

	void SpawnEnemy(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(!ThisPlayer.Paused)
		{
			int enemyCounter = 0;
			for(; enemyCounter < maxNumberEnemies; enemyCounter++){
				if(enemies[enemyCounter] == null){
					break;
				}
			}
			TwoDirection initialDirection = TwoDirection.Right;
			if(enemyCounter < maxNumberEnemies){
				int spawnIndex = 0;
				if(player.transform.position.x < 0) {
					/* Player is on the left - we spawn on the right */
					spawnIndex = 2;
					initialDirection = TwoDirection.Left;
				}
				if(player.transform.position.y > 0.83){
					/* Player is up - we spawn down */
					spawnIndex++;
				}
				Transform finalSpawnPosition = spawnPositions[spawnIndex].transform;
				enemies[enemyCounter] = Instantiate (enemy, finalSpawnPosition.position, finalSpawnPosition.rotation);
				EnemyMovement enemyMovement = enemies[enemyCounter].GetComponent<EnemyMovement>();
				enemyMovement.SnapXChangeDirection(-20, initialDirection);
			}
		}
	}


}
