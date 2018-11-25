﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NaturalResource : TileObject {
    public delegate void HarvestDelegate(List<Resource> resources);

    public enum ResourceSize {
        Small, Medium, Big, Giant
    }

    public GameObject SpriteHolderPrefab;
    public ResourceSize Size { get; protected set; }

    public List<Sprite> spriteOptions;
    public Resource resource;
    public int PeopleRequired { get; protected set; }
    public int MaxAmount { get; protected set; }
    public int MinAmount { get; protected set; }
    public int Amount { get; protected set; }

    // Asset placement values

    // Set x and y boundaries to 5, so that they move in increments of 0.06f to a max of 0.30f
    protected float boundaryMultiplier = 0.075f;
    protected int xBoundary = 4;
    protected int yBoundary = 4;

    // Harvest values
    protected HarvestDelegate harvestDelegate;
    protected bool isHarvesting;
    protected float harvestTime;
    protected float timeHarvesting;

    // Use this for initialization
    void Start () {
        isHarvesting = false;

        harvestTime = PeopleRequired * 3;
        timeHarvesting = 0.0f;
        DetermineSize();

        CreateResource();
        Amount = Random.Range(MinAmount, MaxAmount);
	}
	
	// Update is called once per frame
	void Update() {
        if (isHarvesting) {
            timeHarvesting += Time.deltaTime;
            parentTile.ProgressBar.GetComponent<Slider>().value = (timeHarvesting / harvestTime);
            if (timeHarvesting > harvestTime) {
                HarvestDone();
            }
        }
	}

    public void OnHarvest(HarvestDelegate harvestDelegate) {
        this.harvestDelegate = harvestDelegate;
        isHarvesting = true;

        parentTile.CreateProgressBar();
    }

    protected void HarvestDone() {
        //List<Resource> resources = new List<Resource>();
        //System.Type underlyingType = resource.GetType();

        //for (int i = 0; i < Amount; i++)
        //{
        //    resources.Add(ScriptableObject.CreateInstance(underlyingType) as underlyingType);
        //}

        //harvestDelegate(resources);
        parentTile.RemoveProgressBar();
        Destroy(gameObject);
    }

    /// <summary>
    /// Method to randomly choose a size for the resource
    /// </summary>
    protected void DetermineSize() {
        float bigProbability = Random.value;

        if (bigProbability > .9f) {
            Size = Random.Range(0, 100) < 90 ? ResourceSize.Big : ResourceSize.Giant;
        }
        else {
            ResourceSize[] sizes = (ResourceSize[])System.Enum.GetValues(typeof(ResourceSize));
            Size = sizes[Random.Range(0, sizes.Length - 2)];
        }
    }

    /// <summary>
    /// Method to pick a sprite out of the possible options
    /// </summary>
    /// <returns>Sprite from the list of spriteOptions</returns>
    protected Sprite GenerateSprite() {
        int index = Random.Range(0, spriteOptions.Count);
        Sprite sprite = spriteOptions[index];

        return sprite;
    }

    public abstract void CreateResource();
}
