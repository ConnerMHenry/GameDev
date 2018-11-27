using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LivingResourcesManager.waterCount = GetComponent<Text>();
		LivingResourcesManager.AddWater(150);
	}
}
