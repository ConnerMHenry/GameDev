using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NaturalResource<R> : TileObject where R: Resource {
    public delegate void HarvestDelegate(List<R> resources);

    public enum ResourceSize {
        Small, Medium, Big, Giant
    }

    public GameObject SpriteHolderPrefab;
    public ResourceSize Size { get; protected set; }

    public List<Sprite> spriteOptions;
    public R resource;
    public int PeopleRequired { get; protected set; }
    public int MaxAmount { get; protected set; }
    public int MinAmount { get; protected set; }
    public int Amount { get; protected set; }

    // Asset placement values
    protected float xBoundary = 0.38f;
    protected float yBoundary = 0.38f;

    // Harvest values
    protected HarvestDelegate harvestDelegate;
    protected bool isHarvesting;
    protected float harvestTime;
    protected float timeHarvesting;

    // Use this for initialization
    void Start () {
        //amount = Random.Range(MinAmount, MaxAmount);
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

            if (timeHarvesting > harvestTime) {
                HarvestDone();
            }
        }
	}

    public void OnHarvest(HarvestDelegate harvestDelegate) {
        this.harvestDelegate = harvestDelegate;
        isHarvesting = true;
    }

    protected void HarvestDone() {
        List<R> resources = new List<R>();
        System.Type underlyingType = resource.GetType();
        for (int i = 0; i < Amount; i++)
        {
            resources.Add(ScriptableObject.CreateInstance(underlyingType) as R);
        }

        harvestDelegate(resources);

        Destroy(gameObject);
    }

    /// <summary>
    /// Method to randomly choose a size for the resource
    /// </summary>
    protected void DetermineSize() {
        ResourceSize[] sizes = (ResourceSize[]) System.Enum.GetValues(typeof(ResourceSize));
        Size = sizes[Random.Range(0, sizes.Length)];
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
