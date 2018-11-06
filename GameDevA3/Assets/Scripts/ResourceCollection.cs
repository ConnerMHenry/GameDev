using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour {

    public int food { get; private set; }
    public int water { get; private set; }
    public int energy { get; private set; }
    public int people { get; private set; }
    public int shelter { get; private set; }

    //private List<>

    // Use this for initialization
    void Start () {
        food = 0;
        water = 0;
        energy = 0;
        people = 0;
        shelter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateResource(LivingRequirements res, int amount) {

        switch (res) {
            case LivingRequirements.Food:
                food += amount;
                break;

            case LivingRequirements.Water:
                water += water;
                break;

            case LivingRequirements.Energy:
                energy += amount;
                break;

            default:
                break;
        }
    }

    public void AddBuilding(Building building) {
        building.collection = this;
    }
}
