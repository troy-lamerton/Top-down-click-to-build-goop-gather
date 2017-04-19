// using System.Collections;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	public Building[] buildingsData;
	
	GameObject selectedTile;

	public void SetActiveTile (GameObject tile) {
		selectedTile = tile;
	}

	public void ChooseBuilding (int typeIndex) {
		var data = buildingsData[typeIndex];

		if (GlobalState.ConsumeGoop(data.cost)) {
			var spawnPosition = new Vector3(selectedTile.transform.position.x, 0, selectedTile.transform.position.z);
			Object.Instantiate(data.modelPrefab, spawnPosition, Quaternion.identity);
			this.HideActiveTile();
		}
	}

	void HideActiveTile () {
		selectedTile.SetActive(false);
	}

}


[System.Serializable]
public class Building {
	public string name;
	public GameObject modelPrefab;
	// public Sprite icon;
	public int cost;
}
