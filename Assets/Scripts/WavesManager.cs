using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour {

	// after all baddies die, wait this long before next wave starts
	public float nextWaveDelay = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnBaddies () {
		Debug.Log("baddies are preparing...");
		yield return new WaitForSeconds(1f);
		Debug.Log("baddies wave was spawned!!");
	}

	IEnumerator NextWaveCountdown () {
		yield return new WaitForSeconds(nextWaveDelay);
	}
}
