using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : TileObject {
    public int howMuchRequirements;
    public LivingRequirements providedRequirements;
    public int peopleRequired;
    public float requirementTime;
    public ResourceCollection collection;
    public bool IsOperating {
        get {
            return peopleAssigned == peopleRequired;
        }
    }

    private float requirementTimer = 0.0f;
    private int peopleAssigned = 0;


    public void Update()
    {
        if (peopleAssigned == peopleRequired) {
            requirementTimer += Time.deltaTime;

            if (requirementTimer >= requirementTime) {
                collection.UpdateResource(providedRequirements, howMuchRequirements);
                requirementTimer = 0.0f;
            }
        }
    }
}
