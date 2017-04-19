using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextsGameObjects {
    public Text goopAmount;
    public Text currentWave;
}

// keeps texts up to date, shows and hide popovers in the HUD.
// DOES NOT show/hide BuildHere buttons (or any world space buttons)

public class InterfaceManager : MonoBehaviour {
    public TextsGameObjects texts;
    public GameObject buildingPopover;

    // float screenWidth = 960f;
    // float screenHeight = 420f;

    List<GameObject> popoversList = new List<GameObject>();
    void Initialize () {
        popoversList.Add(buildingPopover);
        // popoversList.Add(coolPopover);
    }
    void Start () {
        this.Initialize();
        this.UpdateTexts();
    }

    void FixedUpdate () {
        UpdateTexts();
    }

    public void ShowBuildingPopover () {
        this.ShowPopover(buildingPopover);
    }

    void ShowPopover (GameObject popoverToShow, bool hideOthers = true) {
        if (hideOthers) {
            HidePopovers();
        }

        this.PositionAtCursor(popoverToShow.transform);
        popoverToShow.SetActive(true);
    }

    public void HidePopovers () {
        foreach (GameObject popover in popoversList) {
            popover.SetActive(false);
        }
    }





    void UpdateTexts () {
        texts.goopAmount.text = GlobalState.goop.ToString();
        texts.currentWave.text = GlobalState.waveNumber.ToString();
    }

    void PositionAtCursor(Transform transform) {
        var screenPoint = Input.mousePosition;
        // TODO: use rectTransform component
        transform.position = screenPoint;
    }
}