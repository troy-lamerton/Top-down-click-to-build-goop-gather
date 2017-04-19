using UnityEngine;

public class InputCallPause : MonoBehaviour {

	GameManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
            gameManager.TogglePaused();
		else if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.TogglePaused();
	}
}
