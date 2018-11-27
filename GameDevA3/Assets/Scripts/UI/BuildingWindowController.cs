using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingWindowController : MonoBehaviour {
    public Button buildButton;
    public TextMeshProUGUI BuildingName;
    public TextMeshProUGUI BuildingDesc;

    public BuildingSlot defaultSlot;

    // Use this for initialization
    void Start () {
        if (defaultSlot != null) {
            DisplayBuilding(defaultSlot);
        }
        buildButton.interactable = false;
	}

    public void DisplayBuilding(BuildingSlot buildingSlot) {
        BuildingName.text = buildingSlot.Name;
        BuildingDesc.text = buildingSlot.building.Description;
        buildButton.interactable = buildingSlot.IsAvailable;
    }
}
