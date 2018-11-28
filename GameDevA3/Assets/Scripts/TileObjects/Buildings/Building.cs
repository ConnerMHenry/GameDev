using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : TileObject {

    public string Description;
    public float totalProductionTime;
    public float totalConstructionTime;
    public List<BuildingCost> buildingCosts;
    public List<ResourceProduction> resourceOutput;
    public List<ResourceType> resourceNeeds;
    public List<LifeNeeds> providedNeeds;
    public List<LifeNeeds> requiredNeeds;
    public Animator buildingAnimator;

    private float constructionTime = 0.0f;
    private float productionTime = 0.0f;
    private bool isBuilding = false;
    private bool isBuilt = false;

    public bool IsProducing { get; protected set; }

    public override int PeopleWorking {
        get {
            return peopleWorking;
        }
        set {
            if (value <= PeopleRequired && isBuilt) {
                IsProducing = value == PeopleRequired;

                if (value == PeopleRequired && parentTile.ProgressBar == null) {
                    parentTile.CreateProgressBar();
                }

                peopleWorking = value;
            }
        }
    }

    public override List<Resource> Resources {
        get {
            List<Resource> resources = new List<Resource>();
            foreach (ResourceProduction production in resourceOutput) {
                Resource resource = new Resource(production.resource);
                resource.Amount = production.amount;
                resources.Add(resource);
            }

            return resources;
        }
    }

    [System.Serializable]
    public class BuildingCost {
        public ResourceType resource;
        public int amount;
    }

    [System.Serializable]
    public struct ResourceProduction {
        public ResourceType resource;
        public int amount;

        [Range(0, 1)]
        public float chance;
    }

    [System.Serializable]
    public struct LifeNeeds
    {
        public LivingRequirements providedRequirements;
        public int amount;
    }


    public void SetActive(bool active) {
        if (!active) {
            gameObject.SetActive(false);
        }
    }


    public void StartConstruction() {
        isBuilding = true;
        buildingAnimator.Play("BuildingAnimation");
        parentTile.CreateProgressBar();
		SFXManager.PlayEffect(totalConstructionTime);
    }

    public void Start()
    {
        PeopleRequired = 0;
        PeopleRequired = requiredNeeds.Find((LifeNeeds obj) => obj.providedRequirements == LivingRequirements.People).amount;

        IsProducing = PeopleRequired == 0;
    }

    public void Update()
    {
        if (isBuilding) {
            constructionTime += Time.deltaTime;
            parentTile.ProgressBar.GetComponent<Slider>().value = constructionTime / totalConstructionTime;
            if (constructionTime >= totalConstructionTime) {
                ConstructionDone();
            }
        }

        bool hasResources = true;

        foreach (ResourceType need in resourceNeeds)
        {
            hasResources = ItemBarController.main.AmountOf(need) > 0;

            if (!hasResources){
				Debug.Log("Doesn't have resources");
                break;
            }
        }

		bool hasRequirements = true;

		// if there are resources, check the living
		foreach (LifeNeeds need in requiredNeeds)
		{
			switch (need.providedRequirements)
			{
				case LivingRequirements.Energy:
					Debug.Log("Doesnt have power: " + need.amount);
					hasRequirements = LivingResourcesManager.CheckPower(need.amount);
					break;
				case LivingRequirements.Food:
					Debug.Log("Doesnt have food: " + need.amount);
					hasRequirements = LivingResourcesManager.CheckFood(need.amount);
					break;
				case LivingRequirements.Water:
					Debug.Log("Doesnt have water: " + need.amount);
					hasRequirements = LivingResourcesManager.CheckWater(need.amount);
					break;
				default:
					break;
			}
			if (!hasRequirements)
			{
				Debug.Log("Doesn't have requirements");
				break;
			}
		}

        if (isBuilt && IsProducing && (hasResources && hasRequirements)) {
            productionTime += Time.deltaTime;
            parentTile.ProgressBar.GetComponent<Slider>().value = productionTime / totalProductionTime;

            // Check somewhere that resourceNeeds are met
            if (productionTime >= totalProductionTime) {
                productionTime = 0.0f;

                foreach(ResourceProduction production in resourceOutput) {
                    if (Random.value <= production.chance)
                    {
                        ItemBarController.main.Add(production.resource, production.amount);
                    }
                }

                foreach(ResourceType need in resourceNeeds) {
                    ItemBarController.main.Add(need, -1);
                }

				foreach (LifeNeeds need in requiredNeeds)
				{
					switch (need.providedRequirements)
					{
						case LivingRequirements.Energy:
							LivingResourcesManager.GetPower(need.amount);
							break;
						case LivingRequirements.Food:
							LivingResourcesManager.GetFood(need.amount);
							break;
						case LivingRequirements.Water:
							LivingResourcesManager.GetWater(need.amount);
							break;
						default:
							break;
					}
				}

				foreach (LifeNeeds need in providedNeeds)
				{
					switch (need.providedRequirements)
					{
						case LivingRequirements.Energy:
							LivingResourcesManager.AddPower(need.amount);
							break;
						case LivingRequirements.Food:
							LivingResourcesManager.AddFood(need.amount);
							break;
						case LivingRequirements.Water:
							LivingResourcesManager.AddWater(need.amount);
							break;
						default:
							break;
					}
				}

				Debug.Log("Getting up keep for: " + PeopleRequired);
				LivingResourcesManager.GetUpKeep(PeopleRequired);
			}
            //Provide the needs to needs manager.
        }
    }   

    private void ConstructionDone() {
        isBuilding = false;
        buildingAnimator.Play("BuildingIdle");
        parentTile.RemoveProgressBar();
        isBuilt = true;

		foreach(LifeNeeds need in providedNeeds)
		{
			if (need.providedRequirements == LivingRequirements.People)
			{
				LivingResourcesManager.AddLivingSpace(need.amount);
				LivingResourcesManager.AddWorkers(need.amount);
			}
		}

        if (parentTile.IsHighlighted) {
            TileInfoController.main.CurrentTile = parentTile;
        }

		if (IsProducing)
		{
			parentTile.CreateProgressBar();
		}
    }
}
