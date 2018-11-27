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

    public int PeopleRequired { get; protected set; }
    public bool IsProducing { get; protected set; }


    private int peopleWorking = 0;
    public int PeopleWorking {
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
        //parentTile.ProgressBar.GetComponent<Slider>().
    }

    public void Start()
    {
        PeopleRequired = 0;
        PeopleRequired = requiredNeeds.Find((LifeNeeds obj) => obj.providedRequirements == LivingRequirements.People).amount;

        IsProducing = false;
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

        if (isBuilt && IsProducing) {
            productionTime += Time.deltaTime;
            parentTile.ProgressBar.GetComponent<Slider>().value = productionTime / totalProductionTime;

            if (productionTime >= totalProductionTime) {
                productionTime = 0.0f;

                foreach(ResourceProduction production in resourceOutput) {
                    ItemBarController.main.Add(production.resource, production.amount);
                }
            }

            //Provide the needs to needs manager.
        }
    }   

    private void ConstructionDone() {
        isBuilding = false;
        buildingAnimator.Play("BuildingIdle");
        parentTile.RemoveProgressBar();
        isBuilt = true;
    }

    //public int howMuchRequirements;
    //public LivingRequirements providedRequirements;
    //public int peopleRequired;
    //public float requirementTime;
    //public ResourceCollection collection;
    //public bool IsOperating {
    //    get {
    //        return peopleAssigned == peopleRequired;
    //    }
    //}

    //private float requirementTimer = 0.0f;
    //private int peopleAssigned = 0;


    //public void Update()
    //{
    //    if (peopleAssigned == peopleRequired) {
    //        requirementTimer += Time.deltaTime;

    //        if (requirementTimer >= requirementTime) {
    //            collection.UpdateResource(providedRequirements, howMuchRequirements);
    //            requirementTimer = 0.0f;
    //        }
    //    }
    //}
}
