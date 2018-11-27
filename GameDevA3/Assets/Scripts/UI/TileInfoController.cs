using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileInfoController : MonoBehaviour {

    public static TileInfoController main;

    public Animator animator;
    public TextMeshProUGUI tileText;

    public TextMeshProUGUI peopleRequiredText;
    public GameObject resourcesArea;
    public Button buildButton;
    public Button addButton;
    public Button removeButton;

    public ResourceRowController ResourceRowPrefab;

    public List<ResourceRowController> resourceRows;

    private Tile currentTile;
    private TileObject currentObject;

    public Tile CurrentTile {
        get {
            return currentTile;
        }
        set {
            if (value != null && currentTile == null) {
                animator.Play("SlideInAnimationTileInfo");
            }
            else if (value == null && currentTile != null) {
                animator.Play("SlideOutAnimationTileInfo");
            }

            if (value != null) {
                SetTileInfo(value);
            }

            currentTile = value;
        }
    }

    private void SetTileInfo(Tile tile) {
        ClearResourceRows();

        tileText.text = tile.Name;

        if (tile.ChildObject != null) {
            currentObject = tile.ChildObject;
            UpdatePeopleAmount();
            foreach (Resource resource in currentObject.Resources)
            {
                if (resource.Amount != 0)
                {
                    CreateResourceRow(resource.ResourceType, resource.Amount);
                }
            }
            buildButton.interactable = false;
        }
        else {
            peopleRequiredText.text = "0/0";
            addButton.interactable = false;
            removeButton.interactable = false;
            buildButton.interactable = true;
        }
    }

	// Use this for initialization
	void Start () {
        if (main == null) {
            main = this;
        }
        else if (main != this) {
            Destroy(this);
        }

        resourceRows = new List<ResourceRowController>();

        removeButton.interactable = false;
        addButton.interactable = true;
        buildButton.interactable = false;
    }

    private void CreateResourceRow(ResourceType resourceType, int amount) {
        int index = resourceRows.Count;

        ResourceRowController newRow = Instantiate(ResourceRowPrefab) as ResourceRowController;
        newRow.SetInfo(resourceType, amount);
        newRow.transform.parent = resourcesArea.transform;
        newRow.transform.localScale = new Vector3(1, 1);
        newRow.transform.localPosition = new Vector2(0, 0);
        RectTransform rect = newRow.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, -(index * 12));

        resourceRows.Add(newRow);
    }

    private void ClearResourceRows() {
        foreach (ResourceRowController resourceRow in resourceRows) {
            Destroy(resourceRow.gameObject);
        }

        resourceRows.Clear();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AssignPersonToResource() {
<<<<<<< HEAD
        if (currentResource.PeopleWorking < currentResource.PeopleRequired && LivingResourcesManager.GetWorkers(1)) {
            currentResource.PeopleWorking += 1;
=======

        if (currentObject != null && currentObject.PeopleWorking < currentObject.PeopleRequired) {
            currentObject.PeopleWorking += 1;
>>>>>>> master
        }

        UpdatePeopleAmount();
    }

    public void RemovePersonFromResource() {
<<<<<<< HEAD
        if (currentResource.PeopleWorking > 0) {
            currentResource.PeopleWorking -= 1;
			LivingResourcesManager.AddWorkers(1);
=======
        if (currentObject.PeopleWorking > 0) {
            currentObject.PeopleWorking -= 1;
>>>>>>> master
        }

        UpdatePeopleAmount();
    }

    private void UpdatePeopleAmount() {
        peopleRequiredText.text = currentObject.PeopleWorking + "/" + currentObject.PeopleRequired;
        removeButton.interactable = currentObject.PeopleWorking > 0;
        addButton.interactable = currentObject.PeopleWorking < currentObject.PeopleRequired;
    }

    public void ShowBuildWindow() {
        if (!BuildingWindowController.main.IsVisible) {
            BuildingWindowController.main.Show();
        }
    }
}
