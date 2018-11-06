using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResource<R> : TileObject where R: Resource {

    public delegate void HarvestDelegate(List<R> resources);

    public R resource;
    public int menRequired;
    public int maxAmount;
    public int minAmount;
    private int amount;

    // Harvest values
    protected HarvestDelegate harvestDelegate;
    protected bool isHarvesting;
    protected float harvestTime;
    protected float timeHarvesting;

    // Use this for initialization
    void Start () {
        amount = Random.Range(minAmount, maxAmount);
        isHarvesting = false;

        harvestTime = menRequired * 3;
        timeHarvesting = 0.0f;
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
        for (int i = 0; i < amount; i++)
        {
            resources.Add(ScriptableObject.CreateInstance(underlyingType) as R);
        }

        harvestDelegate(resources);

        Destroy(gameObject);
    }
}
