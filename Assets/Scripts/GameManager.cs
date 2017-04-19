// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// singleton - ONE instance
// immortal - always in the scene
public class GameManager : MonoBehaviour {

	// set on startup
	float _timeScale;

	public GameObject pauseMenuCanvas;

	// running state
	List<GameObject> hiddenObjects;

	static GameManager instance;
	public static GameManager GetInstance() {
		return instance;
	}

	void Start () {
		// set the vars with the runtime data
		_timeScale = Time.timeScale;

		ShowPauseMenu(false);
	}

	void Awake () {
		if(instance != null) {
			// Someone ELSE is the singleton already.
			// So let's just destroy ourselves before we cause trouble
			Destroy(this.gameObject);
			return;
		}

		// If we get here, we are "the one". Let's act like it.
		instance = this;	// We are a Highlander
		GameObject.DontDestroyOnLoad( this.gameObject );	// Become immortal
		GameObject.DontDestroyOnLoad( pauseMenuCanvas );	// Become immortal
	}

	public void SwitchToPlayScene () {
		GoScene(1);
	}

	public void SwitchToMenu () {
		GoScene(0);
	}

	void GoScene(int index) {
		switch (index)
		{
			case 0:
				SceneManager.LoadScene("MainMenu");
				break;
			case 1:
				SceneManager.LoadScene("Main");
				break;

			default:
				Debug.LogError("Cant load a scene at the out or range index, " + index);
				break;
		}
	}

	public void TogglePaused () {
		if (SceneManager.GetActiveScene().name == "MainMenu") {
			return;
		}
		if (GlobalState.SetPaused(!GlobalState.paused)) {
			// paused is true
			this.GameplayPause();
			ShowPauseMenu(true);
		} else {
			ShowPauseMenu(false);
			this.GameplayResume();
		}
	}

	void ShowPauseMenu (bool show) {
		pauseMenuCanvas.SetActive(show);
	}

	void GameplayPause () {
		Time.timeScale = 0;
		// disable gameplay input - you cant reposition while paused!
		GlobalState.movementEnabled = false;
		GlobalState.gameplayUIEnabled = false;


		hiddenObjects = new List<GameObject>();
		GameObject[] hideOnPauseObjects = GameObject.FindGameObjectsWithTag("HideOnPause");

		foreach (var ensureActive in hideOnPauseObjects) {
			if (ensureActive.activeInHierarchy) {
				hiddenObjects.Add(ensureActive);
			}
		}
		foreach (var hideMe in hiddenObjects) {
			hideMe.SetActive(false);
		}
	}

	void GameplayResume () {
		Time.timeScale = _timeScale;
		GlobalState.movementEnabled = true;
		GlobalState.gameplayUIEnabled = true;

		foreach (var showMe in hiddenObjects) {
			showMe.SetActive(true);
		}
		hiddenObjects.Clear();
	}


	void OnDestroy() {
		Debug.Log("GameManager was destroyed.");

		// save my private variables on disk
		//PlayerPrefs.SetInt("score", score);
		//PlayerPrefs.SetInt("lives", numLives);
	}	
}
