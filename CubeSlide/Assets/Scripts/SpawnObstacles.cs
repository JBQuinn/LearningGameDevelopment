using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour {

	public GameObject obstaclePrefab; //This is the obstacle prefab we will generate along the path
	public GameObject ground; //This is the ground GameObject
	public float depthSpacing = 50f; //How far apart the obstacles should be depth wise
	float[] positions = {-6f, -3f, 0f, 3f, 6f,}; //The 7 different positions the obstacles can be in
	private int counter = 0; //Counts how many rows have been created
	private int indexOfLastHole = 0;

	// Use this for initialization
	void Start () {
		//get the starting position of the player
		Vector3 playerPostion = GameObject.Find("Player").transform.position;
		//Find the z coordinate for the end of the level
		float endOfLevel = ground.transform.position.z+ground.transform.localScale.z/2;
		//Find how far away the first obstacle will be from the player
		float firstDepth = playerPostion.z + 25 + depthSpacing;
		//loop through the different depths for each set of obstacles
		for (float x = firstDepth; x < endOfLevel; x += depthSpacing) {
			//Convert the array of positions to a list
			ArrayList listOfPositions = new ArrayList(positions);
			//if the player is still in the first 100
			if( counter < 500 ) {
				for (int i = 0; i < 3; i++) {
					listOfPositions.RemoveAt(Random.Range(0,listOfPositions.Count));
				}
			} else if (counter < 1000) {
				for (int i = 0; i < 2; i ++) {
					listOfPositions.RemoveAt(Random.Range(0, listOfPositions.Count));
				}
			} else {
				if(indexOfLastHole>0) {
					if (indexOfLastHole < 6) {
						indexOfLastHole = Random.Range(indexOfLastHole-1,indexOfLastHole+2);
					} else {
						indexOfLastHole = Random.Range(indexOfLastHole-1,indexOfLastHole+1);
					}
				} else {
					indexOfLastHole = Random.Range(indexOfLastHole,indexOfLastHole+2);
				}
			}
			//loop through the different positions the obstacles can be in
			foreach ( float y in listOfPositions ) {
				Instantiate(obstaclePrefab, new Vector3(y,1f,x), Quaternion.identity);
			}
			counter ++;
		}
	}
}
