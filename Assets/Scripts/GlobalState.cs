using UnityEngine;

public class GlobalState {
	// bool paused = false;
	public static int goop = 80;
	public static int waveNumber = 1;
	public static int baddiesAlive = 0;
	public static bool paused = false;
	public static bool movementEnabled = true;
	public static bool gameplayUIEnabled = true;

	public static void AddGoop (int amount) {
		goop += amount;
	}

	public static bool ConsumeGoop (int amount) {
		if (amount <= goop) {
			goop -= amount;
			return true;
		}
		else {
			Debug.Log("Cant afford " + amount + " for that");
			return false;
		}
	}

	public static bool SetPaused (bool pause) {
		paused = pause;
		return paused;
	}

	public static void WavePlusPlus() {
		waveNumber++;
	}

	public static void SetBaddiesAlive(int b) {
		if (b < 0) {
			baddiesAlive -= b;
		} else {
			baddiesAlive = b;
		}
	}

	public static void BaddieDied() {
		SetBaddiesAlive(-1);
	}
}
