using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private bool gameEnded = false;
	public float restartDelay = 1f;
	public void EndGame () {
		if ( !gameEnded ){
			print("GAME OVER");
			gameEnded = true;
			Invoke("Restart", restartDelay);
		}
	}

	public bool hasGameEnded() {
		return gameEnded;
	}

	void Restart () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
