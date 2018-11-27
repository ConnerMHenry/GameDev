using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour {

    public string Name;
    public Image spriteImage;
    public Building building;
    public Color disabledColor;
    public BuildingWindowController controller;

    private bool isAvailable = false;
    public bool IsAvailable {
        get {
            return isAvailable;
        }
        set {
            ToggleAvailability(value);
            isAvailable = true;
        }
    }

    public void Start()
    {
        CheckAvailability();
    }

    private void CheckAvailability() {

    }

    private void ToggleAvailability(bool available) {
        spriteImage.color = available ? Color.white : disabledColor;
    }

    public void OnPointerOver() {
        if (controller != null)
        {
            controller.DisplayBuilding(this);
        }
    }
}
