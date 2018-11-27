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
            isAvailable = value;
        }
    }

    public void Start()
    {
        CheckAvailability();
    }

    public void CheckAvailability() {
        List<Building.BuildingCost> requiredResources = building.buildingCosts;

        if (ItemBarController.main == null) {
            IsAvailable = false;
            return;
        }

        foreach (Building.BuildingCost cost in requiredResources) {
            int currentAmount = ItemBarController.main.AmountOf(cost.resource);
            if (cost.amount > currentAmount) {
                IsAvailable = false;
                return;
            }
        }

        IsAvailable = true;
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
