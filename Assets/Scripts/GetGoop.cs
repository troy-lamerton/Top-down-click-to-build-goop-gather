
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class GetGoop : MonoBehaviour {

	Coroutine mining;

	void OnTriggerEnter (Collider coll) {
		var GO = coll.gameObject;
		if (GO.tag == "Player") {
			// mine that goop mound
			mining = StartCoroutine(TakeGoop());
		}
	}

	void OnTriggerExit (Collider coll) {
		var GO = coll.gameObject;
		if (GO.tag == "Player") {
			// stop mining goop
			StopCoroutine(mining);
		}
	}

	IEnumerator TakeGoop () {
		// initial delay
		yield return new WaitForSeconds(0.3f);
		// add goop loop
		while (true) {
			yield return new WaitForSeconds(0.5f);
			GlobalState.AddGoop(1);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
