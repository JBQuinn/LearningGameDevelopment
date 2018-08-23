using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Transform player;
	public Text scoreText;
	private float startingPosition = -45;

	// Update is called once per frame
	void Update () {
		if (!FindObjectOfType<GameManager>().hasGameEnded()){
			scoreText.text = (player.position.z - startingPosition).ToString("0");
		}
	}
}
