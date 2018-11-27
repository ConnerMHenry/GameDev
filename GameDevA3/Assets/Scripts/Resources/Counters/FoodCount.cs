using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LivingResourcesManager.foodCount = GetComponent<Text>();
		LivingResourcesManager.AddFood(150);
	}
}
