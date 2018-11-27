using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NaturalResource : TileObject {

    public enum ResourceSize {
        Small, Medium, Big, Giant
    }

    public GameObject SpriteHolderPrefab;
    public ResourceSize Size { get; protected set; }

    public List<Sprite> spriteOptions;
    public List<ResourcePossibility> possibleResources;
    public List<Resource> resources { get; protected set; }
    public int PeopleRequired { get; protected set; }

    [System.Serializable]
    public class ResourcePossibility {
        public ResourceType type;
        [Range(0.1f, 1.0f)]
        public float chanceOfOccuring;
    }

    private int peopleWorking = 0;
    public int PeopleWorking {
        get {
            return peopleWorking;
        }

        set {
            if (value <= PeopleRequired)
            {
                isHarvesting = value == PeopleRequired;

                if (value == PeopleRequired && parentTile.ProgressBar == null) {
                    parentTile.CreateProgressBar();
                }
                peopleWorking = value;
            }
        }
    }
    //public int MaxAmount { get; protected set; }
    //public int MinAmount { get; protected set; }
    //public int Amount { get; protected set; }

    // Asset placement values

    // Set x and y boundaries to 5, so that they move in increments of 0.06f to a max of 0.30f
    protected float boundaryMultiplier = 0.075f;
    protected int xBoundary = 4;
    protected int yBoundary = 4;

    // Harvest values
    protected bool isHarvesting;
    protected float harvestTime;
    protected float timeHarvesting;

    // Use this for initialization
    void Start () {
        isHarvesting = false;

        harvestTime = PeopleRequired * 3;
        timeHarvesting = 0.0f;
        DetermineSize();

        PeopleRequired = 1;

        CreateResource();
        //Amount = Random.Range(MinAmount, MaxAmount);

        resources = new List<Resource>();
        foreach(ResourcePossibility possibility in possibleResources) {
            if (Random.value <= possibility.chanceOfOccuring) {
                resources.Add(new Resource(possibility.type));
            }
        }
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

    protected void HarvestDone() {

        foreach (Resource resource in resources)
        {
            ItemBarController.main.Add(resource.ResourceType, resource.Amount);
        }

        parentTile.RemoveProgressBar();


        if (parentTile.IsHighlighted)
        {
            Tile tile = parentTile;
            tile.ChildObject = null;
            TileInfoController.main.CurrentTile = tile;
        }
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
