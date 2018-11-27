using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingWindowController : MonoBehaviour {
    public static BuildingWindowController main;

    public Button buildButton;
    public TextMeshProUGUI BuildingName;
    public TextMeshProUGUI BuildingDesc;
    public GameObject ResourceAreas;
    public ResourceRowController ResourceRowPrefab;
    public Animator animator;

    public List<BuildingSlot> buildingSlots;

    public BuildingSlot defaultSlot;
    private BuildingSlot currentSlot;

    private List<ResourceRowController> resourceRows;

    public bool IsVisible { get; private set; }

    // Use this for initialization
    void Start () {
        if (main == null) {
            main = this;
        }
        else if (main != this) {
            Destroy(this.gameObject);
        }

        resourceRows = new List<ResourceRowController>();
        if (defaultSlot != null) {
            DisplayBuilding(defaultSlot);
        }
        buildButton.interactable = false;
        IsVisible = false;
	}

    public void DisplayBuilding(BuildingSlot buildingSlot) {
        this.currentSlot = buildingSlot;
        BuildingName.text = buildingSlot.Name;
        BuildingDesc.text = buildingSlot.building.Description;
        buildButton.interactable = buildingSlot.IsAvailable;
        ClearResourceRows();

        foreach (Building.BuildingCost cost in currentSlot.building.buildingCosts) {
            CreateResourceRow(cost.resource, cost.amount);
        }
    }

    private void CreateResourceRow(ResourceType resourceType, int amount)
    {
        int index = resourceRows.Count;

        ResourceRowController newRow = Instantiate(ResourceRowPrefab) as ResourceRowController;
        newRow.SetInfo(resourceType, amount);
        newRow.transform.parent = ResourceAreas.transform;
        newRow.transform.localScale = new Vector3(1, 1);
        newRow.transform.localPosition = new Vector2(0, 0);
        RectTransform rect = newRow.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, -(index * 12));

        resourceRows.Add(newRow);
    }

    private void ClearResourceRows()
    {
        if (resourceRows != null)
        {

            foreach (ResourceRowController resourceRow in resourceRows)
            {
                Destroy(resourceRow.gameObject);
            }

            resourceRows.Clear();
        }
        else {
            resourceRows = new List<ResourceRowController>();
        }
    }

    public void Show() {
        UpdateSlots();

        animator.Play("ShowWindow");
        IsVisible = true;
    }

    public void UpdateSlots() {
        // Update slots availability
        foreach (BuildingSlot slot in buildingSlots)
        {
            slot.CheckAvailability();
        }
        buildButton.interactable = currentSlot.IsAvailable;
    }

    public void Dismiss() {
        animator.Play("DismissWindow");
        IsVisible = false;
    }

    public void BuildOnTile() {
        if (currentSlot != null && HighlightManager.current_target != null) {
            Tile tile = HighlightManager.current_target;
            if (tile.ChildObject == null) {
                Building building = Instantiate(currentSlot.building) as Building;
                tile.ChildObject = building;
                building.StartConstruction();
                foreach (Building.BuildingCost cost in currentSlot.building.buildingCosts) {
                    ItemBarController.main.Add(cost.resource, -cost.amount);
                }

                Dismiss();
            }
        }
    }
}
