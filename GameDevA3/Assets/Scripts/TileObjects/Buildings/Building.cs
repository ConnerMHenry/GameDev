using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : TileObject {

    public string Description;
    public string BuildingName;
    public List<BuildingCost> buildingCosts;
    public List<ResourceProduction> resourceOutput;
    public List<ResourceType> resourceNeeds;
    public List<LifeNeeds> providedNeeds;
    public List<LifeNeeds> requiredNeeds;

    [System.Serializable]
    public struct BuildingCost {
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
